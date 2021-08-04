
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
        rotateObjects(transform.gameObject);
    }

    // Constantly rotate object
    void rotateObjects(GameObject obj)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            if (child.layer == 9 || child.layer == 10)
            {
                child.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
            }
            else
            {
                rotateObjects(child);
            }
        }
    }
}
