using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringAnim : MonoBehaviour
{
    private Animator animator;

    public void WateringCanAnim(bool action)
    {
        animator = GetComponent<Animator>();
        Debug.Log(animator.name);
        animator.SetBool("IsWatering", action);
    }
}
