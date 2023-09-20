using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryAnimationSpriteController : MonoBehaviour
{
    private float speed;
    private float yPosDestroy = 7;

    private SpriteRenderer spriteRenderer;
    // Start is called before the first frame update

    public void init(float scale, float alpha, float speed)
    {
        transform.localScale = new Vector3(scale, scale, scale);
        this.speed = speed;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color  = new  Color(1,1,1,alpha);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (yPosDestroy < transform.localPosition.y)
        {
            Destroy(this.gameObject);
        }
        transform.localPosition += new Vector3(0, 1, 0) * speed;
    }

    
}
