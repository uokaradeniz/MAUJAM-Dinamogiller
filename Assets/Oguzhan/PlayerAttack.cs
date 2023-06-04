using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Transform attackPivot;
    public float attackRange;
    public LayerMask layerMask;
    private Animator animator;
    public bool isAttacking;

    private Collider human;

    private bool pullHuman;
    // Start is called before the first frame update
    void Start()
    {
        attackPivot = transform.Find("PlayerMesh/AttackPivot");
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Attack") && !isAttacking)
        {
            animator.SetTrigger("Attack");
            Collider[] humans = Physics.OverlapSphere(attackPivot.position, attackRange, layerMask);

            foreach (var human in humans)
            {
                this.human = human;
                pullHuman = true;
                Invoke("PullTime",0.9f);
                Attack(human);
            }
        }
        
        if (pullHuman)
            human.transform.position = attackPivot.position;
    }

    void PullTime()
    {
        human = null;
        pullHuman = false;
    }
    
    void Attack(Collider collider)
    {
        Destroy(collider.gameObject,1f);
        
        isAttacking = true;
      
    }

    void StopAttacking()
    {
        isAttacking = false;
    }
}