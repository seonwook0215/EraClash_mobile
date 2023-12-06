using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sight : MonoBehaviour
{
    public float distance;
    //public float angle;
    public LayerMask objectsLayers;
    public Collider detectedObject;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, distance, objectsLayers);

        if (colliders.Length != 0)
        {
            detectedObject= colliders[0];
            float shorttest_Dis = Vector3.Distance(transform.position, colliders[0].transform.position);
            foreach (Collider collider in colliders)
            {
                float short_dis = Vector3.Distance(transform.position, collider.transform.position);
                if (short_dis < shorttest_Dis)
                {
                    shorttest_Dis = short_dis;
                    detectedObject = collider;
                }
            }
        }
        else
        {
            detectedObject = null;
        }


        //Vector3 directionToController = Vector3.Normalize(collider.bounds.center - transform.position);
        //float angleToCollider = Vector3.Angle(transform.forward, directionToController);
        //if (angleToCollider < angle)
        //{
        //    if (!Physics.Linecast(transform.position, collider.bounds.center, (int)obstaclesLayers))
        //    {
        //        detectedObject = collider;
        //        break;
        //    }
        //}
    }
    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, distance);

    }
}
