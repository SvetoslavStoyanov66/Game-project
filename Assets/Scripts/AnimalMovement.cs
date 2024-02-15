﻿using UnityEngine;
using UnityEngine.UI;

public class AnimalMovement : MonoBehaviour
{
    public float detectionRange = 0.1f;
    public float rotationSpeed = 5f;
    public float movementSpeed = 1f; 

    private enum MovementState { Moving, Rotating,Staying };
    private MovementState currentState = MovementState.Moving;

    [SerializeField]
    private ItemData mildData;
    private RaycastHit _hit;
    private Quaternion _targetRotation;
    float secondsStaying = 0;
    float secondsMoving = 0;
    [SerializeField]
    GameObject notifier;
    public bool canBeMilked = false;
    Animator animator;

   


    string name;
    bool canInteract = false;
    public bool isNotifierActive = false;

    void Start()
    {
        GenerateNewTargetRotation();

    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && canBeMilked && canInteract)
        {
            canBeMilked = false;
            Inventory.Instance.HarvestCrops(mildData);
        }
        if(movementSpeed > -1)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }
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
                movementSpeed = 0;
                
            }
            
            secondsStaying += Time.deltaTime;
            if(secondsStaying >= 9)
            {
                movementSpeed = 1;
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
    public void AssignUI( string AnimName ,ItemData milk,GameObject notifierObject)
    {
        name = AnimName;
        animator = FindObjectOfType<Animator>();
        mildData = milk;
        notifier = notifierObject;
    }
    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Player") && canBeMilked)
        {
            notifier.SetActive(true);
           canInteract = true; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            notifier.SetActive(false);
           canInteract = false; 
        }
    }

   
}
