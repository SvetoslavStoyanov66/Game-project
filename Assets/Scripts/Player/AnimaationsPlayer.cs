﻿using System.Collections;
using UnityEngine;

public class AnimaationsPlayer : MonoBehaviour
{
    private Animator animator;
    private Player player;
    Timer timer;
    [SerializeField]
    WateringAnim wateringAnim;

    void Start()
    {
        // Get the Animator component from the same game object
        animator = GetComponent<Animator>();

        // Find the Player script in the scene
        player = FindObjectOfType<Player>();
    }

    void Update()
    {
        // Retrieve movement inputs from the Player script
        float horizontal = player.GetHorizontalInput();
        float vertical = player.GetVerticalInput();

        // Determine if the character is walking
        bool isWalking = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;

        // Set the "walking" parameter in the animator
        animator.SetBool("isWalking", isWalking);
        
        
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
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsUsingHoe", false);
    }
    private IEnumerator ResetWatering(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsWatering", false);
        if (wateringAnim != null)
        {
            wateringAnim.WateringCanAnim(false);
        }
    }
    private IEnumerator Sleeping(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsSleeping", false);
    }
}
