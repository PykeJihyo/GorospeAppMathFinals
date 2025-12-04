using UnityEngine;


public class Fireball : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 3f;
    public float radius = 0.2f;
    public LayerMask hitLayers; 
    private Vector3 direction;
    private float alive = 0f;

    public void Init(Vector3 dir)
    {
        direction = SafeNormalize(dir);
    }

    void Update()
    {
        float dt = Time.deltaTime;
        Vector3 move = direction * speed * dt;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, radius, direction, out hit, move.magnitude, hitLayers, QueryTriggerInteraction.Ignore))
        {
            // hit something
            OnHit(hit.collider, hit.point, hit.normal);
            return;
        }

        transform.position += move;

        alive += dt;
        if (alive >= lifetime) Destroy(gameObject);
    }

    private void OnHit(Collider col, Vector3 point, Vector3 normal)
    {
        Destroy(gameObject);

    }

    private Vector3 SafeNormalize(Vector3 v)
    {
        float mag = Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        if (mag > 1e-6f) return v / mag;
        return Vector3.forward;
    }
}