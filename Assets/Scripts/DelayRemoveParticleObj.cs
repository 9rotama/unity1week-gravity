using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayRemoveParticleObj : MonoBehaviour
{
    [SerializeField] private float destroyTime;
    void Start()
    {
        StartCoroutine(nameof(remove));

    }

    IEnumerator remove()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(this.gameObject);
    }
}
