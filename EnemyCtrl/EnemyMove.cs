using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class EnemyMove : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float range = 10f;
    float distance;
    public userState.playerState state;
    int lastView;
    [Tooltip("Acceleration and deceleration")]
    public float SpeedChangeRate = 10.0f;
    public float _speed = 0;
    Rigidbody _controller;
    Animator _animator;
    [SerializeField]
    public float _targetRotation = 0.0f;
    public float _animationBlend;

    private void Start()
    {
        _controller = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }
    private void Update()
    {
        Attack();
    }

    void FixedUpdate()
    {
        Move();
        Look();
    }

    private void Move()
    {
        distance = Vector3.Distance(transform.position, player.position);

        if (state != userState.playerState.Idel)
            return;

        
        float targetSpeed = speed;

        if (distance <= range) targetSpeed = 0.0f;
        float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

        float speedOffset = 0.1f;
        float inputMagnitude = 1f;

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

        if(_speed != 0)
        {
            _controller.MovePosition(Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime));
        }
        

        _animator.SetFloat("Speed", _animationBlend);
        _animator.SetFloat("MotionSpeed", inputMagnitude);

    }

    void Look()
    {
        if (player.position.x > gameObject.transform.position.x)
        {
            lastView = 1;
        }
        else
        {
            lastView = -1;
        }

        transform.rotation = Quaternion.Euler(0.0f, 90 * lastView, 0.0f);
    }

    void Attack()
    {
        if (distance <= range)
        {
            state = userState.playerState.Hold;
            _animator.SetTrigger("Attack");
        }
    }
}
