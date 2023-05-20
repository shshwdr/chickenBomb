using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform start;
    public Transform end;
    public GameObject enemy;

    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        enemy. transform.position = start.position;
        enemy. transform.DOMove(end.position, (end.position - start.position).magnitude * speed).SetLoops(-1,LoopType.Yoyo);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
