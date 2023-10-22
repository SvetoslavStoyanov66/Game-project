using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float fillAmount = 1.0f;  // Corrected typo in variable name
    public Image energyFill;
    public Text energyAmountText;
    public float gravity = 9.81f;
    public Timer timer;
    private float timer1 = 0f;
    int counter;
    bool TakeEnergy = true;
    public float minX = -18.0f;
    public float maxX = 35.0f;
    public float minZ = -35.0f;
    public float maxZ = 21.0f;
    private bool eneableRotation = true;
    public GameObject indicator;
    public GameObject indicator2;
    // Reference to the Timer script


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
        MovementLimitaitons();

        // Move the character
        if (movementDirection.magnitude > 0.1f)
        {
            transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);

            if (eneableRotation == true)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.1f);
            }
           
        }

        Interact();

        // Check if energy is 0 and update day and energy accordingly
        if (fillAmount <= 0)
        {
            StartCoroutine(DiseableMovement(12));
            StartCoroutine(SecondsBeforeChangingData(0.75f));
        }
        var animationsPlayer = FindObjectOfType<AnimaationsPlayer>();
        if (animationsPlayer != null)
        {
            animationsPlayer.TriggerAnimations();
        }

        EnergyDisplay();
        EnergyTextDisplay();



        timer1 += Time.deltaTime;

        // Check if 30 seconds have passed
        if (timer1 >= 30f)
        {
            // Decrease fillAmount by 1
            fillAmount -= 0.04f;

            // Ensure fillAmount doesn't go below 0
            fillAmount = Mathf.Max(0, fillAmount);

            // Reset the timer
            timer1 = 0f;
        }
        if (fillAmount >= 1)
        {
            fillAmount = 1;
        }


    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            playerInteraction.InteractWithLand();
            playerInteraction.InteractWithFood();
            if (playerInteraction.selectedTool != null && playerInteraction.selectedTool.name.Equals("Hoe") && playerInteraction.selectedLand != null && TakeEnergy ==  true)
            {
                StartCoroutine (EnergyTaking());
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
    public IEnumerator SecondsBeforeChangingData(float fill)
    {

        yield return new WaitForSeconds(2.5f);


        timer.hours = 8;
        fillAmount = fill;
        if (counter < 1)
        {
            timer.day++;
        }
        counter++;
        yield return new WaitForSeconds(5);
        counter = 0;
    }
    public IEnumerator DiseableMovement(float num)
    {
        moveSpeed = 0;
        eneableRotation = false;
        yield return new WaitForSeconds(num);
        moveSpeed = 5;
        eneableRotation = true;
    }
    
    IEnumerator EnergyTaking()
    {
        TakeEnergy = false;
        fillAmount -= 0.1f;
        yield return new WaitForSeconds(1.5f);
        TakeEnergy = true;
    }
    private void MovementLimitaitons()
    {
        Vector3 newPosition = this.gameObject.transform.position;
        if (this.gameObject.transform.position.x < minX)
        {
            newPosition.x = minX;
            this.gameObject.transform.position = newPosition;
        }
        if (this.gameObject.transform.position.z < minZ)
        {
            newPosition.z = minZ;
            this.gameObject.transform.position = newPosition;
        }
        if (this.gameObject.transform.position.x > maxX)
        {
            newPosition.x = maxX;
            this.gameObject.transform.position = newPosition;
        }
        if (this.gameObject.transform.position.z > maxZ)
        {
            newPosition.z = maxZ;
            this.gameObject.transform.position = newPosition;
        }
    }
    public void SleepingOnBed()
    {
        StartCoroutine(SecondsBeforeChangingData(1));
    }
}