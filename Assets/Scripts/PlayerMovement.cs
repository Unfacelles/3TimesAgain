using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    public float stepHeight = 0.3f;
    public float stepSmooth = 0.1f;
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
        RaycastHit hitLower;
        if (Physics.Raycast(stepRayLower.position, transform.forward, out hitLower, 0.2f))
        {
            RaycastHit hitUpper;
            if (!Physics.Raycast(stepRayUpper.position, transform.forward, out hitUpper, 0.3f))
            {
                rb.position += new Vector3(0f, stepSmooth, 0f);
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
}
