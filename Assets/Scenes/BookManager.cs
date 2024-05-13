using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    public GameAddOns GameAddOns;
    public AnxityMeter AnxityMeter;
    public GameObject[] Books = null;
    public GameObject[] BookStack = null;
    public int BookScore;
    public TextMeshProUGUI score;
    public float stackOffset;
    public GameObject StackSpot;
    public bool temp = true;

    public GameObject[] studySpots;


    // Start is called before the first frame update
    void Start()
    {
        GameAddOns = FindAnyObjectByType<GameAddOns>();
        AnxityMeter = FindAnyObjectByType<AnxityMeter>();
        stackOffset = StackSpot.transform.position.y;

        studySpots = GameObject.FindGameObjectsWithTag("StackSpot");
    }

    // Update is called once per frame
    void Update()
    {
        getBookList();


    }

    public void placingBookDown()
    {
        if (GameAddOns.isPlacingDown)
        {
            BookScore = BookScore + 1;
            score.text = BookScore.ToString();
            StackBookObject(BookStack);
        }

    }

    private void getBookList()
    {
        List<GameObject> validBooks = new List<GameObject>(); // Create a list to store valid GameObjects

        for (int i = 0; i < Books.Length; i++)  // Iterate through the Books array
        {
            if (Books[i] != null) // Check if the GameObject is not null
            {
                validBooks.Add(Books[i]); // Add the GameObject to the validBooks list
            }

        }

        Books = validBooks.ToArray(); // Convert the validBooks list back to an array
    }

    // Method to stack game objects vertically
    public void StackBookObject(GameObject[] objectsToStack)
    {
        if (objectsToStack.Length == 0)
        {
            Debug.LogError("No objects to stack!");
            return;
        }

        // Select a random index
        int i = Random.Range(0, objectsToStack.Length);

        // Instantiate the selected object
        GameObject newObj = Instantiate(objectsToStack[i]);

        for (int j = 0; j < studySpots.Length; j++)
        {
            // Get the position to stack
            Transform StackAreas = studySpots[j].transform;
            Vector3 StackDeck = new Vector3(StackAreas.position.x, stackOffset, StackAreas.position.z);
            

            // Instantiate a new copy of the object for each position
            GameObject copyObj = Instantiate(newObj, StackDeck, Quaternion.identity);



        }



        // Log the position of the stacked object
        Debug.Log(newObj.transform.position);

        // Increment stack offset for the next object
        stackOffset += 0.13f;

        // Start a coroutine to wait for a certain duration
        StartCoroutine(WaitForSecondsCoroutine(1));


    }

    //example IEnumerator for waiting a few seconds in code
    IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        Debug.Log("Coroutine started");
        yield return new WaitForSeconds(seconds); // Wait for specified seconds
        Debug.Log("Coroutine finished after " + seconds + " seconds");
    }

}