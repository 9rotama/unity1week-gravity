using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageObject : MonoBehaviour 
{
    public readonly Vector2 objectSize  = Vector2.one * 1.3f;

    static float screenTop;
    static float screenBottom;

    private void Awake() 
    {
        screenTop = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).y;
    }


    public (float max, float min) GetLimitYPos()
    {
        return (screenTop - objectSize.y/2, screenBottom + objectSize.y/2);
    }

}
