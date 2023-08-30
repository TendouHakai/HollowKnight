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
    [SerializeField] PlayerControl playerControlFrefabs;
    [SerializeField] UIManager uIManagerFrefabs;
    [SerializeField] SceneLoader sceneLoaderFrefabs;

    [Header("----------Viewport Size----------")]
    [SerializeField] Vector2 MaxPosition;
    [SerializeField] Vector2 MinPosition;

    private void Awake()
    {

    }
    void Start()
    {
        if (target != null) return;
        Player player = GameObject.FindObjectOfType<Player>();
        if (player == null)
        {
            player = Instantiate(playerFrefabs, transform.position, Quaternion.identity);
            target = player.transform;

            Instantiate(playerControlFrefabs, Vector3.zero, Quaternion.identity);
            Instantiate(sceneLoaderFrefabs, Vector3.zero, Quaternion.identity);
            Instantiate(uIManagerFrefabs, Vector3.zero, Quaternion.identity);

            GameStateManager.getInstance().addSubcriberDontDestroy();
        }
        else
        {
            target = player.transform;
        }

        SoundManager.getInstance().PlayMusic("AbyssMusic01");
        MinimapManager.getInstance().setPlayer(player);
    }

    // Update is called once per frame

    private void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothPosition = Vector3.Slerp(transform.position, desiredPosition, speed * Time.deltaTime);

        smoothPosition.x = desiredPosition.x;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, MinPosition.x, MaxPosition.x);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, MinPosition.y, MaxPosition.y);

        transform.position = desiredPosition;
    }
}
