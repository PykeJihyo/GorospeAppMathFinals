using UnityEngine;

public class FireballPowerup : MonoBehaviour
{
    public float duration = 10f;
    public string playerTag = "Player";

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        var fireController = other.GetComponent<PlayerFireController>();
        if (fireController != null)
        {
            fireController.GiveFireball(duration);
        }

        Destroy(gameObject);
    }
}