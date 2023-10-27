using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    GameObject startingPosition;
    GameObject structure;
    GameObject actulaStructure;
    [SerializeField]
    Camera cameraForBuilding;
    float moveSpeed = 5.0f;

    public static BuildingManager Instance { get;set; }
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


    void Update()
    {
        if (structure != null)
        {
            Vector3 position = Input.mousePosition;
            position.z = 10; // Set the z-coordinate to 10 (or your desired depth)
            Vector3 mousePosition = cameraForBuilding.ScreenToWorldPoint(position);
            structure.transform.position = new Vector3(mousePosition.x, structure.transform.position.y, mousePosition.z);
            Debug.Log(actulaStructure);
            if (Input.GetMouseButton(0))
            {
                Vector3 positionForInstantiation = structure.transform.position;
                Destroy (structure);
                actulaStructure.transform.position = positionForInstantiation;
                Instantiate(actulaStructure);
            }

        }
    }
    public void BuildingAssigning(GameObject building, GameObject actualBuilding)
    {
        actulaStructure = actualBuilding;
        structure = building;
        Vector3 vector3 = startingPosition.transform.position;
        structure.transform.position = vector3;
        structure = Instantiate(building);
        cameraForBuilding.enabled = true;
    }
}
