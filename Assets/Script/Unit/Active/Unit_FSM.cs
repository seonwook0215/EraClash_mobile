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

    public GameObject Fortress;
    public GameObject Castle;

    public Transform Fortress_pos;
    public Transform Castle_pos;

    private Sight sightSensor;
    public float playerAttackDistance;
    private NavMeshAgent agent;
    private Animator _animator;
    
    public float lastAttackTime;
    public float attackRate;

    public float damage;
    public LayerMask objectlayer;
    private bool isStart;
    private void Awake()
    {
        sightSensor = GetComponentInParent<Sight>();
        agent = GetComponentInParent<NavMeshAgent>();
        _animator = GetComponentInParent<Animator>(); // 적에게 할당된 Animator를 가져옴
    }

    private void Start()
    {
        Debug.Log("start");
        if (gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Fortress = GameObject.Find("Enemy Fortress pos");
            Castle = GameObject.Find("Enemy Castle pos");
            Fortress_pos = Fortress.transform;
            Castle_pos = Castle.transform;
        }
        else if (gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            Fortress = GameObject.Find("Player Fortress pos");
            Castle = GameObject.Find("Player Castle pos");
            Fortress_pos = Fortress.transform;
            Castle_pos = Castle.transform;
        }
        else
        {
            Debug.Log("123");
        }
        isStart = true;
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
        if (isStart)
        {
            StartCoroutine(MovetoAttack());
        }
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
        if (sightSensor.detectedObject != null)
        {
            _animator.SetBool("MovetoAttack", true);
            _animator.SetBool("EnemyinRange", true);
            currentState = UnitState.ChaseEnemy;
            return;
        }
        agent.SetDestination(Fortress_pos.position);
        float distanceToPlayer = Vector3.Distance(transform.position, Fortress_pos.position);

        if (distanceToPlayer <= playerAttackDistance)
        {
            _animator.SetBool("MovetoAttack", false);
            _animator.SetBool("EnemyinRange", true);
            currentState = UnitState.AttackFortress;
            return;
        }
    }
    private void GoToCastle()
    {
        agent.isStopped = false;
        if (sightSensor.detectedObject != null)
        {
            _animator.SetBool("MovetoAttack", true);
            _animator.SetBool("EnemyinRange", true);
            currentState = UnitState.ChaseEnemy;
            return;
        }
        agent.SetDestination(Castle_pos.position);
        float distanceToPlayer = Vector3.Distance(transform.position, Castle_pos.position);

        if (distanceToPlayer <= playerAttackDistance)
        {
            _animator.SetBool("MovetoAttack", false);
            _animator.SetBool("EnemyinRange", true);
            currentState = UnitState.AttackCastle;
            return;
        }
    }
    private void AttackFortress()
    {
        agent.isStopped = false;
        if (sightSensor.detectedObject != null)
        {
            _animator.SetBool("MovetoAttack", true);
            _animator.SetBool("EnemyinRange", true);
            currentState = UnitState.ChaseEnemy;
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, Fortress_pos.position);

        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            _animator.SetBool("MovetoAttack", true);
            _animator.SetBool("EnemyinRange", false);
            currentState = UnitState.GoToFortress;
        }

        FortressAttack();
    }
    private void AttackCastle()
    {
        agent.isStopped = false;
        if (sightSensor.detectedObject != null)
        {
            _animator.SetBool("MovetoAttack", true);
            _animator.SetBool("EnemyinRange", true);
            currentState = UnitState.ChaseEnemy;
            return;
        }
        float distanceToPlayer = Vector3.Distance(transform.position, Castle_pos.position);

        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            _animator.SetBool("MovetoAttack", true);
            _animator.SetBool("EnemyinRange", false);
            currentState = UnitState.GoToCastle;
        }
    }
    private void ChaseEnemy()
    {
        agent.isStopped = false;
        if (sightSensor.detectedObject == null)
        {
            Debug.Log("no enemy");
            //평야에서 싸우는지 확인
            if (BattleManager.instance.inField)
            {
                _animator.SetBool("MovetoAttack", false);
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.Idle;
                return;
            }
            //기지 공격
            //currentState = UnitState.GoToFortress;
            //return;
            if (gameObject.layer == LayerMask.NameToLayer("Player") && PUnitManager.instance.fortress)
            {
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.GoToFortress;
                return;
            }
            else if (gameObject.layer == LayerMask.NameToLayer("Player") && !PUnitManager.instance.fortress)
            {
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.GoToCastle;
                return;
            }
            else if (gameObject.layer == LayerMask.NameToLayer("Enemy") && EUnitManager.instance.fortress)
            {
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.GoToFortress;
                return;
            }
            else if (gameObject.layer == LayerMask.NameToLayer("Enemy") && !EUnitManager.instance.fortress)
            {
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.GoToCastle;
                return;
            }
        }

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
            //평야에서 싸우는지 확인
            if (BattleManager.instance.inField)
            {
                _animator.SetBool("MovetoAttack", false);
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.Idle;
                return;
            }
            //기지 공격
            if (gameObject.layer == LayerMask.NameToLayer("Player") && PUnitManager.instance.fortress)
            {
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.GoToFortress;
                return;
            }
            else if (gameObject.layer == LayerMask.NameToLayer("Player") && !PUnitManager.instance.fortress)
            {
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.GoToCastle;
                return;
            }
            else if (gameObject.layer == LayerMask.NameToLayer("Enemy") && EUnitManager.instance.fortress)
            {
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.GoToFortress;
                return;
            }
            else if (gameObject.layer == LayerMask.NameToLayer("Enemy") && !EUnitManager.instance.fortress)
            {
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.GoToCastle;
                return;
            }
        }


        float distanceToPlayer = Vector3.Distance(transform.position, sightSensor.detectedObject.transform.position);
        if (distanceToPlayer > playerAttackDistance * 1.1f)
        {
            _animator.SetBool("MovetoAttack", true);
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
            lastAttackTime = Time.time;
            _animator.SetTrigger("Attack");
            sightSensor.detectedObject.GetComponentInParent<Life>().HitDamage(damage);
        }
    }

    void FortressAttack()
    {
        var timeSinceLastAttack = Time.time - lastAttackTime;
        if (timeSinceLastAttack > attackRate)
        {
            lastAttackTime = Time.time;
            _animator.SetTrigger("Attack");
            Fortress.GetComponentInParent<Life>().HitDamage(damage);
        }
    }

}
