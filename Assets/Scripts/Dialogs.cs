using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogs : MonoBehaviour
{
    public static Dialogs Instance { get; set; }
    Text dialogText;
    string[] lines;
    float textSpeed = 0.05f;
    int index = 0;
    bool needToBreak = false;
    [SerializeField]
    Button browse;
    [SerializeField]
    Button leave;
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
    }
    void Update()
    {

    }
    public void StartDialog(string[] dialogArray)
    {
        lines = dialogArray;
        index = 0;
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            if (needToBreak)
            {
                needToBreak = false;
                break;
            }
            dialogText.text += c;
            yield return new WaitForSeconds(textSpeed);
            if ((index == lines.Length - 1) && dialogText.text == lines[index])
            {
                index = 0;
                browse.gameObject.SetActive(true);
                leave.gameObject.SetActive(true);
            }
            else
            {
                browse.gameObject.SetActive(false);
                leave.gameObject.SetActive(false);
            }

        }
    }
    public void nextPage()
    {
        if (index < lines.Length)
        {
            if(dialogText.text != lines[index])
            {
                needToBreak = true;
                dialogText.text = lines[index];
            }
            else
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
    }


}
