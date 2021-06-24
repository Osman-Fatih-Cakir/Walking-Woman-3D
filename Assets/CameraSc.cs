
using UnityEngine;

public class CameraSc : MonoBehaviour
{
    public GameObject LHObject;
    public float CameraSpeed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LHObject.GetComponent<LevelHandlerSc>().GameOn 
            && !LHObject.GetComponent<LevelHandlerSc>().GameOver)
        {
            // Move camera at constant speed
            moveCamera(Time.deltaTime);
        }
    }

    void moveCamera(float delta)
    {
        
        GetComponent<Transform>().position += delta * (new Vector3(0, 0, CameraSpeed));
    }
}
