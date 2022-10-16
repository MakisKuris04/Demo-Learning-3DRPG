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
    public float enemyAlertRange;
    public float enemyAttackRange;
    public bool isGuard;
    
    private EnemyStates enemyStates;
    private NavMeshAgent agent;
    private Animator animaOfEnemy;
    private GameObject targetToAttack;
    private Vector3 originalPosition;
    private bool foundTarget;
    private bool isAttacking;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animaOfEnemy = GetComponent<Animator>();
    }

    private void Start()
    {
        originalPosition = this.transform.position;
    }

    private void Update()
    {
        ChangeEnemyStates();
        SyncAnimaParameters();
    }

    
    /// <summary>
    /// ÐÞ¸ÄµÐÈË×´Ì¬
    /// </summary>
    private void ChangeEnemyStates()
    {
        if (TargetInAlertRange())
        {
            enemyStates = EnemyStates.Chase;
        }

        switch (enemyStates)
        {
            case EnemyStates.Guard:
            {
                break;
            }
            
            case EnemyStates.Patrol:
            {
                break;
            }
            
            case EnemyStates.Chase:
            {
                ActionInChase();
                break;
            }
            
            case EnemyStates.Dead:
            {
                break;
            }
        }
    }
    
    private void ActionInChase()
    {
        agent.isStopped = false;
        agent.destination = targetToAttack.transform.position;
        
        foundTarget = TargetInAlertRange();
        isAttacking = TargetInAttackRange();
    }
    
    private bool TargetInAlertRange()
    {
        var collidersInRange = Physics.OverlapSphere(transform.position, enemyAlertRange);

        foreach (var target in collidersInRange)
        {
            if (target.CompareTag("Player"))
            {
                targetToAttack = target.gameObject;
                return true;
            }
        }

        agent.isStopped = true;
        return false;
    }
    
    private bool TargetInAttackRange()
    {
        if (Vector3.Distance(targetToAttack.transform.position, this.transform.position) <= enemyAttackRange)
        {
            agent.isStopped = true;
            return true;
        }
        
        return false;
    }
    
    private void SyncAnimaParameters()
    {
        animaOfEnemy.SetBool("FoundTarget",foundTarget);
        animaOfEnemy.SetBool("Attack", isAttacking);
    }
}
