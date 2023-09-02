using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSceneLoader : MonoBehaviour
{
    public virtual void endSceneLoader()
    {
        this.gameObject.SetActive(false);   
    }
}
