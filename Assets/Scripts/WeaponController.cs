using System;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public CharacterController controller;
    public Animator anim;
    public bool canAttack = false;
    public bool ready = false;
    public LayerMask layer;
    
    public Transform InteractorSource;
    public float InteractRange;
    public float attackForce;

    private float horizontal, vertical;
    public GameObject weapon, arms;
    public Transform weaponPos;
    private Rigidbody rb;
    private Collider colliderW;
    
    private void Start()
    {
        arms.SetActive(false);
        anim.enabled = false;
        canAttack = false;
    }

    private void OnEnable()
    {
        readyV();
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if (weapon != null)
        {
            ready = true;
        }
        
        if (ready)
        {
            colliderW = weapon.GetComponent<Collider>();
            arms.SetActive(true);
            weapon.transform.position = weaponPos.position;
            weapon.transform.rotation = weaponPos.rotation;
            
            rb = weapon.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            colliderW.isTrigger = true;

            
            
            anim.enabled = true;
            canAttack = true;

            if (Input.GetKey(KeyCode.Q) && ready)
            {
               anim.SetTrigger("drop");
               ready = false;
               
               rb.isKinematic = false;
               colliderW.isTrigger = false;
               weapon = null;
               arms.SetActive(false);
               this.enabled = false;
            }
        }
        
        if (!controller.isGrounded && ready)
        {
            anim.SetBool("air", true);
            canAttack = false;
        }
        else
        {
            anim.SetBool("air", false);
            canAttack = true;
        }

        if (horizontal == 1 || vertical == 1 || horizontal == -1 || vertical == -1 && ready)
        {
            anim.SetBool("walk", true);
        }
        else
        {
            anim.SetBool("walk", false);
        }

        if (Input.GetMouseButton(0) && canAttack && ready)
        {
            Attack();
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack1");
        canAttack = false;
    }

    void readyV()
    {
        anim.SetTrigger("ready");
    }

    public void AttackEffect()
    {
        Ray ray = new Ray(InteractorSource.position, InteractorSource.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, InteractRange, layer))
        {
            Transform target = hit.collider.transform;
                
            Rigidbody rigidbody = target.GetComponentInParent<Rigidbody>();
             
            Debug.Log(rigidbody + "||" + target);   
            if (rigidbody != null)
            {
                rigidbody.AddForce(-hit.normal * attackForce, ForceMode.Impulse);
            }

            if (hit.collider != null)
            {
                anim.SetTrigger("GetUp");
            }
        }
    }
    
    public void ResetAttack()
    {
        canAttack = true;
    }
}
