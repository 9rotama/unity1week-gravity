using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        BGMManager.Instance.Play(BGMPath.TITLE);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
