using UnityEngine;

public class AnimaationsPlayer : MonoBehaviour
{
    private Animator animator;
    private Player player;

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

        // Determine if the character is turning left
        bool isTurningLeft = horizontal < -0.1f && Mathf.Abs(vertical) < 0.1f;

        // Set the "turnLeft" parameter in the animator
        animator.SetBool("isTurningLeft", isTurningLeft);
    }

}
