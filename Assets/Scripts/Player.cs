using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    public float stepDistance = 1f; // Distance to move per step

    private Vector3 targetPosition; // Target position for movement

    void Start()
    {
        targetPosition = transform.position;
    }

    void Update()
    {
        // Check for movement input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
        {
            // Calculate the new target position based on input and step distance
            Vector3 inputDirection = new Vector3(horizontalInput, 0f, verticalInput).normalized;
            targetPosition = transform.position + inputDirection * stepDistance;
        }

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }
}
