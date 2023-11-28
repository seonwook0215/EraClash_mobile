using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static EnemyFSM;

public class Unit_FSM : MonoBehaviour
{
    public enum UnitState { Idle, GoToFortress, GoToCastle, AttackFortress, AttackCastle, ChaseEnemy, AttackEnemy }
    public UnitState currentState;

    public Transform Fortress_pos;
    public Transform Castle_pos;

    private Sight sightSensor;
    public float playerAttackDistance;
    private NavMeshAgent agent;
    private Animator _animator;
    private int i=0;

    private void Awake()
    {
        sightSensor = GetComponentInParent<Sight>();
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponentInParent<Animator>(); // ������ �Ҵ�� Animator�� ������
    }

    private void Update()
    {
        if (currentState == UnitState.Idle)
        {
            Idle();
        }
        else if (currentState == UnitState.GoToFortress)
        {
            GoToFortress();
        }
        else if (currentState == UnitState.GoToCastle)
        {
            GoToCastle();
        }
        else if (currentState == UnitState.AttackFortress)
        {
            AttackFortress();
        }
        else if (currentState == UnitState.AttackCastle)
        {
            AttackCastle();
        }
        else if (currentState == UnitState.ChaseEnemy)
        {
            ChaseEnemy();
        }
        else
        {
            AttackEnemy();
        }
    }

    private void Idle()
    {
        agent.isStopped = true;
        StartCoroutine(MovetoAttack());
    }
    private void GoToFortress()
    {
        agent.isStopped = false;
    }
    private void GoToCastle()
    {
        agent.isStopped = false;
    }
    private void AttackFortress()
    {
        agent.isStopped = false;
    }
    private void AttackCastle()
    {
        agent.isStopped = false;
    }
    private void ChaseEnemy()
    {
        agent.isStopped = false;
        //if (sightSensor.detectedObject == null)
        //{
        //    currentState = UnitState.GoToFortress; //���߿� ��,���� Ȯ���ؼ� ���ľ���
        //    return;
        //}
        agent.SetDestination(sightSensor.detectedObject.transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer <= playerAttackDistance)
        {
            currentState = UnitState.AttackEnemy;
            return;
        }
        //�ִϸ��̼� ������Ʈ
        UpdateAnimatorMovement();
    }
    private void AttackEnemy()
    {
        agent.isStopped = true;
        //if (sightSensor.detectedObject == null)
        //{
        //    currentState = UnitState.GoToFortress; //���߿� ��,���� Ȯ���ؼ� ���ľ���
        //    return;
        //}

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = UnitState.ChaseEnemy;
        }


        // �ִϸ��̼� ������Ʈ
        _animator.SetBool("EnemyinRange", true);
    }

    private void UpdateAnimatorMovement()
    {
        //float verticalInput = agent.velocity.normalized.z; // Y������ �̵��ϴ� �ӵ�
        float horizontalInput = agent.velocity.normalized.x; // X������ �̵��ϴ� �ӵ�
        _animator.SetFloat("yDir", horizontalInput, 0.1f, Time.deltaTime);
    }

    IEnumerator MovetoAttack()
    {
        yield return new WaitForSecondsRealtime(0.0f);
        _animator.SetBool("MovetoAttack", true);
        currentState = UnitState.ChaseEnemy;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
    }
}
