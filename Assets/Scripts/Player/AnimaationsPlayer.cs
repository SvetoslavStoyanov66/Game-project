using System.Collections;
using System.Data.Common;
using UnityEngine;

public class AnimaationsPlayer : MonoBehaviour
{
    private Animator animator;
    private Player player;
    Timer timer;
    [SerializeField]
    WateringAnim wateringAnim;
    [SerializeField]
    private AudioClip wateringSound;
    [SerializeField]
    private AudioClip hoeSound; 
    [SerializeField]
    private AudioClip movementSound;
    private AudioSource audioSource1;
    private AudioSource audioSource2;

    void Start()
    {
        // Get the Animator component from the same game object
        animator = GetComponent<Animator>();

        // Find the Player script in the scene
        player = FindObjectOfType<Player>();
        AudioSource[] audioSources = GetComponents<AudioSource>();
        if (audioSources.Length >= 2)
        {
            audioSource1 = audioSources[0];
            audioSource2 = audioSources[1];
        }
        audioSource1.clip = movementSound;

    }

    void Update()
    {
        // Retrieve movement inputs from the Player script
        float horizontal = player.GetHorizontalInput();
        float vertical = player.GetVerticalInput();

        // Determine if the character is walking
        bool isWalking = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        animator.SetBool("isWalking", isWalking);
        if(isWalking && !audioSource1.isPlaying)
        {
            audioSource1.Play();
        }
        else if(!isWalking && audioSource1.isPlaying)
        { 
            audioSource1.clip = movementSound;
            audioSource1.Stop();
        }
        
        
    }
    public void TriggerAnimations()
    {
        if (player.fillAmount <= 0)
        {
            animator.SetBool("IsForceSleeping", true);
            animator.SetBool("IsStandingUp", false);
        }
        else
        {
            // If we were in the sleeping state, reset it and trigger standing up
            if (animator.GetBool("IsForceSleeping"))
            {
                animator.SetBool("IsForceSleeping", false);
                animator.SetBool("IsStandingUp", true);
                StartCoroutine(ResetStandingUpAfterDelay(2));
            }
        }
    }
    public void BedSleeping()
    {
        animator.SetBool("IsSleeping", true);
        StartCoroutine(Sleeping(3));
    }
    private IEnumerator ResetStandingUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsStandingUp", false);
    }
    public void HoeUsageAnimations()
    {
        animator.SetBool("IsUsingHoe", true);
        StartCoroutine(ResetHoe(1.5f));
    }
    public void Watering()
    {
        if (wateringAnim != null)
        {
            wateringAnim.WateringCanAnim(true);
        }
        animator.SetBool("IsWatering", true);
        StartCoroutine(ResetWatering(2.5f));
    }
    private IEnumerator ResetHoe(float delay)
    {
        audioSource2.clip = hoeSound;
        yield return new WaitForSeconds(0.75f);
        audioSource2.Play();
        yield return new WaitForSeconds(delay - 0.75f);
        animator.SetBool("IsUsingHoe", false);
        audioSource2.Stop();
    }
    private IEnumerator ResetWatering(float delay)
    {
        audioSource2.clip = wateringSound;
        audioSource2.Play();
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsWatering", false);
        if (wateringAnim != null)
        {
            wateringAnim.WateringCanAnim(false);
            audioSource2.Stop();
        }
    }
    private IEnumerator Sleeping(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsSleeping", false);
    }
}
