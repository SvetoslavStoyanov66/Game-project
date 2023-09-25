using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private CharacterController controller;
    private Vector3 movement;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float moveInputX = Input.GetAxis("Horizontal");
        float moveInputY = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 inputDirection = new Vector3(moveInputX, 0f, moveInputY).normalized;

        // Calculate the movement vector
        movement = inputDirection * moveSpeed * Time.deltaTime;

        // Apply movement
        controller.Move(movement);

        // If no movement input, stop the character instantly
        if (Mathf.Approximately(moveInputX, 0f) && Mathf.Approximately(moveInputY, 0f))
        {
            controller.Move(Vector3.zero);
        }
    }
}
