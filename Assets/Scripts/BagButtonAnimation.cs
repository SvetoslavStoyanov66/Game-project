using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagButtonAnimation : MonoBehaviour
{
    public static BagButtonAnimation Instance{get;set;}
    Animator animator;
      private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
   
    void Start()
    {
       animator = this.gameObject.GetComponent<Animator>(); 
    }
    public void PlayBagAnimation()
    {
        animator.Play("BagButtonAnimm",0,0);
        animator.Update(0);
    }
}
