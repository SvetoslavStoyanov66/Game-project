using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BuildingManager : MonoBehaviour
{
    [SerializeField]
    GameObject startingPosition;
    GameObject structure;
    GameObject interior;
    GameObject actulaStructure;
    [SerializeField]
    Camera cameraForBuilding;
    [SerializeField]
    Canvas inventoryCanvas;
    [SerializeField]
    Canvas timerCanvas;
    [SerializeField]
    Canvas buildingModeCanvas;
    
    GameObject enterDoor;
    
    GameObject exitDoor;
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
                interior.transform.rotation = rotationForInstantiation;
                positionForInstantiation.y += 0.25f;
                interior.transform.position = positionForInstantiation;
                interior = Instantiate(interior);
                actulaStructure = Instantiate(actulaStructure);
                SetDoorsPOsitions();
                interior.SetActive(false);               
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                structure.transform.Rotate(0, 0, 90);
            }

        }
    }
    public void BuildingAssigning(GameObject building, GameObject actualBuilding, GameObject interiorToInstantiante, GameObject doorEnter,GameObject doorExit)
    {
        enterDoor = doorEnter;
        exitDoor = doorExit;
        interior = interiorToInstantiante;
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
    private void SetDoorsPOsitions()
    {
        enterDoor.SetActive(true);
        exitDoor.SetActive(true);
        Transform transformStructure = actulaStructure.transform.GetChild(0);
        enterDoor.transform.position = transformStructure.position;
        Transform transformInterior = interior.transform.GetChild(0);
        exitDoor.transform.position = transformInterior.position;
    }
}
