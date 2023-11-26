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
        //_animator = GetComponentInParent<Animator>(); // 적에게 할당된 Animator를 가져옴
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

    }
    private void GoToFortress()
    {

    }
    private void GoToCastle()
    {

    }
    private void AttackFortress()
    {

    }
    private void AttackCastle()
    {

    }
    private void ChaseEnemy()
    {

    }
    private void AttackEnemy()
    {
        
    }
}
