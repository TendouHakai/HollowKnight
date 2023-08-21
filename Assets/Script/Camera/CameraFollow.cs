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

    [Header("----------Viewport Size----------")]
    [SerializeField] Vector2 MaxPosition;
    [SerializeField] Vector2 MinPosition;

    private void Awake()
    {

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

        smoothPosition.x = Mathf.Clamp(smoothPosition.x, MinPosition.x, MaxPosition.x);
        smoothPosition.y = Mathf.Clamp(smoothPosition.y, MinPosition.y, MaxPosition.y);

        transform.position = smoothPosition;
    }
}
