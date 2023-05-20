using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HPObject
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void kill()
    {
        if (!isDead)
        {
            
            Invoke("restartGame",2);
        }
        base.kill();
    }

    void restartGame()
    {
        GameManager.Instance.restartGame();
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
