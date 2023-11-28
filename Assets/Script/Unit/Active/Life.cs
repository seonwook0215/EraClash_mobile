using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    //public static Life instance;
    public float amount;
    private Animator _animator;
    public float _amount
    {
        set
        {
            amount = _amount;
            hit();
        }
        get
        {
            return amount;
        }

    }
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
    private void Start()
    {
        amount = 100f;
    }

    private void Update()
    {
        //Debug.Log(amount);
        if (amount <= 0)
        {
            Destroy(gameObject);
            //if (this.tag != "Building")
            //{

            //    //Debug.Log("death");
            //    this.GetComponent<Animator>().SetTrigger("IsDeath");
            //    if (this.gameObject.layer == LayerMask.NameToLayer("Player"))
            //    {
            //        Destroy(this.GetComponent<PUnit>());
            //    }
            //    else if (this.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            //    {
            //        Destroy(this.GetComponent<EUnit>());
            //    }
            //    if (this.tag == "Archer")
            //    {
            //        Destroy(this.GetComponent<ShootArrow>());
            //    }
            //    //Destroy(this.GetComponent<SphereCollider>());
            //    //Destroy(this.GetComponent<Rigidbody>());

            //    StartCoroutine(Death());
            //}
            //else
            //{
            //    Destroy(gameObject);
            //}
        }
    }

    IEnumerator Death()
    {
        yield return new WaitForSecondsRealtime(3.0f);
        Destroy(gameObject);
    }

    public void HitDamage(float damage)
    {
        _amount = amount - damage;
        amount -= damage;
        //Debug.Log(damage+ " "+ _amount);
    }
    void hit()
    {
        if(gameObject.layer==LayerMask.NameToLayer("Player") || gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            _animator.SetTrigger("IsHit");
        }
    }
}
