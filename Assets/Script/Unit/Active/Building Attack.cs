using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingAttack : MonoBehaviour
{
    private Sight sightSensor;
    private float lastAttackTime;
    private float attackRate;
    private float damage;
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
    }
    private void Update()
    {
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
            //Debug.Log("Building Attack");
            lastAttackTime = Time.time;
            sightSensor.detectedObject.GetComponentInParent<Life>().HitDamage(damage);
        }
    }
}
