using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] GameObject select;

    public void OnPointerEnter(PointerEventData eventData)
    {
        select.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        select?.SetActive(false);
    }

    public virtual void Start()
    {
        select.SetActive(false);
    }
}
