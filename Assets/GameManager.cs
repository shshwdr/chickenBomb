using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public  System.Random random;
    private int seed = 0;
    public int maxEggCount = 1;
    public int eggUsedCount = 0;
    public Vector3 lastSavePoint = Vector3.negativeInfinity;
    public void savePoint(Vector3 point)
    {
        lastSavePoint = point;
    }
    public void returnEgg()
    {
        eggUsedCount--;
        if (GameObject.FindObjectOfType<EggMenu>())
        {
            
            GameObject.FindObjectOfType<EggMenu>() .update(eggUsedCount,maxEggCount);
        }
    }

    public void useEgg()
    {
        eggUsedCount++;
        if (GameObject.FindObjectOfType<EggMenu>())
        {
            
            GameObject.FindObjectOfType<EggMenu>() .update(eggUsedCount,maxEggCount);
        }
    }

    public void addMaxCount()
    {
        maxEggCount++;
        if (GameObject.FindObjectOfType<EggMenu>())
        {
            
            GameObject.FindObjectOfType<EggMenu>() .update(eggUsedCount,maxEggCount);
        }
    }
    public void restartGame()
    {
        eggUsedCount = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    // Start is called before the first frame update
    void Awake()
    {
         random = new System.Random(seed);
         DontDestroyOnLoad(gameObject);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartGame();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            addMaxCount();
        }
    }
}
