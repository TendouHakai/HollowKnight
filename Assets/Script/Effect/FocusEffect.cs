using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusEffect : MonoBehaviour
{
    [SerializeField] Animator ani;
    [SerializeField] Vector3 offset;

    public void startFocusEffect()
    {
        transform.position += offset;
        ani.Play("FocusEffect");
        Destroy(this.gameObject, 1f);
    }

    public void StartFocusEffectGet()
    {
        transform.position += offset;
        ani.Play("FocusEffectGet");
        Destroy(this.gameObject, 0.25f);
    }
}
