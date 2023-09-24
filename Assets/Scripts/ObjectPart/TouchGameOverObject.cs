using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchGameOverObject : ObjectPart
{
    public override void OnCollisionWithPlayer(Player player)
    {
        player.OutStage();
    }
}
