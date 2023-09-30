using System.Collections;
using UnityEngine;

public class AnimaationsPlayer : MonoBehaviour
{
    private Animator animator;
    private Player player;
    Timer timer;

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
            // Start the "IsForceSleeping" animation
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
                StartCoroutine(ResetStandingUpAfterDelay(2.0f));
            }
        }
    }
    private IEnumerator ResetStandingUpAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetBool("IsStandingUp", false);
    }

}
