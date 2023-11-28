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
        _animator = GetComponentInParent<Animator>(); // ������ �Ҵ�� Animator�� ������
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
            currentState = UnitState.ChaseEnemy; //���߿� ��,���� Ȯ���ؼ� ���ľ���
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
            //��߿��� �ο���� Ȯ��
            if (BattleManager.instance.inField)
            {
                _animator.SetBool("MovetoAttack", false);
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.Idle;
                return;
            }
            //���� ����
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
        //�ִϸ��̼� ������Ʈ
        UpdateAnimatorMovement();
    }
    private void AttackEnemy()
    {
        agent.isStopped = true;
        if (sightSensor.detectedObject == null)
        {
            //��߿��� �ο���� Ȯ��
            if (BattleManager.instance.inField)
            {
                _animator.SetBool("MovetoAttack", false);
                _animator.SetBool("EnemyinRange", false);
                currentState = UnitState.Idle;
                return;
            }
            //���� ����
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


        // �ִϸ��̼� ������Ʈ
        Attack();
    }

    private void UpdateAnimatorMovement()
    {
        //float verticalInput = agent.velocity.normalized.z; // Y������ �̵��ϴ� �ӵ�
        float horizontalInput = agent.velocity.normalized.x; // X������ �̵��ϴ� �ӵ�
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
