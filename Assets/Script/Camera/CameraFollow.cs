using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindObjectOfType<Player>().transform;
    }

    // Update is called once per frame

    private void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Slerp(transform.position, desiredPosition, speed * Time.deltaTime);

        smoothPosition.x = desiredPosition.x;
        transform.position = desiredPosition;
    }

    //private void FixedUpdate()
    //{
    //    Vector3 desiredPosition = target.position + offset;
    //    Vector3 smoothPosition = Vector3.Slerp(transform.position, desiredPosition, speed * Time.deltaTime);

    //    transform.position = smoothPosition;
    //}
}
