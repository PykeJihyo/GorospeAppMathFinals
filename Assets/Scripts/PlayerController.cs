using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float walkSpeed = 6f;
    public float airSpeedMultiplier = 0.5f;

    [Header("Jump")]
    public float jumpVelocity = 8f;
    public float gravity = -20f;
    public float ascendGravityMultiplier = 2.5f;
    public float descendGravityMultiplier = 0.6f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckDistance = 0.6f; // slightly larger to ensure detection
    public LayerMask groundLayers;

    private Vector3 velocity = Vector3.zero;
    private float verticalVelocity = 0f;
    private bool isGrounded = false;
    private float capsuleHeight;

    void Start()
    {
        if (groundCheck == null)
        {
            GameObject go = new GameObject("groundCheck");
            go.transform.SetParent(transform);
            go.transform.localPosition = new Vector3(0f, -0.5f, 0f);
            groundCheck = go.transform;
        }
        capsuleHeight = GetComponent<CapsuleCollider>() != null ? GetComponent<CapsuleCollider>().height : 2f;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        // Ground check
        isGrounded = Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, groundLayers);

        // Horizontal input
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");
        Vector3 input = new Vector3(inputX, 0f, inputZ);
        if (input.sqrMagnitude > 1f) input.Normalize();

        float horizSpeed = walkSpeed * (isGrounded ? 1f : airSpeedMultiplier);
        Vector3 horizVel = new Vector3(input.x * horizSpeed, 0f, input.z * horizSpeed);
        velocity.x = horizVel.x;
        velocity.z = horizVel.z;

        // Jump
        if (isGrounded && verticalVelocity <= 0f && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = jumpVelocity;
        }

        // Gravity
        float effectiveGravity = gravity;
        if (verticalVelocity > 0f) effectiveGravity *= ascendGravityMultiplier;
        else effectiveGravity *= descendGravityMultiplier;

        verticalVelocity += effectiveGravity * dt;

        // Prevent falling through ground
        if (isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = 0f;

            // Snap to ground
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, out hit, 2f, groundLayers))
            {
                Vector3 p = transform.position;
                p.y = hit.point.y + capsuleHeight / 2f;
                transform.position = p;
            }
        }

        velocity.y = verticalVelocity;
        transform.position += velocity * dt;
    }
}