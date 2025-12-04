using UnityEngine;

public class PlayerInvincibilityController : MonoBehaviour
{
    public bool isInvincible = false;

    private float timer = 0f;
    private float duration = 0f;

    private void Update()
    {
        if (!isInvincible) return;

        timer += Time.deltaTime;
        if (timer >= duration)
        {
            isInvincible = false;
            timer = 0;
        }
    }

    public void ActivateInvincibility(float d)
    {
        duration = d;
        timer = 0;
        isInvincible = true;
        Debug.Log("Player invincible for " + d + " seconds!");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isInvincible) return;

        if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.collider.gameObject);
            Debug.Log("Enemy destroyed by invincible player!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isInvincible) return;

        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            Debug.Log("Enemy destroyed by invincible player (trigger)!");
        }
    }
}