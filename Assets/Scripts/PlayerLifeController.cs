using UnityEngine;

public class PlayerLifeController : MonoBehaviour
{

    [Header("Lives Settings")]
    public int lives = 3;
    public float hitCooldown = 1f;

    [Header("Enemy Contact")]
    public float enemyHitRange = 0.7f;

    private float lastHitTime = -999f;

    void Update()
    {
        HandleEnemyContact();
    }

    public void AddLife(int amount)
    {
        lives += amount;
        Debug.Log("Life added! Current lives: " + lives);
    }

    public void LoseLife(int amount)
    {
        lives -= amount;
        if (lives <= 0)
        {
            lives = 0;
            Debug.Log("Player died!");
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Life lost! Current lives: " + lives);
        }
    }


    private void HandleEnemyContact()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            Vector3 diff = transform.position - enemy.transform.position;
            float distance = Mathf.Sqrt(diff.x * diff.x + diff.y * diff.y);

            if (distance < enemyHitRange && Time.time - lastHitTime >= hitCooldown)
            {
                if (!IsInvincible())
                {
                    LoseLife(1);
                    lastHitTime = Time.time;
                    Debug.Log("Hit by enemy: " + enemy.name);
                }
            }
        }
    }

    public bool IsInvincible()
    {
        PlayerInvincibilityController inv = GetComponent<PlayerInvincibilityController>();
        return inv != null && inv.isInvincible;
    }
}