using UnityEngine;


public class InvinciblePowerup : MonoBehaviour
{
    public float duration = 5f;


    public void Apply(PlayerController player)
    {
        player.StartCoroutine(player.ActivateInvincibility(duration));
        Destroy(gameObject); // remove powerup from scene
    }
}