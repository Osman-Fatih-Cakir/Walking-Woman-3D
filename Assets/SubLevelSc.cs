
using UnityEngine;

public class SubLevelSc : MonoBehaviour
{
    public bool IsActive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 8) // Player
        {
            IsActive = false;
        }
    }
}
