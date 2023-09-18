using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [Header("必要なコンポーネント")]
    [SerializeField] private GameObject player;

    private float xPosPlayerInCamera = -6;
    private float yPosFixed = 0;
    private float zPosFixed = -10;

    // Update is called once per frame
    private void Update()
    {
        transform.position = new Vector3(player.transform.position.x - xPosPlayerInCamera, yPosFixed, zPosFixed);
    }
}
