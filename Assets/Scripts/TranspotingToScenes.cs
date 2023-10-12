using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranspotingToScenes : MonoBehaviour
{
    private static TranspotingToScenes instance;

    // This will make sure there's only one instance of TransportingToScenes
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    // Update is called once per frame
}