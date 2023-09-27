


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public float offSetZ = 9f;
    public float smoothing = 2;
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
    }
    void Update()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(player.position.x,transform.position.y,player.position.z - offSetZ);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}