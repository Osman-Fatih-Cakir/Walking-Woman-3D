using System.Collections.Generic;
using UnityEngine;

public class LevelHandlerSc : MonoBehaviour
{
    public GameObject cam;
    public int Point = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    // End level
    public void EndLevel()
    {
        cam.GetComponent<CameraSc>().CameraSpeed = 0;
    }
}
