using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlarmClock : MonoBehaviour
{
    private LevelLoader LevelLoader;



    // Start is called before the first frame update
    void Start()
    {
        LevelLoader = FindAnyObjectByType<LevelLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        LevelLoader.LoadNextLevel();

    }
}
