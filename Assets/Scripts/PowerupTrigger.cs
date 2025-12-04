using UnityEngine;

public class PowerupTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IPowerup pow = GetComponent<IPowerup>();
            if (pow != null)
            {
                pow.Apply(other.GetComponent<PlayerController>());
            }
        }
    }
}