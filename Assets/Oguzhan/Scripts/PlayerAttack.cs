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
    public float overheatTimer = 5;
    public int speedUpCounter;
    private bool spedUp;
    public float overheatCDR;

    public bool overheatControl;

    // Start is called before the first frame update
    void Start()
    {
        playerControl = GetComponent<PlayerControl>();
        attackPivot = transform.Find("PlayerMesh/AttackPivot");
        animator = GetComponentInChildren<Animator>();
    }

    void StopOverheat()
    {
        playerControl.moveSpeed = playerControl.runSpeed;
        playerControl.gameHandler.overheatText.text = "";
        speedUpCounter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        overheatCDR += Time.deltaTime;
        if (overheatCDR >= 20)
        {
            overheatCDR = 0;
            speedUpCounter = 0;
        }

        if (speedUpCounter >= 2)
        {
            overheatTimer -= Time.deltaTime;
            playerControl.moveSpeed = 1;
            playerControl.gameHandler.overheatText.text = "OVERHEAT! " + Mathf.Round(overheatTimer);

            if (overheatTimer <= 0)
            {
                StopOverheat();
                overheatTimer = 5;
            }
        }

        if (!playerControl.gameHandler.wonGame)
        {
            Collider[] humans = Physics.OverlapSphere(attackPivot.position, attackRange, layerMask);

            if (score >= 20 && Input.GetKeyDown(KeyCode.Q) && !spedUp)
            {
                SpeedUp();
                if (!overheatControl)
                {
                    if (speedUpCounter == 0)
                        overheatCDR = 0;
                    speedUpCounter++;
                    overheatControl = true;
                }
            }

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
    }

    void PullTime()
    {
        human = null;
        pullHuman = false;
    }

    public void StopSpeedUp()
    {
        playerControl.moveSpeed = playerControl.runSpeed;
        overheatControl = false;
        spedUp = false;
    }

    public void SpeedUp()
    {
        spedUp = true;
        score -= 20;
        playerControl.moveSpeed = speedupSpeed;
        Invoke("StopSpeedUp", 5);
    }

    void Attack(Collider collider)
    {
        Instantiate(Resources.Load("Droplet_ps"), collider.transform.position, Quaternion.identity);
        Destroy(collider.gameObject, .5f);
        score += 5;
        isAttacking = true;
    }

    void StopAttacking()
    {
        isAttacking = false;
    }
}