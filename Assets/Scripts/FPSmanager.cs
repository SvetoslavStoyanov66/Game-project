using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPSmanager : MonoBehaviour
{
    public Text text; // Assign this in the inspector if possible
    private float deltaTime = 0.0f;

    void Start()
    {
        Application.targetFrameRate = 60;
        // Ensure text component is attached
        if (text == null) {
            text = GetComponent<Text>();
        }
    }

    void Update()
    {
        // Update delta time
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Calculate fps
        float fps = 1.0f / deltaTime;

        // Update text component with FPS rounded to 1 decimal place
        text.text = string.Format("{0:0.0} FPS", fps);
    }
}
