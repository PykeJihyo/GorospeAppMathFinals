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
                Destroy(gameObject);
            }
        }

        if (other.GetComponent<Fireball>() != null)
        {
            Destroy(gameObject);
        }
    }
}