using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEgg : MonoBehaviour
{
    public int index = 2;
    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance.maxEggCount >= index)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
        {
            Destroy(gameObject);
            GameManager.Instance.addMaxCount();
        }
    }
}
