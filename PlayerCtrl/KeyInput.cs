using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyInput : MonoBehaviour
{
    public Vector2 move;
    public bool attack;
    public bool analogMovement;
    public bool evade;
    public bool heavyAttack;


    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void OnAttack(InputValue value)
    {
        AttackInput(value.isPressed);
    }

    public void AttackInput(bool newAttackInput)
    {
        attack = newAttackInput;
    }

    public void OnEvade(InputValue value)
    {
        EvadeInput(value.isPressed);
    }

    public void EvadeInput(bool newEvadeInput)
    {
        evade = newEvadeInput;
    }

    public void OnHeavyAttack(InputValue value)
    {
        HeavyAttackInput(value.isPressed);
    }

    public void HeavyAttackInput(bool newHeavyAttackInput)
    {
        heavyAttack = newHeavyAttackInput;
    }

}
