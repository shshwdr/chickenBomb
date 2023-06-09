using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : HPObject
{
    // Start is called before the first frame update
    
    private void Start()
    {
        GameObject.FindObjectOfType<EggMenu>().update(GameManager.Instance. eggUsedCount,GameManager.Instance. maxEggCount);
        if (GameManager.Instance.lastSavePoint.x == float.NegativeInfinity)
        {
            
        }
        else
        {
            transform.position = GameManager.Instance.lastSavePoint;
        }
    }

    public override void kill()
    {
        if (!isDead)
        {
            
            Invoke("restartGame",1);
            
            sfxManager.Instance.play(sfxManager.Instance.chickenDie);
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
