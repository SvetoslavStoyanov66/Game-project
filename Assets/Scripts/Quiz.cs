﻿
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
        ButtonAndRewardAssignment();
        string[] strings = new string[1];
        strings[0] = question;
        yield return new WaitForEndOfFrame();
        Dialogs.Instance.StartDialog(strings, "Quiz2");
    }
    private void ButtonAndRewardAssignment()
    {
        if (questionAndAnswers.Count > 0)
        {
            int index = UnityEngine.Random.Range(0, questionAndAnswers.Count);
            FoodData.QuestionAndAnswers selectedQA = questionAndAnswers[index];
            selectedQA.question = question;
            selectedQA.correctAnswer = correctAnswer;

            button.GetComponentInChildren<Text>().text = selectedQA.question;
            button1.GetComponentInChildren<Text>().text = selectedQA.question;
            button2.GetComponentInChildren<Text>().text = selectedQA.question;

            List<string> answers = new List<string> { selectedQA.correctAnswer, selectedQA.wrongAnswer, selectedQA.wrongAnswer2 };
            Shuffle(answers);

            button.GetComponentInChildren<Text>().text = answers[0];
            button1.GetComponentInChildren<Text>().text = answers[1];
            button2.GetComponentInChildren<Text>().text = answers[2];

            reward = questionAndAnswers.Count * 20;
        }
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
        Dialogs.Instance.ResetText();
        waitForDialog2();
    }
    public void AnswerButtonFunction(Text buttonText)
    {
        if(buttonText.text == correctAnswer)
        {
            Dialogs.Instance.ResetText();
            ButtonAndRewardAssignment();
            waitForDialog2();    
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
}
