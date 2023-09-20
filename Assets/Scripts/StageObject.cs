using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.SceneManagement;
using UnityEngine;

public class StageObject : MonoBehaviour 
{
    [field: SerializeField] 
    public Vector2 objectSize {get; private set;} 

    public (float max, float min) GetLimitYPos()
    {
        var screenTop = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height)).y;
        var screenBottom = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).y;

        return (screenTop - objectSize.y/2, screenBottom + objectSize.y/2);
    }

}
