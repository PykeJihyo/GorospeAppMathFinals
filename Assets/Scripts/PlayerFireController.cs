using UnityEngine;
using System.Collections;


public class PlayerFireController : MonoBehaviour
{
    [Header("Fire settings")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireCooldown = 0.25f;
    public KeyCode fireKey = KeyCode.E;

    [Header("Powerup state")]
    public bool hasFireball = false;
    public float fireballDuration = 0f;

    private float lastFireTime = -999f;
    private Coroutine powerCoroutine;

    void Update()
    {
        if (hasFireball && Time.time - lastFireTime >= fireCooldown)
        {
            if (Input.GetKeyDown(fireKey) || Input.GetButtonDown("Fire1"))
            {
                TryFire();
            }
        }
    }

    public void GiveFireball(float duration)
    {
        if (powerCoroutine != null) StopCoroutine(powerCoroutine);
        powerCoroutine = StartCoroutine(PowerupTimer(duration));
    }

    private IEnumerator PowerupTimer(float duration)
    {
        hasFireball = true;
        fireballDuration = duration;
        float t = 0f;
        while (t < duration)
        {
            t += Time.deltaTime;
            yield return null;
        }
        hasFireball = false;
        powerCoroutine = null;
    }

    private void TryFire()
    {
        if (fireballPrefab == null || firePoint == null) return;

        lastFireTime = Time.time;

        Vector3 dir = DetermineFacingDirection();

        GameObject fb = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
        Fireball fbComp = fb.GetComponent<Fireball>();
        if (fbComp != null) fbComp.Init(dir);


        if (dir.sqrMagnitude > 0.0001f)
        {
            fb.transform.rotation = Quaternion.LookRotation(dir);
        }
    }

    private Vector3 DetermineFacingDirection()
    {

        Vector3 right = transform.right;

        float h = Input.GetAxisRaw("Horizontal"); // -1, 0, 1 usually
        if (Mathf.Abs(h) > 0.1f)
        {
            return SafeNormalize(new Vector3(h, 0f, 0f));
        }

        return SafeNormalize(transform.forward);
    }

    private Vector3 SafeNormalize(Vector3 v)
    {
        float mag = Mathf.Sqrt(v.x * v.x + v.y * v.y + v.z * v.z);
        if (mag > 1e-6f) return v / mag;
        return Vector3.forward;
    }
}