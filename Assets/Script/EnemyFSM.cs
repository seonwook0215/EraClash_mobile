using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFSM : MonoBehaviour
{
    public enum EnemyState { GoToBase, AttackBase, ChasePlayer, AttackPlayer};

    public EnemyState currentState;

    private void Update()
    {
        if (currentState == EnemyState.GoToBase) { GoToBase(); }
        else if(currentState == EnemyState.AttackBase) { AttackBase(); }
        else if(currentState == EnemyState.ChasePlayer) { ChasePlayer(); }
        else { AttackPlayer(); }
    }

    void GoToBase() { print("GoToBase"); }
    void AttackBase() { print("AttackBase"); }
    void ChasePlayer() { print("ChasePlayer"); }
    void AttackPlayer() { print("AttackPlayer"); }
}
