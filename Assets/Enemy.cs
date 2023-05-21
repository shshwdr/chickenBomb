using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : HPObject
{

    public float moveRadius;
    public float moveSpeed;
    Rigidbody2D rb;
    public float moveScaleY = 0.9f;
    public float moveTransform = 0.1f;
    public float moveScaleX = 0.7f;
    public float moveScaleTime = 0.6f;
    public SpriteRenderer renderPart;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        renderPart. transform.DOScaleY(moveScaleY, moveScaleTime).SetLoops(-1, LoopType.Yoyo);
        renderPart. transform.DOScaleX(moveScaleX, moveScaleTime).SetLoops(-1, LoopType.Yoyo);
        renderPart. transform.DOLocalMoveY(renderPart.transform.localPosition.y+moveTransform, moveScaleTime).SetLoops(-1, LoopType.Yoyo);
    }

    public override void kill()
    {
        
        transform.DOKill();
        base.kill();

    }


private float lastX = float.NegativeInfinity;
    // Update is called once per frame
    void Update()
    {
        if (lastX == float.NegativeInfinity)
        {
            lastX = transform.position.x;
            return;
        }
        if (transform.position.x >= lastX)
        {
            renderPart.flipX = true;
        }
        else
        {
            
            renderPart.flipX = false;
        }
        lastX = transform.position.x;
    }
    private void FixedUpdate()
    {
        
    }

    // private void OnTriggerStay2D(Collider2D collision)
    // {
    //     //var playerLayer = LayerMask.NameToLayer("Player");
    //     //Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, moveRadius, playerLayer);
    //     if (collision.GetComponent<PlayerMovement>())
    //     {
    //         var targetPosition = collision.transform.position;
    //         var dir = (targetPosition - transform.position).normalized;
    //         rb.MovePosition(transform.position + dir * Time.deltaTime * moveSpeed);
    //     }
    // }
    //
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (GetComponent<HPObject>().isDead)
        {
            return;
        }
        if (collision.GetComponent<Player>())
        {
            collision.GetComponent<Player>().kill();
            //AudioManager.Instance.playInsect();
        }
    }
    //
    // private void OnTriggerExit2D(Collider2D collision)
    // {
    //
    //     if (collision.GetComponent<PlayerMovement>())
    //     {
    //         //AudioManager.Instance.playInsect();
    //     }
    // }
}
