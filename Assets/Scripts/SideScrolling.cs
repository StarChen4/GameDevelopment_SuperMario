using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideScrolling: MonoBehaviour
{
    private Transform player;

    public float height = 5.5f;
    public float undergroundHeight = -10.5f;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.x = Mathf.Max(cameraPosition.x, player.position.x); // 确保摄像机不往左走
        transform.position = cameraPosition;
    }

    public void SetUnderGround(bool underground)
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition.y = underground ? undergroundHeight : height;
        transform.position = cameraPosition;
    }
}
