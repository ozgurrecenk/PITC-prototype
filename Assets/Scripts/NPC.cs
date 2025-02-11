using System;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour, IInteractable
{
    public Rigidbody[] rigidbodies;
    NavMeshAgent agent;
    GameObject player;
    Animator animator;

    bool isFollow;
    
    public enum MissionType{
        Follow,
        Shop,
        Information,
        Ragdoll
    }

    public MissionType mTypes;

    private void Start()
    {
        foreach (Rigidbody rb in rigidbodies)
        {
            rb.isKinematic = true;
        }
        
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isFollow)
        {
            if (agent.remainingDistance >= agent.stoppingDistance)
            {
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
            agent.SetDestination(player.transform.position);    
        }
        else
        {
            agent.SetDestination(transform.position);
        }
    }

    public void Interact()
    {
        if (mTypes == MissionType.Follow)
        {
            if (isFollow)
            {
                isFollow = false;
            }
            else
            {
                isFollow = true;
            }
        }
        else if (mTypes == MissionType.Shop)
        {
            Debug.Log("shop thing");
        }
        else if (mTypes == MissionType.Information)
        {
            Debug.Log("information thing");
        }
        else if (mTypes == MissionType.Ragdoll)
        {
            foreach (Rigidbody rb in rigidbodies)
            {
                rb.isKinematic = false;
                agent.enabled = false;
                animator.enabled = false;
                this.enabled = false;
            }
        }
    }
}
