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
    
    public float lastAttackTime;
    public float attackRate;

    private void Awake()
    {
        sightSensor = GetComponentInParent<Sight>();
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponentInParent<Animator>(); // 적에게 할당된 Animator를 가져옴
    }

    private void Start()
    {
        StartCoroutine(MovetoAttack());
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
        if (sightSensor.detectedObject != null)
        {
            _animator.SetBool("MovetoAttack", true);
            currentState = UnitState.ChaseEnemy; //나중에 성,성루 확인해서 고쳐야함
            return;
        }
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
            _animator.SetBool("MovetoAttack", false);
            _animator.SetBool("EnemyinRange", false);
            currentState = UnitState.Idle; //나중에 성,성루 확인해서 고쳐야함
            return;
        }
        //if (sightSensor.detectedObject == null)
        //{
        //    currentState = UnitState.GoToFortress; //나중에 성,성루 확인해서 고쳐야함
        //    return;
        //}
        agent.SetDestination(sightSensor.detectedObject.transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);

        if (distanceToPlayer <= playerAttackDistance)
        {
            _animator.SetBool("EnemyinRange", true);
            currentState = UnitState.AttackEnemy;
            return;
        }
        //애니메이션 업데이트
        UpdateAnimatorMovement();
    }
    private void AttackEnemy()
    {
        agent.isStopped = true;
        if (sightSensor.detectedObject == null)
        {
            _animator.SetBool("MovetoAttack", false);
            _animator.SetBool("EnemyinRange", false);
            currentState = UnitState.Idle; //나중에 성,성루 확인해서 고쳐야함
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            _animator.SetBool("EnemyinRange", false);
            currentState = UnitState.ChaseEnemy;
        }


        // 애니메이션 업데이트
        Attack();
    }

    private void UpdateAnimatorMovement()
    {
        //float verticalInput = agent.velocity.normalized.z; // Y축으로 이동하는 속도
        float horizontalInput = agent.velocity.normalized.x; // X축으로 이동하는 속도
        _animator.SetFloat("yDir", horizontalInput, 0.1f, Time.deltaTime);
    }

    IEnumerator MovetoAttack()
    {
        yield return new WaitForSecondsRealtime(5.0f);
        _animator.SetBool("MovetoAttack", true);
        currentState = UnitState.ChaseEnemy;
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawWireSphere(transform.position, playerAttackDistance);
    }

    void Attack()
    {
        var timeSinceLastAttack = Time.time - lastAttackTime;
        if(timeSinceLastAttack > attackRate)
        {
            Debug.Log("attack");
            lastAttackTime = Time.time;
            _animator.SetTrigger("Attack");
            sightSensor.detectedObject.GetComponentInParent<TestLife>()._amount -= 1.0f;
        }
    }

}
