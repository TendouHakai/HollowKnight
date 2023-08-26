using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;
using static UnityEditor.PlayerSettings;

public class MiniMapExpandControler : MiniMapControler
{
    public bool isplayerIN;

    protected override void Start()
    {
        player = GameObject.FindObjectOfType<Player>();
        loadMarkersData();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (isplayerIN)
        {
            base.Update();
        }
    }

    public override void loadMarkersData()
    {
        List<markerInArea> markersInArea = MapConfig.getInstance().GetMarkerInArea(ID);

        foreach(markerInArea markerA in markersInArea)
        {
            Marker marker = Instantiate(markerFrefab, transform.position, Quaternion.identity);
            marker.ID = markerA.ID;
            marker.transform.parent = this.transform;

            marker.transform.localPosition = markerA.position;
            markers.Add(marker);
        }
    }

    public override void addMarker(int id, Vector3 pos)
    {
        Vector3 normalVector = Divide(
                originRTranform.InverseTransformPoint(pos),
                endRTranform.position - originRTranform.position
            );

        Vector3 mapPos = Multiply(normalVector, endMTranform.localPosition);

        // check market exsist or not
        if(MapConfig.getInstance().addMarker(ID, id, mapPos))
        {
            // add game object market into minimap
            Marker marker = Instantiate(markerFrefab, transform.position, Quaternion.identity);
            marker.ID = id;
            marker.transform.parent = this.transform;

            marker.transform.localPosition = mapPos;
            markers.Add(marker);
            // save marker
            MinimapManager.getInstance().addMarker(id, pos);
        }    
    }

    public void setPLayerisIn(bool isplayerIn)
    {
        this.isplayerIN = isplayerIn;
        if (isplayerIn)
            playerMarkerTranform.gameObject.SetActive(true);
        else playerMarkerTranform.gameObject.SetActive(false);
    }

    public void updateMapCoordinates()
    {
        originRTranform = MinimapManager.getInstance().MinimapUI.originRTranform;
        endRTranform = MinimapManager.getInstance().MinimapUI.endRTranform;
    }
}
