using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCanimations : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    
    public void ActivateAnimations()
    {
        animator.SetBool("talking" ,true);
    }
    public void DeactivateAnimations()
    {
        animator.SetBool("talking" ,false);
    }
}
