using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
    private float vertical;
    private float horizontal;
    [Header("Movement")] public float runSpeed;
    [HideInInspector]public float moveSpeed;
    public float rotationSpeed;
    private Transform playerMesh;
    public float gravityForce;
    private CharacterController controller;
    private Animator animator;
    [HideInInspector]public GameHandler gameHandler;

// Start is called before the first frame update
    private void Start()
    {
        gameHandler = GameObject.Find("Game Handler").GetComponent<GameHandler>();
        animator = GetComponentInChildren<Animator>();
        playerMesh = transform.Find("PlayerMesh");
        controller = GetComponent<CharacterController>();
        moveSpeed = runSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        if (!gameHandler.wonGame)
        {
            vertical = Input.GetAxis("Vertical");
            horizontal = Input.GetAxis("Horizontal");
            Vector3 dir = Vector3.right * horizontal + Vector3.forward * vertical;
            CalculateRotation(dir);
            controller.Move(moveSpeed * Time.deltaTime * dir.normalized);
            controller.Move(Vector3.down * gravityForce);
        }
    }

    private void CalculateRotation(Vector3 direction)
    {
        if (vertical != 0 || horizontal != 0)
            playerMesh.transform.rotation = Quaternion.Slerp(playerMesh.transform.rotation,
                Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
    }
}