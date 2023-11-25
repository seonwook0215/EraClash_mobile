using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootArrow : MonoBehaviour
{
    public GameObject prefab;
    public GameObject shootpoint;
    bool atk = true;
   // Update is called once per frame
    void FixedUpdate()
    {
        if (this.GetComponent<Animator>().GetBool("EnemyinRange"))
        {
            if (atk) {
                atk = false;
                StartCoroutine(Shoot());
            }            
        }
        else
        {
            atk = true;
        }
    }

    IEnumerator Shoot()
    {
        bool ch = true;
        if (atk)
        {
            ch = false;
        }
        if (ch)
        {
            yield return new WaitForSecondsRealtime(1.08f);
            Instantiate(prefab, shootpoint.transform.position, shootpoint.transform.rotation);
            StartCoroutine(Shoot());
            ch = false;
        }
        
    }
}
