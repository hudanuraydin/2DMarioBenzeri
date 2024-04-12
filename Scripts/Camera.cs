using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private float smoothX;
    [SerializeField] 
    private float smoothY;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float posX = Mathf.MoveTowards(transform.position.x, player.position.x, smoothX);
        float posY=Mathf.MoveTowards(transform.position.y, player.position.y, smoothY);
        transform.position =new Vector3(Mathf.Clamp(posX,minX,maxX),Mathf.Clamp(posY,minY,maxY),transform.position.z);
    }
}
