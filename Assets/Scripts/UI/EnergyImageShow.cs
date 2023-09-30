using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class EnergyImageShow : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image imageToActivate;

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Activate the desired image when the mouse hovers over this image
        imageToActivate.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Deactivate the desired image when the mouse exits this image
        imageToActivate.gameObject.SetActive(false);
    }
}
