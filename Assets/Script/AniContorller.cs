using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniContorller : MonoBehaviour
{
   // public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<overlapspere>().enemy_inrange)
        {
         //   this.GetComponent<Animator>().SetBool("EnemyinRange",true);
            Debug.Log("enemy in range");
        }
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    anim.SetTrigger("Run");
        //}
        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    anim.SetTrigger("Impact");
        //}
    }
}
