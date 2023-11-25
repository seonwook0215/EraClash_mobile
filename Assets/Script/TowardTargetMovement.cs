using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toward : MonoBehaviour
{
    public Transform target;
    public float speed;


    private void Update()
    {
        this.transform.LookAt(target);
       // this.transform.position = Vector3.MoveTowards(this.transform.position, target.position, speed * Time.deltaTime);
       
    }
}
