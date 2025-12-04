using UnityEngine;


public class Fireball : MonoBehaviour
{
    public float speed = 10f; 
    public float lifetime = 3f; 


    private Vector3 direction; 


    public void Initialize(Vector3 dir)
    {
        direction = dir.normalized;
    }


    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;


        lifetime -= Time.deltaTime;
        if (lifetime <= 0f)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.GetComponent<Enemy>();



        if (!other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}