using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class Car : MonoBehaviour
{
   [SerializeField]
   MainCamera mainCamera;
   [SerializeField]
   GameObject notifier;
   [SerializeField]
   GameObject travelMapUI;
   Animator animator;
   [SerializeField]
   Darking darking;
   [SerializeField]
   TownEnter transporting;
   private void Start() 
   {
       animator = this.gameObject.GetComponent<Animator>();
   }
   private void Update()
   {
       if(notifier.activeSelf && Input.GetKeyDown(KeyCode.E) && !travelMapUI.gameObject.activeSelf)
       {
         travelMapUI.gameObject.SetActive(true);
         notifier.SetActive(false);
       }
       else if(Input.GetKeyDown(KeyCode.E))
       {
         travelMapUI.gameObject.SetActive(false);
       }
   }


   private void OnTriggerEnter(Collider other)
   {
       notifier.SetActive(true);
       Text notiText = notifier.GetComponentInChildren<Text>();
       notiText.text = "Натисни Е за да избереш локация за пътуване";
   }
   private void OnTriggerExit(Collider other)
   {
       notifier.SetActive(false);
       Text notiText = notifier.GetComponentInChildren<Text>();
       notiText.text = "";
   }
   public void TriggerCarAnimations()
   {
      transporting.Transporting();
      if(gameObject.name == "Pickup truck")
      {
        mainCamera.CameraFollowingCar();
      }
      else
      {
        mainCamera.CameraFollowingCar2();
      }
      travelMapUI.gameObject.SetActive(false);
      StartCoroutine(WaitForAnimations());
   }
   IEnumerator WaitForAnimations()
   {
      darking.animator.SetBool("IsDarken", true);
      yield return new WaitForSeconds(0.9f);
      animator.SetBool("isDriving", true);
      yield return new WaitForSeconds(3f);
      darking.animator.SetBool("IsDarken", false);
      GameManager gameManager = FindObjectOfType<GameManager>();
    if (gameObject.name == "Pickup truck" && gameManager.spriteDisplay != null && SaveSystem.isSafeSlotEmpty(SaveSystem.slot))
    {
      gameManager.EneableTownTurtorial();
    }
      yield return new WaitForSeconds(1f);
      animator.SetBool("isDriving", false);

  }
}
