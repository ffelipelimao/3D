using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int amountDamage = 1;
    public float speed = 50f;

    void Awake()
    {
        Destroy(gameObject, timeToDestroy);
    }
    void Update()
    {
        transform.Translate(Vector3.forward * (Time.deltaTime * speed));
    }

    void OnCollisionEnter(Collision collision)
    {
        var damageable = collision.transform.GetComponent<IDamageable>();
        if (damageable != null) damageable.Damage(amountDamage);
        Destroy(gameObject);
    }
}
