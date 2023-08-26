    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkerPoint : MonoBehaviour
{
    [SerializeField] int idMarker;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            MarkerConfig config = MarkerConfigs.getInstance().getConfig(idMarker);
            if (InventoryConfig.getInstance().IsInInventory(config.IDItemNeed))
            {
                UIManager.getInstance().addMarker(idMarker, transform.position);
            }
        }
    }
}
