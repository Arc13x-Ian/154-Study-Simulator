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

    //fade text variables 
    public float fadeDuration = 1f;
    public float displayDuration = 2f;


    public void Anser()
    {
        if (isCorrect)
        {

            CorrectText.text = "Correct";
            ++quizManager.CorrectAnswerIndex;
            StartCoroutine(FadeText());
            quizManager.correct();
        }
        else
        {
            CorrectText.text = "Wrong";
            StartCoroutine(FadeText());
            quizManager.correct();
        }
    }


    //fade functions 
    private IEnumerator FadeText()
    {
        // Fade in
        yield return Fade(0f, 1f, fadeDuration, CorrectText);

        // Wait for display duration
        yield return new WaitForSeconds(displayDuration);

        // Fade out
        yield return Fade(1f, 0f, fadeDuration, CorrectText);

        // You can add more logic here if needed
    }

    private IEnumerator Fade(float startAlpha, float endAlpha, float duration, TextMeshProUGUI text)
    {
        Color color = text.color;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            color.a = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            text.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = endAlpha;
        text.color = color;
    }




}
