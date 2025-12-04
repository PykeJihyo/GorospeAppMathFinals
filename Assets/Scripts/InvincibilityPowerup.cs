using UnityEngine;

public class InvincibilityPowerup : MonoBehaviour
{
    public string playerTag = "Player";
    public float duration = 8f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        PlayerInvincibilityController inv = other.GetComponent<PlayerInvincibilityController>();

        if (inv != null)
        {
            inv.ActivateInvincibility(duration);
        }

        Destroy(gameObject);
    }
}