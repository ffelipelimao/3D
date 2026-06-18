using UnityEngine;

public class ItemCollectableBase : MonoBehaviour
{
    public string compareTag = "Player";
    public ParticleSystem particlePrefab;
    public float timeToHide = 6f;
    public GameObject graphicItem;
    public CollectableType itemType;

    [Header("Sound")]
    public AudioSource audioSource;
    public Collider coll;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.CompareTag(compareTag))
        {
            Collect();
        }
    }
    protected virtual void Collect()
    {
        if (coll != null)
        {
            coll.enabled = false;
        }
        if (graphicItem != null)
        {
            graphicItem.SetActive(false);
        }
        Invoke("HideObject", timeToHide);
        OnCollect();
    }

    private void HideObject()
    {
        gameObject.SetActive(false);
    }

    protected virtual void OnCollect()
    {
        if (particlePrefab != null)
        {
            ParticleSystem particle = Instantiate(
                particlePrefab,
                transform.position,
                Quaternion.identity
            );

            particle.Play();
        }

        if (audioSource != null)
        {
            audioSource.Play();
        }

        CollectableManager.Instance.AddByType(itemType);
    }
}
