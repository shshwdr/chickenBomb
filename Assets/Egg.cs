using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public float blinkStartTime = 0.5f;
    public  float blinkStartInterval = 0.1f;
    public  float blinkStartIntervalDecrease = 0.1f;
    public  float blinkStartIntervalMinimal = 0.1f;
    private SpriteRenderer renderer;
    public Color color;
    private float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time > blinkStartTime)
        {
            DOTween.To(()=> renderer.color, x=> renderer.color = x, color, blinkStartInterval).SetLoops(2,LoopType.Yoyo);
            blinkStartTime += blinkStartInterval * 2;
            blinkStartInterval -= blinkStartIntervalDecrease;
            if (blinkStartInterval < blinkStartIntervalMinimal)
            {
                blinkStartInterval =blinkStartIntervalMinimal;
            }

        }
    }
}
