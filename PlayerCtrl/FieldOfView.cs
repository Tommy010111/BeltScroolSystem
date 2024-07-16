using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float viewRadius;
    public float viewAngle;
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    //있어야함, 왜냐면 타겟 사이에 다른 오브젝트가 있는데 그 오브젝트를 투과해서 뒤의 타겟오브젝트를 볼 수 있음 

    public List<Transform> visibleTargets = new List<Transform>();


    public void FindTargets(float viewRadiuses, float viewAngles,float dmg)
    {
        visibleTargets.Clear();
        viewAngle = viewAngles;
        viewRadius = viewRadiuses;
        Vector3 myPos = transform.position + new Vector3(0,1,0);
        Collider[] targetInViewRadius = Physics.OverlapSphere(myPos, viewRadiuses, targetMask);

        for (int i = 0; i < targetInViewRadius.Length; i++)
        //ViewRadius 안에 있는 타겟의 개수 = 배열의 개수보다 i가 작을 때 for 실행 
        {
            Transform target = targetInViewRadius[i].transform; //타겟[i]의 위치 
            Vector3 targetPos = target.position + new Vector3(0,1,0);
            Vector3 dirToTarget = (targetPos - myPos).normalized;
             //vector3타입의 타겟의 방향 변수 선언 = 타겟의 방향벡터, 타겟의 position - 이 게임오브젝트의 position) normalized = 벡터 크기 정규화 = 단위벡터화
            if (Vector3.Angle(transform.forward, dirToTarget) < viewAngles / 2)
            // 전방 벡터와 타겟방향벡터의 크기가 시야각의 1/2이면 = 시야각 안에 타겟 존재 
            {
                float dstToTarget = Vector3.Distance(myPos, targetPos); //타겟과의 거리를 계산 
                if (!Physics.Raycast(myPos, dirToTarget, dstToTarget, obstacleMask))
                {
                    visibleTargets.Add(target);
                    targetInViewRadius[i].GetComponent<EnemyHit>().ShowTxt(dmg);
                    print("raycast hit!");
                    Debug.DrawRay(myPos, dirToTarget * 10f, Color.red, 5f);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.red;
        // DrawSolidArc(시작점, 노멀벡터(법선벡터), 그려줄 방향 벡터, 각도, 반지름)
        Handles.DrawSolidArc(transform.position + new Vector3(0, 1, 0), Vector3.up, transform.forward, viewAngle / 2, viewRadius);
        Handles.DrawSolidArc(transform.position + new Vector3(0, 1, 0), Vector3.up, transform.forward, -viewAngle / 2, viewRadius);
    }
}
