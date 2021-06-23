
using UnityEngine;

public class WomanSc : MonoBehaviour
{
    public GameObject LHObject;

    private Animator anim;
    
    private Vector3 ipos;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ipos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Mathf.Sin(Time.time) * 1.0f;
        transform.position = new Vector3(ipos.x + x, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OLD: " + LHObject.GetComponent<LevelHandlerSc>().Point);
        if (other.gameObject.layer == 9) // Negative object
        {
            LHObject.GetComponent<LevelHandlerSc>().Point -= 75;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.layer == 10) // Positive object
        {
            LHObject.GetComponent<LevelHandlerSc>().Point += 100;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.layer == 11) // Finish
        {
            Debug.Log("Total Time: " + Time.timeSinceLevelLoad);
            if (LHObject.GetComponent<LevelHandlerSc>().Point > 0) // Win 
            {
                anim.SetTrigger("finish_pos");
            }
            else // Fail
            {
                anim.SetTrigger("finish_neg");
            }
            LHObject.GetComponent<LevelHandlerSc>().EndLevel();
        }
        Debug.Log("POINT: " + LHObject.GetComponent<LevelHandlerSc>().Point);
    }
}
