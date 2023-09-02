using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapControler : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI NameAreaText;
    [SerializeField] protected Image mapImg;
    [SerializeField] protected Player player;
    public int ID;

    [SerializeField] MinimapManager minimapManager;

    [Header("-------------TRANFORM--------------")]
    [SerializeField] public Transform originRTranform;
    [SerializeField] public Transform endRTranform;
    [SerializeField] protected RectTransform endMTranform;
    [SerializeField] protected RectTransform playerMarkerTranform;
    int idMarkerplayer;

    [Header("-------------MARKER----------------")]
    [SerializeField] protected Marker markerFrefab;
    [SerializeField] protected List<Marker> markers;


    protected Vector3 normalized, mapped;

    protected virtual void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        idMarkerplayer = playerMarkerTranform.GetComponent<Marker>().ID;
        if (MapConfig.getInstance().GetAreaConfig(ID).isUnlock == false)
        {
            minimapManager.isNoMap = true;
            minimapManager.changeToNoMapUI();
        }
        loadMarkersData();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        MarkerConfig config = MarkerConfigs.getInstance().getConfig(idMarkerplayer);
        if (InventoryConfig.getInstance().IsInInventory(config.IDItemNeed))
        {
            playerMarkerTranform.gameObject.SetActive(true);
            normalized = Divide(
                originRTranform.InverseTransformPoint(player.transform.position),
                endRTranform.position - originRTranform.position
            );
            mapped = Multiply(normalized, endMTranform.localPosition);
            playerMarkerTranform.localPosition = mapped;
        }
        else
        {
            playerMarkerTranform.gameObject.SetActive(false);
        }
    }

    public Vector3 Divide(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, 0);
    }

    public Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, 0);
    }

    public virtual void loadMarkersData()
    {
        List<markerInArea> markersInArea = MapConfig.getInstance().GetMarkerInArea(ID);

        foreach (markerInArea markerA in markersInArea)
        {
            Marker marker = Instantiate(markerFrefab, transform.position, Quaternion.identity);
            marker.ID = markerA.ID;
            marker.transform.parent = this.transform;

            marker.transform.localPosition = markerA.position * mapImg.transform.localScale.x;
            markers.Add(marker);
        }
    }

    public virtual void addMarker(int id, Vector3 pos)
    {
        Vector3 normalVector = Divide(
                originRTranform.InverseTransformPoint(pos),
                endRTranform.position - originRTranform.position
            );

        Vector3 mapPos = Multiply(normalVector, endMTranform.localPosition);

        Marker marker = Instantiate(markerFrefab, transform.position, Quaternion.identity);
        marker.ID = id;
        marker.transform.parent = this.transform;

        marker.transform.localPosition = mapPos;
        markers.Add(marker);
    }

    public void setPlayer(Player playerTranform)
    {
        player = playerTranform;
    }
}
