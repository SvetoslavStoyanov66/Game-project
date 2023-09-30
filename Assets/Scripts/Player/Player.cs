using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float fillAmount = 1.0f;  // Corrected typo in variable name
    public Image energyFill;
    public Text energyAmountText;
    private float rotationSpeed = 1f;
    public float gravity = 9.81f;
    public Timer timer;
    private float timer1 = 0f;// Reference to the Timer script


    PlayerInteraction playerInteraction;
    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerInteraction = GetComponentInChildren<PlayerInteraction>();
        timer = FindObjectOfType<Timer>();  // Find the Timer script in the scene
        
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

        // Check if energy is 0 and update day and energy accordingly
        if (fillAmount <= 0)
        {
            StartCoroutine(SecondsBeforeChangingData());
           
        }
        var animationsPlayer = FindObjectOfType<AnimaationsPlayer>();
        if (animationsPlayer != null)
        {
            animationsPlayer.TriggerAnimations();
        }
        
        EnergyDisplay();
        EnergyTextDisplay();
        
        
            // Update the timer
            timer1 += Time.deltaTime;

            // Check if 30 seconds have passed
            if (timer1 >= 30f)
            {
                // Decrease fillAmount by 1
                fillAmount -= 0.01f;

                // Ensure fillAmount doesn't go below 0
                fillAmount = Mathf.Max(0, fillAmount);

                // Reset the timer
                timer1 = 0f;
            }
        
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerInteraction.InteractWithLand();
            if (playerInteraction.selectedTool != null && playerInteraction.selectedTool.name.Equals("Hoe") && playerInteraction.selectedLand != null)
            {
                fillAmount -= 0.1f;
                EnergyDisplay();  // Update the energy display
            }
        }

    }
    
    private void EnergyDisplay()
    {
        energyFill.fillAmount = fillAmount;
    }

    private void EnergyTextDisplay()
    {
        if (energyAmountText != null)
        {
            energyAmountText.text = "Energy - " + (fillAmount * 100).ToString("0");
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
    public IEnumerator SecondsBeforeChangingData()
    {
       yield return new WaitForSeconds(2.5f);
        timer.day++;  // Advance to the next day
        timer.hours = 8;  // Reset hours to 8
        fillAmount = 0.75f;  // Reset energy to 75%
    }
   


}
