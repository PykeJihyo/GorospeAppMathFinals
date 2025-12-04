using UnityEngine;

public class LifePowerup : MonoBehaviour
{
    public string playerTag = "Player";
    public int lifeAmount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        PlayerLifeController life = other.GetComponent<PlayerLifeController>();
        if (life != null)
        {
            life.AddLife(lifeAmount);

            Destroy(gameObject);
        }

    }
}