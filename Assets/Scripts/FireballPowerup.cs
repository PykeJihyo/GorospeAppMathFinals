using UnityEngine;


public class FireballPowerup : MonoBehaviour
{
    public void Apply(PlayerController player)
    {
        player.fireballAmmo += 1; // Give player one extra fireball
        Destroy(gameObject); // Remove the powerup from the scene
    }
}