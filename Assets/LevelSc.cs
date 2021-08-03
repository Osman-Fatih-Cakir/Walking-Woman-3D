
using UnityEngine;

public class LevelSc : MonoBehaviour
{
    public float rotateSpeed = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Constantly rotate object
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
        }
    }
}
