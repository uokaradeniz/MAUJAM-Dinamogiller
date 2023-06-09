using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public bool spedUp;
    public float overheatCDR;

    public bool overheatControl;

    private AudioSource playerAudioSource;
    private AudioSource childAudioSource;

    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        childAudioSource = GetComponentInChildren<AudioSource>();
        playerAudioSource = GetComponent<AudioSource>();
        playerControl = GetComponent<PlayerControl>();
        attackPivot = transform.Find("PlayerMesh/AttackPivot");
        animator = GetComponentInChildren<Animator>();
    }

    void StopOverheat()
    {
        childAudioSource.Stop();
        playerControl.moveSpeed = playerControl.runSpeed;
        playerControl.gameHandler.overheatText.text = "";
        speedUpCounter = 0;
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControl.gameHandler.wonGame || !playerControl.gameHandler.lostGame)
        {
            if (score <= 0)
                score = 0;
            
            if (overheatCDR < 8 && speedUpCounter == 1)
                playerControl.gameHandler.overheatCDRText.text = "" + Mathf.Round(overheatCDR);
            else if (isPlaying)
            {
                playerControl.gameHandler.overheatCDRText.text = " Hot";
                playerControl.gameHandler.overheatCDRText.color = Color.yellow;
            }
            else
            {
                playerControl.gameHandler.overheatCDRText.text = " Cool";
                playerControl.gameHandler.overheatCDRText.color = Color.blue;
            }

            if (!overheatControl)
            {
                overheatCDR -= Time.deltaTime;
            }

            if (overheatCDR <= 0)
            {
                overheatCDR = 8;
                speedUpCounter = 0;
            }

            if (speedUpCounter >= 2)
            {
                overheatTimer -= Time.deltaTime;
                playerControl.moveSpeed = 1;
                playerControl.gameHandler.overheatText.text = "OVERHEAT! " + Mathf.Round(overheatTimer);
                if (!isPlaying)
                {
                    score -= 5;
                    childAudioSource.PlayOneShot((AudioClip)Resources.Load("engine"));
                    isPlaying = true;
                }

                if (overheatTimer <= 0)
                {
                    StopOverheat();
                    overheatTimer = 5;
                }
            }

            Collider[] humans = Physics.OverlapSphere(attackPivot.position, attackRange, layerMask);

            if (playerControl.gameHandler.gameTimer >= 5 && Input.GetKeyDown(KeyCode.Q) && !spedUp)
            {
                SpeedUp();
                if (!overheatControl)
                {
                    if (speedUpCounter == 0)
                        overheatCDR = 8;
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
            {
                human.transform.position = attackPivot.position;
                human.GetComponent<CapsuleCollider>().enabled = false;
            }
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
        childAudioSource.PlayOneShot((AudioClip)Resources.Load("EngineBoost"));
        spedUp = true;
        playerControl.gameHandler.gameTimer -= 8;
        playerControl.moveSpeed = speedupSpeed;
        Invoke("StopSpeedUp", 7);
    }

    void Attack(Collider collider)
    {
        Instantiate(Resources.Load("Droplet_ps"), collider.transform.position, Quaternion.identity);
        playerAudioSource.PlayOneShot((AudioClip)Resources.Load("blood"));
        Destroy(collider.gameObject, .5f);
        score += 5;
        isAttacking = true;
    }

    void StopAttacking()
    {
        isAttacking = false;
    }
}