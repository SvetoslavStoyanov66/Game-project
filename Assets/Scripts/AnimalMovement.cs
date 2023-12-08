using UnityEngine;
using UnityEngine.UI;

public class AnimalMovement : MonoBehaviour
{
    public float detectionRange = 0.1f;
    public float rotationSpeed = 5f;
    public float movementSpeed = 2f; 

    private enum MovementState { Moving, Rotating,Staying };
    private MovementState currentState = MovementState.Moving;

    private RaycastHit _hit;
    private Quaternion _targetRotation;
    private Vector3 _moveDirection;
    float secondsStaying = 0;
    float secondsMoving = 0;

    GameObject statsBox;
    GameObject notifier;

    string name;

    void Start()
    {
        GenerateNewTargetRotation();
    }

    void Update()
    {
        MovementStopping();
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
    void MovementStopping()
    {
        secondsMoving += Time.deltaTime;
        if(secondsMoving >= 3)
        {
            if(movementSpeed > 0)
            {
                movementSpeed -= Time.deltaTime * 0.8f;
            }
            
            secondsStaying += Time.deltaTime;
            if(secondsStaying >= 7)
            {
                movementSpeed = 2;
                secondsMoving = 0;
                secondsStaying = 0;
            }
        }
    }

    void DetectObstacle()
    {
        Vector3 forward = transform.forward;

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
    public void AssignUI(GameObject box,GameObject not, string AnimName)
    {
        name = AnimName;
        statsBox = box;
        notifier = not;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            Text text = notifier.GetComponentInChildren<Text>();
            text.text = "Press E to make " + name + " happy";
            notifier.SetActive(true);
        }     
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            notifier.SetActive(false);
        }    
    }
}
