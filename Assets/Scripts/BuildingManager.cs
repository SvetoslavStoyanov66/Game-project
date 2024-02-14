using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using JetBrains.Annotations;
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
   [SerializeField]
   ExitDoorForAnimalsBuildings coopExitDoor;
   [SerializeField]
   GameObject cowSelection;
   [SerializeField]
   Camera cowCamera;
   [SerializeField]
   Light cowLight;
   [SerializeField]
   GameObject cowNotifier;

   [SerializeField]
   GameObject cowInteriorDoor;
   [SerializeField]
   ExitDoorForAnimalsBuildings cowExitDoor;
   public bool coopActive = false;
   public bool cowBuildingActive = false;
   [SerializeField]

   Canvas animalCanvas;
   [SerializeField]

   Canvas animalCanvas2;
   [SerializeField]
   GameObject farm;
   int buildingPrice;

   string structureType;
   public Vector3 coopVector;
   public Vector3 cowShedVector;

   public Vector3 coopQuaternion;
   public Vector3 cowShedQuaternion;

   
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

        }
    }
    public void BuildingAssigning(GameObject building, GameObject actualBuilding,string type,int price)
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
        structureType = type;
        buildingPrice = price;
    }
    public void SaveChangesButton()
    {
        if (actulaStructure != null && coopExitDoor != null)
{
    EnterDoorForAnimalsBuildings door = actulaStructure.GetComponent<EnterDoorForAnimalsBuildings>();
    if (door != null)
    {
        if(actulaStructure.name == "Chiken farm(Clone)")
        {
            door.Assigment(coopSelection, coopCamera, coopLight, sun, notifier, coopInteriorDoor,animalCanvas);
        }
        else
        {
            door.Assigment(cowSelection, cowCamera, cowLight, sun ,cowNotifier, cowInteriorDoor,animalCanvas2);
        }
        
    }

    Transform exitDoorTransform = actulaStructure.gameObject.transform;
    if (exitDoorTransform.childCount > 0)
    { 
        
             GameObject childObject = exitDoorTransform.GetChild(0).gameObject;
              if (childObject != null)
               {
                   if(actulaStructure.name == "Chiken farm(Clone)")
                    {
                        coopExitDoor.DoorAssigment(childObject);
                    }
                    else
                    {
                        cowExitDoor.DoorAssigment(childObject);
                    }
               }
       
    }
    if(structureType == "chicken")
    {
        coopActive = true;
        coopVector = actulaStructure.transform.position;
        coopQuaternion = actulaStructure.transform.eulerAngles;
    }
    else
    {
        cowBuildingActive = true;
        cowShedVector = actulaStructure.transform.position;
        cowShedQuaternion = actulaStructure.transform.eulerAngles;
    }
    
    
}

        
      
       

        cameraForBuilding.enabled = false;
        inventoryCanvas.enabled = true;
        timerCanvas.enabled = true;
        buildingModeCanvas.enabled = false;
        farm.SetActive(false);
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
        farm.SetActive(false);
        Money.Instance.moneyAmount += buildingPrice;
    }
     public void CanInstantiante(bool can)
    {
        canInstantiate = can;
    }
    public bool isThereActiveCoop()
    {
        return coopActive;
    }
    public bool isThereActiveCowBuilding()
    {
        return cowBuildingActive;
    }
    public void SaveBuildingDoor(GameObject Structure)
    {
        EnterDoorForAnimalsBuildings door = Structure.GetComponent<EnterDoorForAnimalsBuildings>();
    if (door != null)
    {
        if(Structure.name == "Chiken farm(Clone)")
        {
            door.Assigment(coopSelection, coopCamera, coopLight, sun, notifier, coopInteriorDoor,animalCanvas);
        }
        else
        {
            door.Assigment(cowSelection, cowCamera, cowLight, sun ,cowNotifier, cowInteriorDoor,animalCanvas2);
        }
        
    }

    Transform exitDoorTransform = Structure.gameObject.transform;
    if (exitDoorTransform.childCount > 0)
    { 
        
             GameObject childObject = exitDoorTransform.GetChild(0).gameObject;
              if (childObject != null)
               {
                   if(Structure.name == "Chiken farm(Clone)")
                    {
                        coopExitDoor.DoorAssigment(childObject);
                    }
                    else
                    {
                        cowExitDoor.DoorAssigment(childObject);
                    }
               }
       
    }
    }

   
}
