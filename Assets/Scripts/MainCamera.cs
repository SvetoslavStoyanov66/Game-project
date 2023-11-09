


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    bool followCar = false;
    public Transform player;
    [SerializeField]
    Transform car;
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
        if(followCar)
        {
            FollowCar();
        }
        else
        {
            FollowPlayer();
        }
    }
    void FollowPlayer()
    {
        Vector3 targetPosition = new Vector3(player.position.x,player.position.y + 6,player.position.z - offSetZ);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
    void FollowCar() 
    {
        Vector3 targetPosition = new Vector3(car.position.x,car.position.y + 6,car.position.z - offSetZ);
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
     IEnumerator CarFollowTime(float time)
    {
        followCar = true;
        yield return new WaitForSeconds(time);
        followCar = false;
    }
    public void CameraFollowingCar()
    {
        StartCoroutine(CarFollowTime(3f));
    }
    
   
}