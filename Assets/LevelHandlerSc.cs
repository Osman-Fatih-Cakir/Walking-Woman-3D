using System.Collections.Generic;
using UnityEngine;

public class LevelHandlerSc : MonoBehaviour
{
    [HideInInspector]
    public bool GameOn = false;
    [HideInInspector]
    public bool GameOver = false;
    public bool GameFail = false;
    public GameObject StartCanvasObj;
    public GameObject WinCanvasObj;
    public GameObject FailCanvasObj;
    public GameObject[] LEVELS;
    public GameObject cam;
    public GameObject PlayerObj;
    public GameObject WomanObj;
    public int Point = 0;
    public int LevelCount = 7;
    private List<GameObject> Current_Levels = new List<GameObject>();
    private List<Vector3> Level_Positions = new List<Vector3>();
    private Vector3 CameraStartPoint;
    private Vector3 PlayerStartPoint;

    // Start is called before the first frame update
    void Start()
    {
        // Store the camera and player starting point
        CameraStartPoint = cam.transform.position;
        PlayerStartPoint = PlayerObj.transform.position;

        StartCanvasObj.SetActive(true);

        // Take level positions
        for (int i = 0; i < LevelCount; i++)
        {
            Level_Positions.Add(new Vector3(0, 0, i*20));//LEVELS[i].transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Start level
    public void StartNewLevel()
    {
        // Destroy old levels
        for (int i = 0; i < Current_Levels.Count; i++)
        {
            Destroy(Current_Levels[i]);
        }
        Current_Levels.Clear();

        // Initialize the levels
        reshuffle(LEVELS);
        for (int i = 0; i < LevelCount; i++)
        {
            GameObject temp = Instantiate(LEVELS[i], Level_Positions[i], Quaternion.identity);
            Current_Levels.Add(temp);
            Debug.Log(temp.name);
        }

        // Set camera and player positions
        cam.transform.position = CameraStartPoint;
        PlayerObj.transform.position = PlayerStartPoint;
        WomanObj.GetComponent<WomanSc>().StartAgain();

        GameOn = true;
        GameOver = false;

        // Disable start canvas
        StartCanvasObj.SetActive(false);
        WinCanvasObj.SetActive(false);
        FailCanvasObj.SetActive(false);

        GameFail = false;

    }

    // Restart the level
    public void RetryLevel()
    {
        // Destroy old levels
        for (int i = 0; i < Current_Levels.Count; i++)
        {
            Destroy(Current_Levels[i]);
        }
        Current_Levels.Clear();

        // Initialize the levels (The levels will not be shuffled)
        for (int i = 0; i < LevelCount; i++)
        {
            GameObject temp = Instantiate(LEVELS[i], Level_Positions[i], Quaternion.identity);
            Current_Levels.Add(temp);
        }
        
        // Set camera and player positions
        cam.transform.position = CameraStartPoint;
        PlayerObj.transform.position = PlayerStartPoint;
        WomanObj.GetComponent<WomanSc>().StartAgain();

        GameOn = true;
        GameOver = false;

        // Disable start canvas
        StartCanvasObj.SetActive(false);
        WinCanvasObj.SetActive(false);
        FailCanvasObj.SetActive(false);

        GameFail = false;
    }

    // End level
    public void EndLevel()
    {
        GameOver = true;

        // Open Game Over canvas
        if (Point >= 0) // Win canvas
        {
            WinCanvasObj.SetActive(true);
        }
        else // Lose canvas
        {
            GameFail = true;
            FailCanvasObj.SetActive(true);
        }
    }

    void reshuffle(GameObject[] texts)
        {
            // Knuth shuffle algorithm :: courtesy of Wikipedia :)
            for (int t = 0; t < texts.Length; t++ )
            {
                GameObject tmp = texts[t];
                int r = Random.Range(t, texts.Length);
                texts[t] = texts[r];
                texts[r] = tmp;
            }
        }
}
