using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Items/Food")]
public class FoodData : ItemData
{
      [System.Serializable]
    public struct QuestionAndAnswers
    {
        public string question;
        public string correctAnswer;
        public string wrongAnswer;
        
        public string wrongAnswer2;
    }
    public int energyFillAmount;
    public int sellPrice;
    public QuestionAndAnswers quiz1;
    public QuestionAndAnswers quiz2;
    public QuestionAndAnswers quiz3;
}
