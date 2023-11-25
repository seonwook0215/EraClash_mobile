using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    //public static Life instance;
    public float amount;
    //public UnityEvent onDeath;

    private void Update()
    {
        //Debug.Log(amount);
        if (amount <= 0)
        {
            if (this.tag != "Building")
            {

                //Debug.Log("death");
                this.GetComponent<Animator>().SetTrigger("IsDeath");
                if (this.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    Destroy(this.GetComponent<PUnit>());
                }
                else if (this.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    Destroy(this.GetComponent<EUnit>());
                }
                if (this.tag == "Archer")
                {
                    Destroy(this.GetComponent<ShootArrow>());
                }
                Destroy(this.GetComponent<SphereCollider>());
                Destroy(this.GetComponent<Rigidbody>());
                StartCoroutine(Death());
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Death()
    {

        //Debug.Log("death");
        yield return new WaitForSecondsRealtime(3.0f);
        Destroy(gameObject);
    }
}
