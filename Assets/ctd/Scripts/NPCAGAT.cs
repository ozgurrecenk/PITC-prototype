using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;


public class NPCAGAT : MonoBehaviour, IInteractable
{
    public Collider col;
    public Rigidbody[] rb;
    public MonoBehaviour[] scripts;
    public Transform player;
    public NavMeshAgent agent;
    public Animator animator;
    public GameObject gui, ragdollGO;
    public bool walk;
    public int faceLevel = 1;

    public GameObject[] faces;
    
    private void Start()
    {
        col.enabled = true;
        foreach (Rigidbody rb in rb)
        {
            rb.isKinematic = true;
        }
    }

    void Update()
    {
        if (faceLevel > 3)
        {
            faceLevel = 1;
        }

        if (faceLevel == 1)
        {
            faces[1].SetActive(true);
            faces[2].SetActive(false);
            faces[3].SetActive(false);
        }
        else if (faceLevel == 2)
        {
            faces[1].SetActive(false);
            faces[2].SetActive(true);
            faces[3].SetActive(false);
        }
        else if (faceLevel == 3)
        {
            faces[1].SetActive(false);
            faces[2].SetActive(false);
            faces[3].SetActive(true);
        }
        
        if (walk)
        {
            Debug.Log(agent.stoppingDistance);
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                animator.SetBool("walk", false);
            }
            else
            {
                animator.SetBool("walk", true);
            }
            
            agent.SetDestination(player.position);
        }
        else
        {
            animator.SetBool("walk", false);
        }
    }

    public void Interact()
    {
        foreach (var script in scripts)
        {
            script.enabled = false;
        }
        Time.timeScale = 0;
        gui.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void follow()
    {
        walk = true;
    }

    public void cry()
    {
        walk = false;
        animator.SetTrigger("cry");
    }

    public void flip()
    {
        walk = false;
        animator.SetTrigger("flip");
    }

    public void ragdoll()
    {
        col.enabled = false;
        this.enabled = false;
        animator.enabled = false;
        foreach (Rigidbody rb in rb)
        {
            rb.isKinematic = false;
        }
    }
}
