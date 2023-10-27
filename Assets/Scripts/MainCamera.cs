


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public Transform player;
    public float offSetZ = 9f;
    public float smoothing = 2;
    Player player1;
    void Start()
    {
        player = FindObjectOfType<Player>().transform;
        player1 = GetComponent<Player>();  
    }
    void Update()
    {
        FollowPlayer();
    }
    void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(player.position.x,player.position.y + 6,player.position.z - offSetZ);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
   
}