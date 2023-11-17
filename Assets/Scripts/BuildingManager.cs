using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    GameObject startingPosition;
    GameObject structure;
    public GameObject interior;
    public GameObject actulaStructure;
    [SerializeField]
    Camera cameraForBuilding;
    [SerializeField]
    Canvas inventoryCanvas;
    [SerializeField]
    Canvas timerCanvas;
    [SerializeField]
    Canvas buildingModeCanvas;
    
   [SerializeField]
   GameObject coopSelection;
   [SerializeField]
   Camera coopCamera;
   [SerializeField]
   Light coopLight;
   [SerializeField]
   Light sun;
   [SerializeField]
   GameObject notifier;

   [SerializeField]
   GameObject coopInteriorDoor;

   
    float moveSpeed = 5.0f;
    private bool canInstantiate = true;

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
            if (Input.GetMouseButton(0) && canInstantiate)
            {
                Vector3 positionForInstantiation = structure.transform.position;
                Quaternion rotationForInstantiation = structure.transform.rotation;
                Destroy (structure);

                actulaStructure.transform.rotation = rotationForInstantiation;
                actulaStructure.transform.position = positionForInstantiation;


                positionForInstantiation.y += 0.1f;
                actulaStructure = Instantiate(actulaStructure);              
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                structure.transform.Rotate(0, 0, 90);
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
        inventoryCanvas.enabled = false;
        timerCanvas.enabled = false;
        buildingModeCanvas.enabled = true;
    }
    public void SaveChangesButton()
    {
        EnterDoorForAnimalsBuildings door = actulaStructure.GetComponent<EnterDoorForAnimalsBuildings>();
        door.Assigment(coopSelection, coopCamera, coopLight, sun, notifier, coopInteriorDoor);

        cameraForBuilding.enabled = false;
        inventoryCanvas.enabled = true;
        timerCanvas.enabled = true;
        buildingModeCanvas.enabled = false;
    }
    public void LeaveButton()
    {
        if (structure != null)
        {
            Destroy(structure);
        }
        else if (actulaStructure != null)
        {
            Destroy(actulaStructure);
        }
        cameraForBuilding.enabled = false;
        inventoryCanvas.enabled = true;
        timerCanvas.enabled = true;
        buildingModeCanvas.enabled = false;
    }
     public void CanInstantiante(bool can)
    {
        canInstantiate = can;
    }
   
}
