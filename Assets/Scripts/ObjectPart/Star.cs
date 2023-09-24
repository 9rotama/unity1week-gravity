using System.Collections;
using System.Collections.Generic;
using KanKikuchi.AudioManager;
using UnityEngine;

public class Star : ObjectPart
{
    [SerializeField] int point;
    [SerializeField] private GameObject getParticle;

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

        GameObject particleObj =Instantiate(getParticle);
        particleObj.transform.position = transform.position;

        SEManager.Instance.Play(SEPath.SCORE_ITEM);


        //画面外に移動
        transform.position = Vector2.left * 20;
    }


}
