using UnityEngine;

public class DiagonalMeteor : MonoBehaviour
{
    public float speed = 30f;
    public float damage = 10000f;
    public float lifetime = 5f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // forward•ûŒü‚ÉˆÚ“®
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    void OnCollisionEnter(Collision collision)
    {
        TryDamage(collision.collider.gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        TryDamage(other.gameObject);
    }

    void TryDamage(GameObject obj)
    {
        if (obj.CompareTag("Enemy"))
        {
            obj.GetComponent<EnemyHealth>()?.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
