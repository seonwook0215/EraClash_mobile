using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Life : MonoBehaviour
{
    //public static Life instance;
    public float amount;
    private Animator _animator;
    private NavMeshAgent agent;

    public AudioSource hurtaudioSource;
    public AudioClip hurt_clip;
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
        agent = GetComponentInParent<NavMeshAgent>();
    }
    private void Start()
    {
        if (this.tag == "Archer")
        {
            amount = 20f;
            _animator.SetBool("Alive", true);
        }
        else if (this.tag == "Shield")
        {
            amount = 50f;
            _animator.SetBool("Alive", true);
        }
        else if (this.tag == "Sword")
        {
            amount = 30f;
            _animator.SetBool("Alive", true);
        }
        else if (this.tag == "Spear")
        {
            amount = 40f;
            _animator.SetBool("Alive", true);
        }
    }

    private void Update()
    {
        //Debug.Log(amount);
        if (amount <= 0)
        {
            if (gameObject.layer==LayerMask.NameToLayer("Player")&&!AttackManager.instance.useArdrenaline)
            {
                StartCoroutine(Death());
            }
            else if(gameObject.layer == 10)
            {
                
            }
            else
            {
                Destroy(gameObject);
            }            
            if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                StartCoroutine(Death());
            }
            else if(gameObject.layer == 10)
            {
                
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator Death()
    {
        gameObject.layer = 10;
        agent.velocity = Vector3.zero;
        agent.updatePosition = false;
        agent.updateRotation = false;

        _animator.SetTrigger("IsDeath");
        _animator.SetBool("EnemyinRange", false);
        _animator.SetBool("MovetoAttack", false);
        _animator.SetBool("Alive", false);
        yield return new WaitForSecondsRealtime(3.0f);
        Destroy(gameObject);
    }

    public void HitDamage(float damage)
    {
        _amount = amount - damage;
        amount -= damage;
    }
    void hit()
    {
        if(gameObject.layer==LayerMask.NameToLayer("Player") || gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            hurtaudioSource.clip = hurt_clip;
            hurtaudioSource.Play();
            _animator.SetTrigger("IsHit");
        }
    }
}
