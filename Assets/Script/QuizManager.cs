using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizManager : MonoBehaviour
{
    public List<StudyQuestions> QnA;
    public GameObject[] options;
    public int currentQuestions;


    public TextMeshProUGUI Questiontxt;

    private void Start()
    {
        generateQuestions();
    }
    
    void setAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answer>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestions].Answers[i];

            if(QnA[currentQuestions].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<Answer>().isCorrect = true;
            }
        }
    }

    public void correct()
    {
        generateQuestions();
        QnA.RemoveAt(currentQuestions);
    }



    void generateQuestions()
    {
        currentQuestions = Random.Range(0, QnA.Count);
        Debug.Log(QnA.Count);
        if (QnA.Count < 1)
        {
            Destroy(gameObject);
        }

        Questiontxt.text = QnA[currentQuestions].Questions;
        setAnswers();


    }

}
