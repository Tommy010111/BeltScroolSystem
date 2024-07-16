using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class State : MonoBehaviour
{
    [Header("기본 스탯")]
    public float lv;
    public float maxHp;
    public float curHp;
    public float maxMp;
    public float curMp;
    public float speed;
    public float dmg;

    [Header("스탯 표시하는 곳")]
    //public
    public Image hpBar;
    public Image mpBar;

}
