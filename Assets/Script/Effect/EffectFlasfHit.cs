using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectFlasfHit : MonoBehaviour
{
    [SerializeField] Material flashMaterial;
    [SerializeField] float duration;
    [SerializeField] bool isloop;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;

    public bool isFlash;
    private float timeStart;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalMaterial = spriteRenderer.material;

        isFlash = false;

        timeStart = 0f;  
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlash)
        {
            if (timeStart > duration)
            {
                Flash();

                timeStart = 0f;
                if (isloop == false) 
                {
                    endFlash(); 
                }
            }
            else timeStart += Time.deltaTime;
        }
    }

    public void Flash()
    {
        if(spriteRenderer.material == originalMaterial)
        {
            spriteRenderer.material = flashMaterial;
        }
        else spriteRenderer.material = originalMaterial;
    }

    public void startFlash()
    {
        isFlash = true;

        timeStart = 0f;

        Flash();
    }

    public void endFlash()
    {
        spriteRenderer.material = originalMaterial;

        isFlash = false;
    }
}
