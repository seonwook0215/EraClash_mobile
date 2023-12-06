using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttack : MonoBehaviour
{
    private Sight sightSensor;
    private float lastAttackTime;
    private float attackRate;
    private float damage;
    private bool firstAttack;
    private void Awake()
    {
        sightSensor = GetComponentInParent<Sight>();
    }

    private void Start()
    {
        if (gameObject.tag == "Fortress")
        {
            damage = 20;
        }
        else if (gameObject.tag == "Castle")
        {
            damage = 40;
        }
        attackRate = 1.0f;
        firstAttack = true;
    }
    private void Update()
    {
        if (firstAttack)
        {

        }
        if(sightSensor.detectedObject != null)
        {
            Attack();
        }
    }
    void Attack()
    {
        var timeSinceLastAttack = Time.time - lastAttackTime;
        if (timeSinceLastAttack > attackRate)
        {
            lastAttackTime = Time.time;
            Debug.Log(damage);
            sightSensor.detectedObject.GetComponentInParent<Life>().HitDamage(damage);
        }
    }

    IEnumerator waitFirstAttack()
    {
        yield return new WaitForSeconds(6.0f);
        Debug.Log("First Attack");
        firstAttack = false;
    }
}
