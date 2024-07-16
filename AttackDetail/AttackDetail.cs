using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ATkDetail",menuName ="Attack/Type",order = 0)]
public class AttackDetail : ScriptableObject
{
    public userState.attackType attackType;
    public float radius;
    public float angle;
    public float drain;
}
