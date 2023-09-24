using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ObjectPart
{
    [SerializeField] int point;

    static GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        if(gameManager == null) {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }
    }

    public override void OnCollisionWithPlayer(Player player)
    {
        gameManager.IncreaseScore(point);

        //画面外に移動
        transform.position = Vector2.left * 20;
        Debug.Log("ポイントゲットだぜ");
    }


}
