using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StructureInBuildingMode : MonoBehaviour
{
    [SerializeField]
    Material greenMaterial;
    [SerializeField]
    Material redMaterial;

    private void OnTriggerEnter(Collider other)
    {
        Material material = redMaterial;
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in childRenderers)
        {
            renderer.material = material;
        }
        BuildingManager.Instance.CanInstantiante(false);
    }
    private void OnTriggerExit(Collider other)
    {
        Material material = greenMaterial;
        Renderer[] childRenderers = GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in childRenderers)
        {
            renderer.material = material;
        }
        BuildingManager.Instance.CanInstantiante(true);
    }
}
