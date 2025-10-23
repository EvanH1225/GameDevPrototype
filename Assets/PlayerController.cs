using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float turnSpeed = 200f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float v = Input.GetAxis("Vertical");   // W/S or Up/Down

        Vector3 move = transform.forward * v + transform.right * h;
        rb.MovePosition(rb.position + move * moveSpeed * Time.deltaTime);

        // Simple mouse rotation
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(Vector3.up, mouseX * turnSpeed * Time.deltaTime);
    }
}
