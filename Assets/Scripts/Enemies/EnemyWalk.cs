using UnityEngine;

public class EnemyWalk : EnemyBase
{

    public GameObject[] wayPoints;
    public float minDistance = 1f;

    public float speed = 0;

    private int _index = 0;

    public override void Update()
    {
        base.Update();
        if (Vector3.Distance(transform.position, wayPoints[_index].transform.position) < minDistance)
        {
            _index++;
            if (_index >= wayPoints.Length)
            {
                _index = 0;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, wayPoints[_index].transform.position, Time.deltaTime * speed);
        transform.LookAt(wayPoints[_index].transform.position);
    }

}
