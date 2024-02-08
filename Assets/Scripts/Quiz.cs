
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Quiz : MonoBehaviour
{
    [SerializeField]
    Button button;
    [SerializeField]
    Button button1;
    [SerializeField]
    Button button2;
    int reward;
    List<FoodData.QuestionAndAnswers> questionAndAnswers = new List<FoodData.QuestionAndAnswers>();
    [SerializeField]
    GameObject notifier;
    bool canInteract = false;
    bool cantClose = false;
    bool cantUseDialog = false;
    [SerializeField]
    GameObject dialogUI;
    int questionsAsked = 0;
    const int maxQuestions = 3;
    string question;
    string correctAnswer;
    string[] quizDialog = {
        "Здравей! Като бизнесмен в селското стопанство, искам да проверя знанията ти за земеделието.",
        " Подготвен ли си за малко въпроси? При правилни отговори, те очаква възнаграждение.",
        "Какво ще правиш?"};
    public void AddQustionDataToList(FoodData.QuestionAndAnswers data)
    {
        questionAndAnswers.Add(data);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canInteract)
        {
            notifier.SetActive(false);
            if (!dialogUI.activeSelf)
            {
                dialogUI.SetActive(true);
                StartCoroutine(waitForDialog());

            }
            else
            {
                Dialogs.Instance.nextPage();
            }
        }
    }
    IEnumerator waitForDialog()
    {
        yield return new WaitForEndOfFrame();
        Dialogs.Instance.StartDialog(quizDialog, "Quiz");
    }
    IEnumerator waitForDialog2()
    {
        if (questionAndAnswers != null && questionAndAnswers.Count > 0)
        {
            ButtonAndRewardAssignment();
            string[] strings = new string[1];
            strings[0] = question;
            yield return new WaitForEndOfFrame();
            Dialogs.Instance.StartDialog(strings, "Quiz2");
        }
    }
    private void ButtonAndRewardAssignment()
    {
        if (questionAndAnswers.Count > 0 && questionsAsked < maxQuestions)
        {
            int index = UnityEngine.Random.Range(0, questionAndAnswers.Count);
            FoodData.QuestionAndAnswers selectedQA = questionAndAnswers[index];
            question = selectedQA.question;
            correctAnswer = selectedQA.correctAnswer;

            List<string> answers = new List<string> { selectedQA.correctAnswer, selectedQA.wrongAnswer, selectedQA.wrongAnswer2 };
            Shuffle(answers);

            button.GetComponentInChildren<Text>().text = answers[0];
            button1.GetComponentInChildren<Text>().text = answers[1];
            button2.GetComponentInChildren<Text>().text = answers[2];

            reward = questionAndAnswers.Count * 20;

            questionAndAnswers.RemoveAt(index);
            questionsAsked++;
        }
        else
        {
            EndQuiz();
        }
    }
    private void EndQuiz()
    {
        Dialogs.Instance.ResetText();
        dialogUI.SetActive(false);
    }
    private void Shuffle<T>(List<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = UnityEngine.Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
    public void AcceptTheChallangeButtom()
    {
        if (questionAndAnswers != null && questionAndAnswers.Count > 0)
        {
            DialogSizeChange(1450, 500);
            Dialogs.Instance.ResetText();
            if(dialogUI.activeSelf) 
            {
                StartCoroutine(waitForDialog2());
            }
        }
    }
    public void AnswerButtonFunction(Text buttonText)
    {
        if (buttonText.text == correctAnswer)
        {
            Dialogs.Instance.ResetText();
            ButtonAndRewardAssignment();
            StartCoroutine(waitForDialog2());
        }
    }
    private void OnTriggerEnter()
    {
        notifier.SetActive(true);
        canInteract = true;
    }
    private void OnTriggerExit()
    {
        notifier.SetActive(false);
        canInteract = false;

    }
    private void DialogSizeChange(int x, int y)
    {
        RectTransform rectTransform = dialogUI.gameObject.GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(x, y);
        rectTransform.anchoredPosition = new Vector2(0, -149);
        Transform textTransform = dialogUI.transform.GetChild(0);
        RectTransform textReactTransform = textTransform.gameObject.GetComponent<RectTransform>();
        textReactTransform.anchoredPosition = new Vector2(0, 71);
        textReactTransform.sizeDelta = new Vector2(1320, 180);
    }
}
