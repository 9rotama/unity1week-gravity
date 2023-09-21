using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class SimpleBlock : ObjectPart
{
    
    [SerializeField] private GameObject BlockHitParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void OnCollisionWithPlayer(Player player)
    {
        if(player.IsFloating) {
            SEManager.Instance.Play(SEPath.HIT_FLOOR);
            
            GameObject particleObj = Instantiate(BlockHitParticle);
            if (player.isGravityUpward)
            {
                particleObj.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 0.5f, player.transform.position.z);
            }
            else
            {
                particleObj.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.5f, player.transform.position.z);
            }
            
            player.IsFloating = false;
        }
    }

    
}
