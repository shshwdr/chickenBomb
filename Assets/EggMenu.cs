using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class EggMenu:MonoBehaviour
{
    public Transform parent;

    public EggCell[] eggCells;
    // Start is called before the first frame update
    protected  void Awake()
    {
        eggCells = parent.GetComponentsInChildren<EggCell>();
    }

    public void update(int used, int maxCount)
    {
        int i = 0;
        for (; i < math.min(eggCells.Length, maxCount - used); i++)
        {
            eggCells[i].show(true); 
        }
        for (; i < math.min(eggCells.Length,maxCount); i++)
        {
            eggCells[i].show(false); 
        }
        for (; i < eggCells.Length; i++)
        {
            eggCells[i].hide(); 
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
