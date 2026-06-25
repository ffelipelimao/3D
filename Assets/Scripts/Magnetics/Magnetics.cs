using UnityEngine;

public class Magnetics : MonoBehaviour
{
    public float dist = 0.2f;
    public float coinSpeed = 3f;

    void Update()
    {
        if (Vector3.Distance(transform.position, Player.Instance.transform.position) > dist)
        {
            coinSpeed++;
            transform.position = Vector3.MoveTowards(transform.position, Player.Instance.transform.position, Time.deltaTime * coinSpeed);
        }
    }
}
