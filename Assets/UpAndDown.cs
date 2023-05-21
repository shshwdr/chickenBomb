using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    public float time = 3;

    public float distance = 1;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOMoveY(transform.position.y + distance, time).SetLoops(-1, LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
