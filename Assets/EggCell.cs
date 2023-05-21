using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EggCell : MonoBehaviour
{
    public void show(bool isVisible)
    {
        gameObject.SetActive(true);
        GetComponentInChildren<Image>().color = new Color(1, 1, 1, isVisible ? 1 : 0.5f);
    }
    
    public void hide()
    {
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
