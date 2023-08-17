using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatheringSwarm : BaseObject
{
    public Vector3 startPosition;
    [SerializeField] public float timeChangeDirect;
    [SerializeField] public float distance;
    private float timeChangeDirectStart;
    protected override void Start()
    {
        base.Start();

        startPosition = transform.position;
        timeChangeDirectStart = 0f;
    }
    public override void Move()
    {
        if(timeChangeDirectStart > timeChangeDirect)
        {
            Vector3 temp = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

            velocity = temp;
            timeChangeDirectStart = 0f;
        }
        else timeChangeDirectStart += Time.deltaTime;
        base.Move();

        if(Vector3.Distance(startPosition, transform.position) > distance)
        {
            Vector3 temp = startPosition - transform.position;
            velocity = temp.normalized;

            timeChangeDirectStart = 0f;
        }
    }
}
