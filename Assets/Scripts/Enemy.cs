using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    public bool startMovingRight = true;

    public float gravity = -9.81f;
    public float groundCheckDistance = 0.1f;

    private Vector3 startPos;
    private int dir = 1;
    private float velocityY = 0f;

    void Start()
    {
        startPos = transform.position;
        dir = startMovingRight ? 1 : -1;
    }

    void Update()
    {
        float dt = Time.deltaTime;

        velocityY += gravity * dt;

        float newX = transform.position.x + dir * moveSpeed * dt;
        float newY = transform.position.y + velocityY * dt;

        Vector3 newPos = new Vector3(newX, newY, transform.position.z);

        transform.position = newPos;

        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance))
        {
            velocityY = 0;
        }

        float dist = Mathf.Abs(transform.position.x - startPos.x);
        if (dist >= moveDistance)
        {
            dir *= -1;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.collider.CompareTag("Player") &&
            !collision.collider.CompareTag("Ground"))
        {
            dir *= -1;
        }
    }
}