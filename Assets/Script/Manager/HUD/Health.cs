using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField] Image img;

    public void healthImgSetNativeSize()
    {
        img.SetNativeSize();
    }
}
