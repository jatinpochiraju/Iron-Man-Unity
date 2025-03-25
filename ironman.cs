using UnityEngine;

public class IronManController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float flightSpeed = 10f;
    public float rotationSpeed = 2f;
    public float liftForce = 5f; // Vertical thrust when flying

    private Rigidbody rb;
    private bool isFlying = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true; // Start with gravity for walking mode
    }

    void Update()
    {
        HandleMovement();
        HandleFlightToggle();
    }

    void HandleMovement()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.forward * moveZ + transform.right * moveX;
        moveDirection.Normalize();

        if (isFlying)
        {
            // Fly mode: No gravity, move in all directions
            rb.useGravity = false;
            rb.velocity = moveDirection * flightSpeed + Vector3.up * (Input.GetKey(KeyCode.Space) ? liftForce : 0);
        }
        else
        {
            // Walk mode: Apply gravity, move only on ground
            rb.useGravity = true;
            rb.velocity = new Vector3(moveDirection.x * walkSpeed, rb.velocity.y, moveDirection.z * walkSpeed);
        }

        // Rotate the character smoothly
        float rotateY = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(0, rotateY, 0);
    }

    void HandleFlightToggle()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlying = !isFlying;
            if (!isFlying)
            {
                rb.velocity = Vector3.zero; // Stop flying immediately
            }
        }
    }
}
