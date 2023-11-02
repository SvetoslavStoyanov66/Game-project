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
    bool canChangePage = true;
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
    void Update()
    {

    }
    public void StartDialog(string[] dialogArray, string name)
    {
        if (name == "SeedShop")
        {
            browse = browseSeeds;
            leave = LeaveSeeds;
        }
        else if (name == "BuildingShop")
        {
            browse = browseBuildings;
            leave = leaveBuildings;
        }
        lines = dialogArray;
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            if ((index == lines.Length - 1) && dialogText.text == lines[index])
            {
                canChangePage = false;
                index = 0;
                browse.gameObject.SetActive(true);
                leave.gameObject.SetActive(true);
                
            }
            else
            {
                browse.gameObject.SetActive(false);
                leave.gameObject.SetActive(false);
            }
            if (needToBreak)
            {
                needToBreak = false;
                break;
            }
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    public void nextPage()
    {
        if (index < lines.Length)
        {
            if (dialogText.text != lines[index] && canChangePage)
            {
                needToBreak = true;
                dialogText.text = lines[index];
            }
            else if (canChangePage)
            {
                index++;
                dialogText.text = string.Empty;
                StartCoroutine(TypeLine());
            }
        }
    }
    public void ResetText()
    {
        dialogText.text = string.Empty;
        browse.gameObject.SetActive(false);
        leave.gameObject.SetActive(false);
        canChangePage = true;
    }


}
