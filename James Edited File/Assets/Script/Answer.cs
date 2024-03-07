using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Answer : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;

    //display a Correct text and then fade it out so they can move on to the next question
    public TextMeshProUGUI CorrectText;


    public void Anser()
    {

        if (isCorrect)
        {

            CorrectText.text = "Correct";
            quizManager.correct();
        }
        else
        {
            CorrectText.text = "Wrong";
            quizManager.correct();
        }

    }


}
