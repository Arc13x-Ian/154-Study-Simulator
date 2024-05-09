using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<StudyQuestions> QnA;
    public GameObject[] options;
    public int currentQuestions;
    public int CorrectAnswerIndex = 0;
    public int SCORE = 0;
    public int QuizLimit;


    public TextMeshProUGUI Questiontxt;
    public TextMeshProUGUI score;
    public TextMeshProUGUI score2;

    

    //reference to Player Variables
    public GameAddOns GameAddOns;

    private void Start()
    {
        generateQuestions();



        //we get the player script from somewhere from the scene
        GameAddOns = FindAnyObjectByType<GameAddOns>();

    }

    private void Update()
    {
        int scoreindex = 0;
        int scoreindex2 = 0;

        if (GameAddOns.StudyDone == false)
        {
            if (QuizLimit >= 4)
            {
                GameAddOns.StudyDone = true;
                GameAddOns.StudyScreen.SetActive(false);
                SCORE = SCORE + CorrectAnswerIndex;
                QuizLimit = 0;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameAddOns.canMove = true;

            }
        }
        if(QuizLimit >= 4)
            {   
            GameAddOns.StudyDone = true;
            GameAddOns.StudyScreen.SetActive(false);
            SCORE = SCORE + CorrectAnswerIndex;
            QuizLimit = 0;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameAddOns.canMove = true;

        }

        scoreindex2 = scoreindex2 + CorrectAnswerIndex;
        score2.text = scoreindex2.ToString();

        scoreindex = scoreindex + SCORE;
        score.text = scoreindex.ToString();
        

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
        
        QnA.RemoveAt(currentQuestions);
        
        generateQuestions();

    }



    void generateQuestions()
    {
        if (QnA.Count == 0)
        {
            //Player.StudyScreen.SetActive(false);
            GameAddOns.StudyDone = true;
            Questiontxt.text = null;
            
        }



        currentQuestions = Random.Range(0, QnA.Count);
        //Debug.Log(QnA.Count);
        

        Questiontxt.text = QnA[currentQuestions].Questions;
        setAnswers();


    }

}
