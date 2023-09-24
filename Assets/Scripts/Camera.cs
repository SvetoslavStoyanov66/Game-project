using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform target; // Player's transform to follow
    public Vector3 offset = new Vector3(0f, 10f, -10f); // Offset from the target position

    void Update()
    {
        if (target != null)
        {
            // Follow the player's position with the specified offset
            transform.position = target.position + offset;
        }
    }
}
