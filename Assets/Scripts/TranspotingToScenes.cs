using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranspotingToScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
    // Update is called once per frame
