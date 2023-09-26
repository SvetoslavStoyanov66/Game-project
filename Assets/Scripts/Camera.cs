


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player, cameraTrans;

    void Update()
    {
        cameraTrans.LookAt(player);
    }
}