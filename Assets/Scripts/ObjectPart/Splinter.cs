using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Splinter : ObjectPart
{
    static GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if(gameManager == null) {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnCollisionWithPlayer(Player player)
    {
        gameManager.DecreaseScore();
        Debug.Log("刺さったぺこ");
    }
}
