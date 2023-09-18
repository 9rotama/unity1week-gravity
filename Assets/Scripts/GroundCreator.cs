using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCreator : MonoBehaviour
{
    [SerializeField] GameObject squareGround;

    [SerializeField] Transform playerTransform;

    float groundInterval; 

    Vector2 squareGroundSize;
    float minSpawnYPos = -4.33f;
    float maxSpawnYPos = 4.33f;

    Vector2 prevPlayerPos;

    // Start is called before the first frame update
    void Start()
    {

        squareGroundSize = squareGround.GetComponent<SpriteRenderer>().bounds.size;
        Debug.Log(squareGroundSize);

        groundInterval = squareGroundSize.x;
        
        for(int i=0; i<10; i++){
            Vector2 spawnPos = new Vector2(i * squareGroundSize.x, Random.Range(0,2) == 0 ? minSpawnYPos : maxSpawnYPos);
            Create(spawnPos);
        }


    }

    int groundBase;

    // Update is called once per frame
    void Update()
    {

        int tmp =  Mathf.FloorToInt(playerTransform.position.x / groundInterval);

        if(groundBase < tmp) {
            Vector2 spawnPos = new Vector2((float)tmp * groundInterval + 20.0f,minSpawnYPos /*Random.Range(0,2) == 0 ? minSpawnYPos : maxSpawnYPos*/);
            Create(spawnPos);
            groundBase = tmp;
        }
    }

    void Create(Vector2 pos) 
    {
        Instantiate(squareGround, pos, Quaternion.identity);
    }
}
