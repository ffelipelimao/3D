using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float timeToDestroy = 2f;
    public int amountDamage = 1;
    public float speed = 50f;
    public List<string> tagsToHit;

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
        foreach (var item in tagsToHit)
        {
            if (collision.transform.tag == item)
            {
                var damageable = collision.transform.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    Vector3 dir = collision.transform.position - transform.position;
                    dir = -dir.normalized;
                    dir.y = 0;
                    damageable.Damage(amountDamage, dir);
                }
                break;
            }
        }
        Destroy(gameObject);
    }
}
