using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

abstract public class ObjectPart : MonoBehaviour
{

    public Vector2 objectSize {get; private set;}

    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        objectSize = spriteRenderer.bounds.size;
        // Debug.Log(objectSize);
    }


    public virtual void OnCollisionWithPlayer(Player player) {}
}
