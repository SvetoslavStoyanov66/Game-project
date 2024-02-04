using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    public float moveSpeedScale = 1.0f;
    public float moveSpeed = 5.0f;
    public float fillAmount = 1.0f;  // Corrected typo in variable name
    public Image energyFill;
    public Text energyAmountText;
    public float gravity = 9.81f;
    public Timer timer;
    private float timer1 = 0f;
    int counter;
    bool TakeEnergy = true;
    private bool eneableRotation = true;
    public GameObject indicator;
    public GameObject indicator2;
   
    // Reference to the Timer script
    [SerializeField]
    GameObject dilaogUI;
    [SerializeField]
    Canvas shopForBuildingsCanvas;
    [SerializeField]
    Canvas shopForSeedsCanvas;
    [SerializeField]
    Canvas shopForAnimalsCanvas;
    [SerializeField]
    Canvas buildingModeCanvas;
    public bool eneableMovememt = true;




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
            transform.Translate(movementDirection * moveSpeed * moveSpeedScale * Time.deltaTime, Space.World);

            if (eneableRotation == true)
            {
                Quaternion targetRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.23f);
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
         if(dilaogUI.activeSelf || shopForAnimalsCanvas.isActiveAndEnabled || shopForBuildingsCanvas.isActiveAndEnabled || buildingModeCanvas.isActiveAndEnabled || shopForSeedsCanvas.isActiveAndEnabled)
        {
           Animator animator = animationsPlayer.gameObject.GetComponent<Animator>();
           animator.enabled = false;
           this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
           moveSpeed = 0;
           eneableRotation = false;
        }
        else if(!eneableMovememt)
        {
            moveSpeed = 0;
            eneableRotation = false;
        }
        else
        {
            Animator animator = animationsPlayer.gameObject.GetComponent<Animator>();
            animator.enabled = true;   
            moveSpeed = 5;
            eneableRotation = true;
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
        eneableMovememt = false;
        eneableRotation = false;
        yield return new WaitForSeconds(num);
        eneableRotation = true;
        eneableMovememt = true;
    }
    
    IEnumerator EnergyTaking()
    {
        TakeEnergy = false;
        fillAmount -= 0.1f;
        yield return new WaitForSeconds(1.5f);
        TakeEnergy = true;
    }
    public void SleepingOnBed()
    {
        StartCoroutine(SecondsBeforeChangingData(1));
    }
}