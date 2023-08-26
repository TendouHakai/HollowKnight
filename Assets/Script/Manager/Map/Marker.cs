using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Marker : MonoBehaviour
{
    [SerializeField] public int ID;
    [SerializeField] Image img;

    // Start is called before the first frame update
    void Start()
    {
        MarkerConfig marker = MarkerConfigs.getInstance().getConfig(ID);
        img.sprite = marker.img;
    }
}
