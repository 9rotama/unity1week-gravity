using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeometryAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject[] sprites;

    private const float spawnSpan = 0.6f;
    private const float scaleMax = 1f;
    private const float scaleMin = 0.1f;
    private const float speedMax = 0.1f;
    private const float speedMin = 0.01f;
    private const float alphaMax = 0.5f;
    private const float alphaMin = 0.1f;
    private const float xMax = 10f;
    private const float xMin = -10f;


    void Start()
    {
        StartCoroutine(nameof(SpawnSprite));
    }


    void Spawn()
    {
        GameObject spriteObj = Instantiate(sprites[Random.Range(0, sprites.Length - 1)], this.transform);
        float x = Random.Range(xMin, xMax);

        spriteObj.transform.localPosition = new Vector3(x, transform.position.y, transform.position.z);
        float scale = Random.Range(scaleMin, scaleMax);
        float speed = Random.Range(speedMin, speedMax);
        float alpha = Random.Range(alphaMin, alphaMax);

        spriteObj.GetComponent<GeometryAnimationSpriteController>().init(scale, alpha, speed);
    }

    private IEnumerator SpawnSprite()
    {
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnSpan);
        }
    }
}
