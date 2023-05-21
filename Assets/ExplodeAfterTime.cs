using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ExplodeAfterTime : MonoBehaviour
{
    private bool hasExploded = false;
    private float timer = 0;
    public GameObject explodeCollider;
    public float afterTime = 2;

    public float radius;

    public LayerMask layerMask;
    public LayerMask destroyLayerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            explode();
        }
    }

    void explode()
    {
        if (hasExploded)
        {
            return;
        }
        
        sfxManager.Instance.play(sfxManager.Instance.bomb);
        hasExploded = true;

        var go= Instantiate(explodeCollider,transform.position,quaternion.identity);
        go.gameObject.SetActive(true);
        Destroy(gameObject);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, layerMask);

        foreach (var collider in colliders)
        {
            if (collider.GetComponent<HPObject>() && collider.GetComponent<HPObject>().canBeKilled)
            {
                collider.GetComponent<HPObject>().kill();
            }
            
        }
        
        
        Collider2D[] colliders2 = Physics2D.OverlapCircleAll(transform.position, radius, destroyLayerMask);

        foreach (var collider in colliders2)
        {
            Destroy( collider.gameObject);

        }
    }
    // Update is called once per frame
    void Update()
    {
        if (hasExploded)
        {
            return;
        }

        timer += Time.deltaTime;
        if (timer >= afterTime)
        {

            explode();
        }
    }
}