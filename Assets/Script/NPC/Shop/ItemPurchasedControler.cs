using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPurchasedControler : MonoBehaviour
{
    [SerializeField] Shop shopcontroler;
    
    public void endNotification()
    {
        shopcontroler.ItemDisplayW.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
