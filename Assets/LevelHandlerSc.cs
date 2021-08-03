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
    public GameObject GameplayCanvasObj;
    public GameObject[] LEVELS;
    public GameObject[] HealtyFoods;
    public GameObject[] UnhealtyFoods;
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
            Level_Positions.Add(new Vector3(0, 0.1f, i*20));//LEVELS[i].transform.position);
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
            int random = Random.Range(0, LEVELS.Length);
            GameObject temp = Instantiate(LEVELS[random], Level_Positions[i], Quaternion.identity);
            set_level_assets(temp);
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
        GameplayCanvasObj.SetActive(true);

        GameFail = false;

    }

    // Set level assets
    private void set_level_assets(GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            if (child.layer == 10) // Positive object
            {
                int random = Random.Range(0, HealtyFoods.Length);
                GameObject temp = Instantiate(HealtyFoods[random]);
                temp.transform.parent = child.transform;
                temp.transform.localPosition = new Vector3(0, 0, 0);
                temp.transform.localScale = new Vector3(2.5F, 2.5F, 2.5F);
            }
            else if (child.layer == 9) // Negative object
            {
                int random = Random.Range(0, UnhealtyFoods.Length);
                GameObject temp = Instantiate(UnhealtyFoods[random]);
                temp.transform.parent = child.transform;
                temp.transform.localPosition = new Vector3(0, 0, 0);
                temp.transform.localScale = new Vector3(3.5F, 3.5F, 3.5F);
            }
        }
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
            int random = Random.Range(0, LEVELS.Length);
            GameObject temp = Instantiate(LEVELS[random], Level_Positions[i], Quaternion.identity);
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
        GameplayCanvasObj.SetActive(true);

        GameFail = false;
    }

    // End level
    public void EndLevel()
    {
        GameOver = true;
        GameplayCanvasObj.SetActive(false);
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
