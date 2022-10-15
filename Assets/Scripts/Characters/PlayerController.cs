using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations;


public class PlayerController : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator animaOfCharacter;

    //private Vector3 destination;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animaOfCharacter = GetComponent<Animator>();
    }


    private void Start()
    {
        MouseManager.Instance.OnMouseClicked += MoveToTarget;//OnMouseClicked是参数V3的Action委托，通过+=注册（注册的方法必须参数相同）
    }

    private void Update()
    {
        //HaveAchievedToDestination(destination);
        animaOfCharacter.SetFloat("speed", agent.velocity.sqrMagnitude);//使用Animator中的Blend tree
    }

    private void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
        //destination = target;
        //animaOfCharacter.SetBool("isMoving", true);
    }
    //注册进OnMouseClicked中，OnMouseClicked调用时，连带调用注册在其中的该方法
}
