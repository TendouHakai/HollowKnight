using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField] public int ID;

    [Header("----------Component----------")]
    [SerializeField] Image img;
    [SerializeField] TextMeshProUGUI geocountText;

    private void Start()
    {
        ItemConfig item = ItemConfigs.getInstance().getConfig(ID);
        img.sprite = item.img;

        geocountText.text = item.geoCount.ToString();
    }

}
