using UnityEngine;

public class RainPosition : MonoBehaviour
{
    public Transform objectToCopy; // The object whose position you want to copy
    public Transform destinationObject; // The object to which the position will be copied

    void Update()
    {
        if (objectToCopy != null && destinationObject != null)
        {
            // Copy the x and z positions from the objectToCopy to the destinationObject
            Vector3 newPosition = new Vector3(objectToCopy.position.x, destinationObject.position.y, objectToCopy.position.z);
            destinationObject.position = newPosition;
        }
    }
}
