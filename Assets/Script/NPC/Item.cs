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
    [SerializeField] public RectTransform rectTranform;

    public bool isEmpty;

    private void Start()
    {
        if (isEmpty) return;
        ItemConfig item = ItemConfigs.getInstance().getConfig(ID);
        img.sprite = item.img;
        if(geocountText != null)
        {
            geocountText.text = item.geoCount.ToString();
        }
    }

    public void loadItem(int ID)
    {
        this.ID = ID;
        ItemConfig item = ItemConfigs.getInstance().getConfig(this.ID);
        if(item != null)
        {
            Debug.Log(item.img.ToString());
            img.sprite = item.img;
            if (geocountText != null)
            {
                geocountText.text = item.geoCount.ToString();
            }
        }
    }
}
