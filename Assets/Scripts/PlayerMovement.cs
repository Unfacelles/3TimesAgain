using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    public float stepHeight = 0.3f;
    public Transform stepRayUpper;
    public Transform stepRayLower;

    private Rigidbody rb;
    private bool isGrounded;
    [HideInInspector] public bool canMove = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!canMove)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            return;
        }

        float move = Input.GetAxis("Horizontal");
        rb.velocity = new Vector3(move * moveSpeed, rb.velocity.y, 0f);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

        StepClimb();
    }

    void StepClimb()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) < 0.01f) return;

        // This direction will be (-1, 0, 0) for left, (1, 0, 0) for right
        Vector3 direction = new Vector3(horizontalInput, 0f, 0f).normalized;

        // Cast rays from ray origins in the move direction
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.position, direction, out hitLower, 0.3f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.position, direction, out hitUpper, 0.35f))
            {
                float stepY = hitLower.point.y;
                float playerY = rb.position.y;

                float stepOffset = stepY - playerY;

                if (stepOffset > 0f && stepOffset <= stepHeight)
                {
                    // Try a stronger forward push
                    float forwardPushStrength = 0.7f; // tweakable

                    Vector3 upwardStep = new Vector3(0f, stepOffset, 0f);
                    Vector3 forwardStep = direction * forwardPushStrength;

                    rb.MovePosition(rb.position + upwardStep + forwardStep);
                }
            }
        }
    }




    void OnCollisionEnter(Collision collision)
    {
        if (collision.contacts[0].normal == Vector3.up)
        {
            isGrounded = true;
        }
    }

    // Optional: draw rays in Scene view for debugging
    void OnDrawGizmos()
    {
        if (stepRayLower && stepRayUpper)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(stepRayLower.position, transform.right * 0.2f);
            Gizmos.color = Color.green;
            Gizmos.DrawRay(stepRayUpper.position, transform.right * 0.3f);
        }
    }
}
