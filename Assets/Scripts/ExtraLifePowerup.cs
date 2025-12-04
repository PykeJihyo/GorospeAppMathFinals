using UnityEngine;


public class ExtraLifePowerup : MonoBehaviour
{
    public void Apply(PlayerController player)
    {
        player.lives += 1; // give an extra life
        Destroy(gameObject); // remove the powerup from scene
    }
}