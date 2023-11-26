using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static EnemyFSM;

public class Unit_FSM : MonoBehaviour
{
    public enum UnitState { Idle, GoToFortress, GoToCastle, AttackFortress, AttackCastle, ChaseEnemy, AttackEnemy }
    public UnitState currentState;

    public Transform Fortress_pos;
    public Transform Castle_pos;

    public Sight sightSensor;
    public float baseAttackDistance;
    public float playerAttackDistance;
    private NavMeshAgent agent;
    private Animator _animator;
    private void Awake()
    {
        //agent = GetComponentInParent<NavMeshAgent>();
        //_animator = GetComponentInParent<Animator>(); // ������ �Ҵ�� Animator�� ������
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
        if (sightSensor.detectedObject == null)
        {
            currentState = UnitState.GoToFortress; //���߿� ��,���� Ȯ���ؼ� ���ľ���
            return;
        }
        agent.SetDestination(sightSensor.detectedObject.transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer <= playerAttackDistance)
        {
            currentState = UnitState.AttackEnemy;
        }

        //�ִϸ��̼� ������Ʈ
    }
    private void AttackEnemy()
    {
        agent.isStopped = false;

        if (sightSensor.detectedObject == null)
        {
            currentState = UnitState.GoToFortress; //���߿� ��,���� Ȯ���ؼ� ���ľ���
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            currentState = UnitState.ChaseEnemy;
        }


        // �ִϸ��̼� ������Ʈ
    }
}
