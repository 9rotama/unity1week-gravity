using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPlayerParticle : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(nameof(remove));

    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(5f);
        Destroy(this.gameObject);
    }
}
