using UnityEngine;

public class AnimalMovement : MonoBehaviour
{
    public float detectionRange = 0.1f; // Range to detect obstacles
    public float rotationSpeed = 5f; // Speed at which the animal turns
    public float movementSpeed = 2f; // Speed at which the animal moves forward
    public float rotationChangeInterval = 3f; // Time interval to change rotation

    private enum MovementState { Moving, Rotating };
    private MovementState currentState = MovementState.Moving;

    private RaycastHit _hit; // Store the raycast hit information
    private Quaternion _targetRotation;
    private Vector3 _moveDirection;

    void Start()
    {
        GenerateNewTargetRotation();
    }

    void Update()
    {
        switch (currentState)
        {
            case MovementState.Moving:
                DetectObstacle();
                MoveForward();
                break;
            case MovementState.Rotating:
                RotateToTargetDirection();
                break;
        }
    }

    void DetectObstacle()
    {
        Vector3 forward = transform.forward;

        // Cast a ray forward to detect obstacles
        if (Physics.Raycast(transform.position, forward, out _hit, detectionRange))
        {
            currentState = MovementState.Rotating;
        }
    }

    void MoveForward()
    {
        if (currentState == MovementState.Moving)
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
        }
    }

    void GenerateNewTargetRotation()
    {
        float randomYRotation = Random.Range(0f, 360f);
        _targetRotation = Quaternion.Euler(0f, randomYRotation, 0f);
    }

    void RotateToTargetDirection()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, Time.deltaTime * rotationSpeed);
        
        if (Quaternion.Angle(transform.rotation, _targetRotation) < 1f)
        {
            currentState = MovementState.Moving;
            GenerateNewTargetRotation();
        }
    }
}
