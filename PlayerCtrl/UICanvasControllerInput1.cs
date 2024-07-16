using UnityEngine;


public class UICanvasControllerInput1 : MonoBehaviour
{

    [Header("Output")]
    public KeyInput starterAssetsInputs;

    public void VirtualMoveInput(Vector2 virtualMoveDirection)
    {
        starterAssetsInputs.MoveInput(virtualMoveDirection);
    }


    public void VirtualEvadeInput(bool virtualEvadeState)
    {
        starterAssetsInputs.EvadeInput(virtualEvadeState);
    }

    public void VirtualAttackInput(bool virtualAttackState)
    {
        starterAssetsInputs.AttackInput(virtualAttackState);
    }

}

