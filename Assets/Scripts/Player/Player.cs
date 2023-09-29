using System.Drawing;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;

    private float rotationSpeed = 100f;
    public float gravity = 9.81f;
    PlayerInteraction playerInteraction;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
    }
   

    void Update()
    {
        Vector3 gravityVector = Vector3.down * gravity;
        characterController.Move(gravityVector * Time.deltaTime);
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movementDirection = new Vector3(horizontalInput, 0.0f, verticalInput).normalized;

        // Move the character
        if (movementDirection.magnitude > 0.1f)
        {
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);

            // Rotate the character to face the movement direction
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
           
        }
        Interact();

    }
    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerInteraction.Interact();
        }
    }
    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }

    public float GetVerticalInput()
    {
        return Input.GetAxis("Vertical");
    }
}
