using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public static class userState
{
    public enum playerState { Idel, Attack, Dodge, Hold, Delay }
    public enum attackType { Fire, Water, Wind, Ston }
}


public class CharMove : State
{
    public static CharMove instance;


    public userState.playerState state;

    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;

    [Tooltip("How fast the character turns to face movement direction")]
    [Range(0.0f, 0.3f)]
    public float RotationSmoothTime = 0.12f;
    Vector3 dir = Vector3.zero;
    public float _speed;   
    private bool _hasAnimator;
    public float _animationBlend;
    [SerializeField]
    public float _targetRotation = 0.0f;
    private float _verticalVelocity;
    public float lastView;
    public Animator _animator;


    public KeyInput _input;
    private PlayerInput _playerInput;
    public Rigidbody _controller;
    private GameObject _mainCamera;
    private CapsuleCollider _capsuleCollider;

    private int _animIDSpeed;
    private int _animIDMotionSpeed;

    private void Awake()
    {
        instance = this;

        // get a reference to our main camera
        if (_mainCamera == null)
        {
            _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
        _animator = GetComponent<Animator>();
        _controller = GetComponent<Rigidbody>();
        _capsuleCollider = GetComponent<CapsuleCollider>();
        AssignAnimationIDs();
    }

    void Update()
    {
        if(state != userState.playerState.Dodge && _capsuleCollider.enabled == false)
        {
            _capsuleCollider.enabled = true;
        }

        hpBar.fillAmount = curHp / maxHp;
        mpBar.fillAmount = curMp / maxMp;

        dir.x = _input.move.x;
        dir.z = _input.move.y;
        dir.Normalize();
    }

    private void FixedUpdate()
    {
        Move();
    }

    // Update is called once per frame
    

    private void AssignAnimationIDs()
    {
        _animIDSpeed = Animator.StringToHash("Speed");
        _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
    }

    private void Move()
    {
        if (state != userState.playerState.Idel)
            return;
        float targetSpeed = speed;

        if (_input.move == Vector2.zero) targetSpeed = 0.0f;

        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            _speed = Mathf.Round(_speed * 1000f) / 1000f;
        }
        else
        {
            _speed = targetSpeed;
        }

        _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (_animationBlend < 0.01f) _animationBlend = 0f;

        if (_input.move != Vector2.zero)
        {
            if(_input.move.x != 0)
            {
                if(_input.move.x >= 0)
                {
                    lastView = 1;
                }
                else if (_input.move.x < 0)
                {
                    lastView = -1;
                }

                transform.rotation = Quaternion.Euler(0.0f, 90 * lastView, 0.0f);                
            }
            else if(_input.move.y != 0)
            {
                transform.rotation = Quaternion.Euler(0.0f, 90 * lastView, 0.0f);
            }
            
        }

        _controller.MovePosition(gameObject.transform.position + dir * (speed * Time.deltaTime));
        
        _animator.SetFloat(_animIDSpeed, _animationBlend);
        _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        

    }
}
