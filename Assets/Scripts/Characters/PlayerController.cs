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

    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animaOfCharacter = GetComponent<Animator>();
    }


    private void Start()
    {
        MouseManager.Instance.OnMouseClicked += MoveToTarget;
        MouseManager.Instance.OnEnemyClicked += EventAttack;
    }
    
    
    private void Update()
    {
        animaOfCharacter.SetFloat("speed", agent.velocity.sqrMagnitude);//使用Animator中的Blend tree;
        MoveToAttackEnemy();
    }

    
    private void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
    }

    private void EventAttack(GameObject target)
    {
        if(null == target)
            return;

        EnemyToAttack = target;
    }

    IEnumerator MoveToAttackEnemy()
    {
        agent.isStopped = false;
        
        transform.LookAt(EnemyToAttack.transform);

        while (Vector3.Distance(EnemyToAttack.transform.position, transform.position) > 1)
        {
            agent.destination = EnemyToAttack.transform.position;
            yield return null;
        }

        agent.isStopped = true;
    }

}
