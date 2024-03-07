using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void StartGame(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }
}
