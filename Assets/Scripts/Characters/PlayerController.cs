using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;


public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animaOfCharacter;
    private GameObject EnemyToAttack;
    private float attackCooldown;

    public float attackRange;

    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animaOfCharacter = GetComponent<Animator>();
    }
    
    private void Start()
    {
        MouseManager.Instance.OnMouseClicked += EventMove;
        MouseManager.Instance.OnEnemyClicked += EventAttack;
    }
    
    private void Update()
    {
        animaOfCharacter.SetFloat("speed", agent.velocity.sqrMagnitude);
        AttackCountDown();
    }

    //Move
    private void EventMove(Vector3 target)
    {
        StopAllCoroutines();
        agent.isStopped = false;
        agent.destination = target;
    }

    //Attack
    private void EventAttack(GameObject target)
    {
        if(null == target)
            return;

        EnemyToAttack = target;
        StartCoroutine(MoveToAttackEnemy());
    }

    IEnumerator MoveToAttackEnemy()
    {
        agent.isStopped = false;

        transform.LookAt(EnemyToAttack.transform);

        while (Vector3.Distance(EnemyToAttack.transform.position, transform.position) > attackRange)
        {
            agent.destination = EnemyToAttack.transform.position;
            yield return null;
        }

        agent.isStopped = true;

        if (attackCooldown < 0)
        {
            attackCooldown = 0.5f;
            animaOfCharacter.SetTrigger("Attack");
        }
    }

    private void AttackCountDown()
    {
        attackCooldown -= Time.deltaTime;
    }

}
