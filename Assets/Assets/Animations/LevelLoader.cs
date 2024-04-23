using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private GameAddOns GameAddOns;
    private QuizManager QuizManager;
    private AnxityMeter AnxityMeter;

    public Animator transition;

    public float LoadTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        GameAddOns = FindAnyObjectByType<GameAddOns>();
        QuizManager = FindAnyObjectByType<QuizManager>();
        AnxityMeter = FindAnyObjectByType<AnxityMeter>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GameAddOns.GameOver == true)
        {
            StartCoroutine(LoadLevel(4));
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (AnxityMeter.Anxietyscore >= 100.01f)
        {
            StartCoroutine(LoadLevel(4));
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


        if (QuizManager.SCORE >= 4)
        {
            StartCoroutine(LoadLevel(5));
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameWinScreen") || SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameOverScreen"))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
        
    }

    public void ReLoadMainMenu()
    {
        StartCoroutine(LoadLevel(1));
    }

    IEnumerator LoadLevel(int LevelIndex)
    {
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(LoadTime);

        //LoadLevel scene
        SceneManager.LoadScene(LevelIndex);
    }


}