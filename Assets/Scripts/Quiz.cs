 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
   [SerializeField] TextMeshProUGUI questionText;
   [SerializeField] QuestionSO question;

   [Header ("Answers")]
   [SerializeField] GameObject[] answerButtons;
   [SerializeField]bool hasAnswerEarly;
   int correctAnswerIndex;
   [Header ("Buttons Colors")]
   [SerializeField]Sprite defaultAnswerSprite;
   [SerializeField]Sprite correctAnswerSprite;
   [Header ("Timer")]
   [SerializeField] Image timerImage;
   Timer timer;
   private void Start() {

        DisplayQuestion();
        timer = FindObjectOfType<Timer>();
   }
   private void Update() {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion){
            hasAnswerEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!hasAnswerEarly && !timer.isAnsweringQuestion ){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
   }
        Image buttonImage;
   public void OnAnswerSelected(int index){
    hasAnswerEarly = true;
    DisplayAnswer(index);
    SetButtonState(false);
    timer.CancelTimer();
   }
   void DisplayAnswer (int index)
   {
        Image buttonImage;
        if (index == question.GetCorrectAnswerIndex()){
        questionText.text = "Correct!!!";
        buttonImage  = answerButtons[index].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    } else {
        correctAnswerIndex = question.GetCorrectAnswerIndex();
        string correctAnswer = question.GetAnswer(correctAnswerIndex);
        questionText.text = "Sorry , the correct answer was :\n" + correctAnswer; 
        buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
        buttonImage.sprite = correctAnswerSprite;
    }
   }
   void GetNextQuestion (){
    SetButtonState(true);
    SetDefaultButtonSprites();
    DisplayQuestion();
   }
    void DisplayQuestion(){
        questionText.text = question.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++){
        TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
        buttonText.text = question.GetAnswer(i);
        }
    }
void SetButtonState (bool state){
    for (int i = 0; i < answerButtons.Length; i++){
        Button button = answerButtons[i].GetComponent<Button>();
        button.interactable = state;
}
}
void SetDefaultButtonSprites(){
        for (int i = 0; i < answerButtons.Length; i++){
        buttonImage  = answerButtons[i].GetComponent<Image>();
        buttonImage.sprite = defaultAnswerSprite;
}
}
}


