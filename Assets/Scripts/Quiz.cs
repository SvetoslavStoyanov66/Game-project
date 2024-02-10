
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class Quiz : MonoBehaviour
{
    [SerializeField]
    Button button;
    [SerializeField]
    Button button1;
    [SerializeField]
    Button button2;
    private int reward = 0;
    List<FoodData.QuestionAndAnswers> questionAndAnswers = new List<FoodData.QuestionAndAnswers>();
    [SerializeField]
    GameObject notifier;
    bool canInteract = false;
    bool cantClose = false;
    bool cantUseDialog = false;
    public bool isQuestioinForDayUsed = false;

    [SerializeField]
    GameObject dialogUI;
    const int maxQuestions = 3;
    string question;
    string correctAnswer;
    string[] quizDialog = {
        "Здравей! Като бизнесмен в селското стопанство, искам да проверя знанията ти за земеделието.",
        " Подготвен ли си за малко въпроси? При правилни отговори, те очаква възнаграждение.",
        "Какво ще правиш?"};
    string quizCorrectAnswerResponse = $"Чудесно се справихте! Вие наистина знаете тайните на земеделието. Заповядайте вашата награда за знанието!";
    string quizIncorrectAnswerResponse = "Жалко, не успяхте този път. Но в земеделието, както и в живота, всяка грешка е урок. Нека това не ви обезкуражава, а ви мотивира да опитате отново!";
    string noQuestionsForQuizResponse = "Изглежда, че все още нямате отключени въпроси. За да продължите, трябва първо да си отгледате растенията. Всеки зеленчук носи нови знания. Грижете се за вашата градина, и скоро ще можете да се върнете за нови предизвикателства!";
    string noAttemptsForQuizResponse = "Изчерпахте днешния си опит. Върнете се утре за нов шанс. Всяко ново утро носи нови възможности!";
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
    IEnumerator waitForDialog3(string answer)
    {
        string[] strings = new string[1];
        strings[0] = answer;
        yield return new WaitForEndOfFrame();
        Dialogs.Instance.StartDialog(strings, "Quiz3");
    }

    private void ButtonAndRewardAssignment()
    {
        if (questionAndAnswers.Count > 0)
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

            reward = questionAndAnswers.Count * 10;
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
        if (questionAndAnswers != null && questionAndAnswers.Count > 0 && !isQuestioinForDayUsed)
        {
            DialogSizeChange(1550, 500);
            Dialogs.Instance.ResetText();
            StartCoroutine(waitForDialog2());
            isQuestioinForDayUsed = true;
        }
        else if (questionAndAnswers.Count == 0)
        {
            Dialogs.Instance.ResetText();

            StartCoroutine(waitForDialog3(noQuestionsForQuizResponse));
        }
        else if (isQuestioinForDayUsed)
        {
            Dialogs.Instance.ResetText();

            StartCoroutine(waitForDialog3(noAttemptsForQuizResponse));
        }
    }
    public void AnswerButtonFunction(Text buttonText)
    {
        Dialogs.Instance.ResetText();
        if (buttonText.text == correctAnswer)
        {
            Money.Instance.moneyAmount += reward;
            StartCoroutine(waitForDialog3(quizCorrectAnswerResponse));
        }
        else
        {
            StartCoroutine(waitForDialog3(quizIncorrectAnswerResponse));
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
        rectTransform.anchoredPosition = new Vector2(0, -99);
        Transform textTransform = dialogUI.transform.GetChild(0);
        RectTransform textReactTransform = textTransform.gameObject.GetComponent<RectTransform>();
        textReactTransform.anchoredPosition = new Vector2(0, 101);
        textReactTransform.sizeDelta = new Vector2(1320, 180);
    }
}
