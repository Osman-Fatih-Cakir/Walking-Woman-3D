
using UnityEngine;

public class WomanSc : MonoBehaviour
{
    public GameObject LHObject;

    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) // Negative object
        {
            LHObject.GetComponent<LevelHandlerSc>().Point -= 75;
        }
        else if (other.gameObject.layer == 10) // Positive object
        {
            LHObject.GetComponent<LevelHandlerSc>().Point += 100;
        }
        else if (other.gameObject.layer == 11) // Finish
        {
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
