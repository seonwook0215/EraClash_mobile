using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TestLife : MonoBehaviour
{
    public float amount;
    private Animator _animator;
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
    }
    private void Start()
    {
        amount = 100f;
    }
    private void Update()
    {
    }

    void hit()
    {
        Debug.Log("hit");
        _animator.SetTrigger("IsHit");
    }
}
