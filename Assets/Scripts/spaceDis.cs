using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spaceDis : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key press detected and ignored.");
        }
    }
}
