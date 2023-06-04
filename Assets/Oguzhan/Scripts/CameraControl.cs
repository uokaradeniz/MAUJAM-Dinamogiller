using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class CameraControl : MonoBehaviour
{
    public float smoothingFactor;
    private Transform player;
    public Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, smoothingFactor * Time.deltaTime);
        
    }
}
