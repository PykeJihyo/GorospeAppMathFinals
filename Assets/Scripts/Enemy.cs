using UnityEngine;

public class Enemy : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController p = other.GetComponent<PlayerController>();
            if (p.isInvincible)
            {
                Die();
            }
        }

        if (other.GetComponent<Fireball>() != null)
        {
            Die();
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }
}