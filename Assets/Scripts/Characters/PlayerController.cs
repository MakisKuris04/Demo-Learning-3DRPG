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
        MouseManager.Instance.OnMouseClicked += MoveToTarget;//OnMouseClicked�ǲ���V3��Actionί�У�ͨ��+=ע�ᣨע��ķ������������ͬ��
    }

    private void Update()
    {
        //HaveAchievedToDestination(destination);
        animaOfCharacter.SetFloat("speed", agent.velocity.sqrMagnitude);//ʹ��Animator�е�Blend tree
    }

    private void MoveToTarget(Vector3 target)
    {
        agent.destination = target;
        //destination = target;
        //animaOfCharacter.SetBool("isMoving", true);
    }
    //ע���OnMouseClicked�У�OnMouseClicked����ʱ����������ע�������еĸ÷���
}
