using System.Collections;
using UnityEngine;

public class Darking : MonoBehaviour
{
    public Animator animator;
    public Player player;// Reference to the Animator controlling the darkening animation

    void Update()
    {
        if (player != null)
        {

            if (player.fillAmount <= 0)
            {
                StartDarkenAnimation();
            }
            Debug.Log(player.fillAmount);
        }
        
        
    }
    public void StartDarkenAnimation()
    {
        StartCoroutine(darking());
    }
    public IEnumerator darking()
    {
        animator.SetBool("IsDarken", true);
        yield return new WaitForSeconds(3);
       animator.SetBool("IsDarken", false);

    }
}
