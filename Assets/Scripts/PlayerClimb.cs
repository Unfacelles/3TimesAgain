using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public float climbSpeed = 2f;
    private PlayerMovement movement;
    private Rigidbody rb;
    private bool isClimbing = false;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Climbable") && Input.GetKey(KeyCode.Space))
        {
            if (!isClimbing)
            {
                isClimbing = true;
                movement.isClimbing = true;
                rb.useGravity = false;
            }

            rb.velocity = new Vector3(0f, climbSpeed, 0f);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Climbable"))
        {
            isClimbing = false;
            movement.isClimbing = false;
            rb.useGravity = true;
        }
    }
}
