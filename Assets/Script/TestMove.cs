using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator _animator;
    public GameObject enemy;
    bool move = false;
    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponentInParent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Idle());
    }

    // Update is called once per frame
    void Update()
    {
        if (move)
        {
            agent.SetDestination(enemy.transform.position);
            UpdateAnimatorMovement();
        }
    }

    IEnumerator Idle()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        //agent.isStopped = false;
        move = true;
        _animator.SetBool("MovetoAttack", true);
    }
    private void UpdateAnimatorMovement()
    {
        //float verticalInput = agent.velocity.normalized.z; // Y������ �̵��ϴ� �ӵ�
        float horizontalInput = agent.velocity.normalized.x; // X������ �̵��ϴ� �ӵ�

        _animator.SetFloat("yDir", horizontalInput, 0.1f, Time.deltaTime);

    }
}
