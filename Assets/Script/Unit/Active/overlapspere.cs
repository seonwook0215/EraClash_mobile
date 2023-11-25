using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class overlapspere : MonoBehaviour
{
    public float radius =0f;
    public LayerMask layer;
    public Collider[] enemis;
    public Collider target;
    public bool enemy_inrange = false;
    public float angle = 45;
    public float distance = 3;
    //public Transform[] enemis;
    //public Transform target;

    private void Update()
    {
        enemis = Physics.OverlapSphere(transform.position, radius, layer);
        if (enemis.Length != 0)
        {
            enemy_inrange = true;
            target = enemis[0];
            float shorttest_Dis = Vector3.Distance(transform.position, enemis[0].transform.position);
            foreach (Collider enemy in enemis)
            {
                float short_Dis = Vector3.Distance(transform.position, enemy.transform.position);
                if (short_Dis < shorttest_Dis)
                {
                    shorttest_Dis = short_Dis;
                    target = enemy;
                }
            }
        }
        else
        {
            enemy_inrange = false;
        }

        //  FindcloseTarget();
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, radius);

        //Vector3 rightDirection = Quaternion.Euler(0, angle, 0) * transform.forward;
        //Gizmos.DrawRay(transform.position, rightDirection * distance);

        //Vector3 leftDirection = Quaternion.Euler(0, -angle, 0) * transform.forward;
        //Gizmos.DrawRay(transform.position, leftDirection * distance);
    }
}
