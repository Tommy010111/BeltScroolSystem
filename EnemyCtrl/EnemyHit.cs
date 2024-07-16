using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyHit : MonoBehaviour
{
    [SerializeField]
    TextMeshPro textMeshPro;
    public void ShowTxt(float dmg)
    {
        textMeshPro.text = dmg.ToString();
    }
}
