using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dialogs : MonoBehaviour
{
    public static Dialogs Instance { get; set; }
    Text dialogText;
    string[] lines;
    float textSpeed = 0.05f;
    public int index = 0;
    bool needToBreak = false;
    Button browse;
    Button leave;
    [SerializeField]
    Button browseSeeds;
    [SerializeField]
    Button LeaveSeeds;
    [SerializeField]
    Button browseBuildings;
    [SerializeField]
    Button leaveBuildings;

    [SerializeField]
    Button browseAnimals;
    [SerializeField]
    Button leaveAnimals;
    [SerializeField]
    Animator AnimalShopNPCAnim;
    [SerializeField]
    Animator BuildingShopNPCAnim;
    [SerializeField]
    Animator SeedShopNPCAnim; 
    Animator npcAnimator;
    bool canChangePage = true;
    [SerializeField]
    Button BrowseAnimalsFoods;
    string name;
    RectTransform leaveRect;
    RectTransform browseRect;
    RectTransform browseAnimalsFoodsRect;
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
        dialogText = GetComponentInChildren<Text>();
        dialogText.text = string.Empty;
        index = 0;
    }
    public void StartDialog(string[] dialogArray, string name)
    {
        this.name = name;
        if (name == "SeedShop")
        {
            browse = browseSeeds;
            leave = LeaveSeeds;
            npcAnimator = SeedShopNPCAnim;
        }
        else if (name == "BuildingShop")
        {
            browse = browseBuildings;
            leave = leaveBuildings;
            npcAnimator = BuildingShopNPCAnim;
        }
        else if(name == "AnimalsShop")
        {
            browse = browseAnimals;
            leave = leaveAnimals;
            npcAnimator = AnimalShopNPCAnim;
        }
        lines = dialogArray;
        index = 0;
        StartCoroutine(TypeLine());
        npcAnimator.SetBool("isTalking", true);
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            if (needToBreak)
            {
                npcAnimator.SetBool("isTalking", false);
                needToBreak = false;
                break;
            }
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
         if ((index == lines.Length - 1) && dialogText.text == lines[index])
            {
                canChangePage = false;
                index = 0;
                browse.gameObject.SetActive(true);
                if(name == "AnimalsShop")
                {
                    leaveRect = leave.GetComponent<RectTransform>();
                    browseRect = browse.GetComponent<RectTransform>();
                    browseAnimalsFoodsRect = BrowseAnimalsFoods.GetComponent<RectTransform>();
                    BrowseAnimalsFoods.gameObject.SetActive(true);
                    leaveRect.anchoredPosition = new Vector2(leaveRect.anchoredPosition.x, -68f);
                    browseRect.anchoredPosition = new Vector2(browseRect.anchoredPosition.x, 27f);
                    browseAnimalsFoodsRect.anchoredPosition = new Vector2(browseAnimalsFoodsRect.anchoredPosition.x, -19f);
                }
                leave.gameObject.SetActive(true);
                
            }
            else
            {
                browse.gameObject.SetActive(false);
                BrowseAnimalsFoods.gameObject.SetActive(false);
                if(name == "AnimalsShop")
                {
                    leaveRect = leave.GetComponent<RectTransform>();
                    browseRect = browse.GetComponent<RectTransform>();
                    browseAnimalsFoodsRect = BrowseAnimalsFoods.GetComponent<RectTransform>();
                    leaveRect.anchoredPosition = new Vector2(leaveRect.anchoredPosition.x, -49f);
                    browseRect.anchoredPosition = new Vector2(browseRect.anchoredPosition.x, -1f);
                }
                leave.gameObject.SetActive(false);
            }
    }
    public void nextPage()
{
    if (index < lines.Length && canChangePage)
    {
        if (dialogText.text != lines[index])
        {
            needToBreak = true;
            dialogText.text = lines[index];
        }
        else
        {
            if (index < lines.Length - 1)
            {
                index++;
                dialogText.text = string.Empty;
                StartCoroutine(TypeLine());
                npcAnimator.SetBool("isTalking", true);
            }
            else
            {
                canChangePage = false;
            }
        }
    }
}
    public void ResetText()
    {
        dialogText.text = string.Empty;
        browse.gameObject.SetActive(false);
        leave.gameObject.SetActive(false);
        BrowseAnimalsFoods.gameObject.SetActive(false);
        canChangePage = true;
    }


}
