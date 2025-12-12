using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // Rigidbody attached to this player.
    private Rigidbody rb;

    // Reference to the main camera, used for centering when this player is active.
    private Camera mainCamera;

    // Movement input values (X and Y axes).
    private float movementX;
    private float movementY;

    // Movement speed and jump force for this player.
    public float speed = 0;
    public float jumpForce = 0;

    // Indicates if this player is allowed to jump (touching the ground).
    private bool canJump = false;

    // Indicates if this player is currently controlled by the user.
    private bool isActive = false;

    private bool isAtExit = false;

    // Called by the manager to initialize this child controller.
    public void InitAsChild(Camera cam)
    {
        rb = GetComponent<Rigidbody>();
        mainCamera = cam;
    }

    // Called by the manager to enable or disable control of this player.
    public void SetActive(bool value)
    {
        isActive = value;
    }

    // Allows the manager to access this player's Rigidbody.
    public Rigidbody GetRigidbody()
    {
        return rb;
    }

    // Called by the manager when Move input is received.
    public void OnMoveInput(InputValue movementValue)
    {
        if (!isActive) return;

        // Read the movement vector from input (X and Y).
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // Called by the manager when Jump input is received.
    public void OnJumpInput(InputValue jumpValue)
    {
        if (!isActive) return;

        if (canJump && rb != null)
        {
            // // Reset vertical velocity before applying jump force.
            // Vector3 v = rb.linearVelocity;
            // v.y = 0;
            // rb.linearVelocity = v;

            // Apply an impulse upwards for the jump.
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
        }
    }

    // Physics update for this player.
    void FixedUpdate()
    {
        if (!isActive || rb == null) return;

        // Build a movement vector along X and Y axes.
        Vector3 movement = new Vector3(movementX, movementY, 0.0f);

        // Apply force to move the player.
        rb.AddForce(movement * speed);
    }

    // Called by Unity when this player collides with another collider.
    void OnCollisionEnter(Collision collision)
    {
        // Only allow jumping again when touching the ground.
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Player"))
        {
            Vector3 collisionNormal = collision.contacts[0].normal;
            if (collisionNormal.y > 0)
            {
                canJump = true;
            }
        }
    }

    // Check if player is at his exit
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "exit" + gameObject.name)
        {
            Debug.Log("Player " + gameObject.name + " has reached their exit");
            isAtExit = true;
            // Use the newer API and guard against null to avoid deprecation and NRE
            var manager = UnityEngine.Object.FindFirstObjectByType<PlayersManager>();
            if (manager != null)
                manager.NotifyPlayerAtExit();
        }
    }

    // Check if not anymore touching the exit
    void OnTriggerExit(Collider other)
    {
        if (other.name == "exit" + gameObject.name)
        {
            Debug.Log("Player " + gameObject.name + " has left their exit");
            isAtExit = false;
        }
    }

    public bool IsAtExit()
    {
        return isAtExit;
    }
}
