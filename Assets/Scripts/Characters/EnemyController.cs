using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Guard,
    Patrol,
    Chase,
    Dead
}

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyController : MonoBehaviour
{
    public NavMeshAgent agent;
    public EnemyType enemyType;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
}
