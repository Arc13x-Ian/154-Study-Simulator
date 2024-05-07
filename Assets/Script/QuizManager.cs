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


    public TextMeshProUGUI Questiontxt;
    public TextMeshProUGUI score;

    

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
        int scoreindex = 4;

        if (GameAddOns.StudyDone == false)
        {
            if (CorrectAnswerIndex == 4)
            {
                GameAddOns.StudyDone = true;
                GameAddOns.StudyScreen.SetActive(false);
                CorrectAnswerIndex = 0;
                ++SCORE;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                GameAddOns.canMove = true;

            }
        }

        scoreindex = scoreindex - SCORE;
        score.text = scoreindex.ToString();


        if (SCORE >= 4)
        {
            GameAddOns.StudyDone = true;
            GameAddOns.StudyScreen.SetActive(false);
            //SceneManager.LoadScene("GameWinScreen");
        }
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
        CorrectAnswerIndex++;
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
