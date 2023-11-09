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
       notiText.text = "Press E to browse travel locations";
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
      mainCamera.CameraFollowingCar();
      travelMapUI.gameObject.SetActive(false);
      StartCoroutine(WaitForAnimations());
   }
   IEnumerator WaitForAnimations()
   {
      darking.animator.SetBool("IsDarken", true);
      yield return new WaitForSeconds(0.7f);
      animator.SetBool("isDriving", true);
      yield return new WaitForSeconds(5f);
      animator.SetBool("isDriving", false);
      darking.animator.SetBool("IsDarken", false);
   }
}
