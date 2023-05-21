using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public List<GameObject> flower;

    public Transform flowerPositionParent;
    Transform[] flowerPositions;
    private int maxPlant = 4;
    private HashSet<int> visited = new HashSet<int>();
    // Start is called before the first frame update
    void Start()
    {
        flowerPositions = flowerPositionParent.GetComponentsInChildren<Transform>();

        for (int i = 0; i < maxPlant; i++)
        {
            if (!create())
            {
                break;
            }
        }
    }

    bool create()
    {
        
        var rand1 = GameManager.Instance.random.Next(flowerPositions.Length);
        var rand2 = GameManager.Instance.random.Next(flower.Count);
if(visited.Contains(rand1))
{
    return false;
}

visited.Add(rand1);
        Instantiate(flower[rand2], flowerPositions[rand1]);
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
