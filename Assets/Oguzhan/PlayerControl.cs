
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private float vertical;
    private float horizontal;
    [Header("Movement")] public float moveSpeed;

    private CharacterController controller;
// Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        Vector3 dir = transform.right * horizontal + transform.forward * vertical;
        controller.Move(moveSpeed * Time.deltaTime * dir);
    }
}
