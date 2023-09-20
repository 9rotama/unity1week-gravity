using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class SimpleBlock : ObjectPart
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void OnCollisionWithPlayer(Player player)
    {
        if(player.IsFloating) {
            SEManager.Instance.Play(SEPath.HIT_FLOOR);
            player.IsFloating = false;
        }
        
    }

    
}
