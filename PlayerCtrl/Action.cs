using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class Action : MonoBehaviour
{
    KeyInput _input;
    Animator _animator;
    Rigidbody _controller;
    float dashDelay;
    [SerializeField]
    float dashCool;
    CharMove player;
    FieldOfView fov;
    CapsuleCollider coll;

    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<KeyInput>();
        _animator = GetComponent<Animator>();
        _controller = GetComponent<Rigidbody>();
        fov = GetComponent<FieldOfView>();
        player = GetComponent<CharMove>();
        coll = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
        Evade();
        HeavyAttack();
    }

    void Evade()
    {
        if (dashDelay > 0)
        {
            dashDelay -= Time.deltaTime;
        }

        if (_input.evade && dashDelay <= 0)
        {
            dashDelay = dashCool;
            player.state = userState.playerState.Dodge;
            coll.enabled = false;
            _animator.SetTrigger("Evade");
            _input.evade = false;
        }
        else if (_input.evade)
        {
            _input.evade = false;
        }
    }

    void Attack()
    {
        if (_input.attack && player.state == userState.playerState.Hold)
        {
            player.state = userState.playerState.Attack;
            _animator.SetTrigger("Attack");
            _input.attack = false;
        }
        else if (_input.attack && (player.state == userState.playerState.Idel || player.state == userState.playerState.Attack))
        {
            player.state = userState.playerState.Attack;
            _animator.SetTrigger("Attack");
            _input.attack = false;
        }
        else if (_input.attack && player.state == userState.playerState.Delay)
        {
            _input.attack = false;
        }
        else
        {
            _input.attack = false;
        }
    }

    public void AttackCmd(AttackDetail detail)
    {
        fov.FindTargets(detail.radius, detail.angle, detail.drain * player.dmg);
    }

    void HeavyAttack()
    {
        if (_input.heavyAttack)
        {
            player.state = userState.playerState.Hold;
            _animator.SetBool("Hold", true);
        }
        else
        {
            _animator.SetBool("Hold", false);
        }
    }
    
}
