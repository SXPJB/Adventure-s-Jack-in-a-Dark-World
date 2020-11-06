using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraFollow : MonoBehaviour
{

    public GameObject player;
    public Vector2 minCamPosition, maxCamPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float posX = player.transform.position.x;
        float posY = player.transform.position.y;
        transform.position = new Vector3(
            Mathf.Clamp(posX ,minCamPosition.x,maxCamPosition.x),
            Mathf.Clamp(posY, minCamPosition.y, maxCamPosition.y), 
            transform.position.z);
        
    }
}
