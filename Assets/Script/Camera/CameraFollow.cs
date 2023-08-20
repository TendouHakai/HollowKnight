using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed;
    [SerializeField] Vector3 offset;

    // start Scene
    [Header("----------Start scene----------")]
    [SerializeField] Player playerFrefabs;
    [SerializeField] Vector3 playerPosStart;
    [SerializeField] PlayerControl playerControlFrefabs;
    [SerializeField] UIManager uIManagerFrefabs;
    [SerializeField] SoundManager soundManagerFrefabs;

    private void Awake()
    {
        //Player player = GameObject.FindObjectOfType<Player>();
        //if (target == null)
        //{
        //    player = Instantiate(playerFrefabs, playerPosStart, Quaternion.identity);
        //    target = player.transform;

        //    Instantiate(playerControlFrefabs, Vector3.zero, Quaternion.identity);
        //    Instantiate(uIManagerFrefabs, Vector3.zero, Quaternion.identity);
        //    Instantiate(soundManagerFrefabs, Vector3.zero, Quaternion.identity);
        //}
        //else
        //{
        //    target = player.transform;
        //}
    }
    void Start()
    {
        Player player = GameObject.FindObjectOfType<Player>();
        if (player == null)
        {
            player = Instantiate(playerFrefabs, playerPosStart, Quaternion.identity);
            target = player.transform;

            Instantiate(playerControlFrefabs, Vector3.zero, Quaternion.identity);
            Instantiate(uIManagerFrefabs, Vector3.zero, Quaternion.identity);
            Instantiate(soundManagerFrefabs, Vector3.zero, Quaternion.identity);
        }
        else
        {
            target = player.transform;
        }
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
