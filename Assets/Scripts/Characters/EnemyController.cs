using System;
using System.Collections;
using System.Collections.Generic;using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;


public enum EnemyStates
{
    Guard, Patrol, Chase, Dead
}
[RequireComponent(typeof(NavMeshAgent))]


public class EnemyController : MonoBehaviour
{
    public float enemyAttackRange;
    
    private NavMeshAgent agent;
    private EnemyStates enemyStates;

    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    
    private void Update()
    {
        ChangeEnemyStates();
    }

    
    /// <summary>
    /// ÐÞ¸ÄµÐÈË×´Ì¬
    /// </summary>
    private void ChangeEnemyStates()
    {
        if (PlayerInAttackRange())
        {
            enemyStates = EnemyStates.Chase;
        }

        switch (enemyStates)
        {
            case EnemyStates.Guard:
            {
                break;
            }

            case EnemyStates.Chase:
            {
                break;
            }

            case EnemyStates.Patrol:
            {
                break;
            }

            case EnemyStates.Dead:
            {
                Destroy(this);
                break;
            }
        }
    }

    private bool PlayerInAttackRange()
    {
        var collidersInRange = Physics.OverlapSphere(transform.position, enemyAttackRange);

        foreach (var target in collidersInRange)
        {
            if (target.CompareTag("Player"))
            {
                return true;
            }
        }

        return false;
    }
}
