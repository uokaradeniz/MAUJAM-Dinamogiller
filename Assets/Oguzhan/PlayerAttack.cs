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
    public int score;

    private PlayerControl playerControl;
    public float speedupSpeed;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        attackPivot = transform.Find("PlayerMesh/AttackPivot");
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] humans = Physics.OverlapSphere(attackPivot.position, attackRange, layerMask);
        
        if(score >= 20 && Input.GetKeyDown(KeyCode.Q))
            SpeedUp();
        
        if (!isAttacking && Input.GetButtonDown("Attack"))
        {
            isAttacking = true;
            animator.SetTrigger("Attack");

            foreach (var human in humans)
            {
                this.human = human;
                pullHuman = true;
                Invoke("PullTime", 0.48f);
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

    public void StopSpeedUp()
    {
        playerControl.moveSpeed = playerControl.runSpeed;
    }
    public void SpeedUp()
    {
        score -= 20;
        playerControl.moveSpeed = speedupSpeed;
        Invoke("StopSpeedUp", 5);
    }

    void Attack(Collider collider)
    {
        Destroy(collider.gameObject, .5f);
        score += 5;
        isAttacking = true;
    }

    void StopAttacking()
    {
        isAttacking = false;
    }
}