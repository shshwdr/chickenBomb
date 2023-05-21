using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillPlayerWhenHit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {

        // if (GetComponent<HPObject>().isDead)
        // {
        //     return;
        // }
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().kill();
            //AudioManager.Instance.playInsect();
        }
    }
}
