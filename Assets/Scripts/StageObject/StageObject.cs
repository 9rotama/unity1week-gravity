using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

abstract public class StageObject : MonoBehaviour
{

    public Vector2 objectSize {get; private set;}

    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        objectSize = spriteRenderer.bounds.size;
    }

    public (float max, float min) GetLimitYPos()
    {
        var screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        var screenTop = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).y;


        return (screenTop + objectSize.y/2, screenBottom - objectSize.y/2);
    }

    public virtual void OnCollisionWithPlayer(Player player) {}
}
