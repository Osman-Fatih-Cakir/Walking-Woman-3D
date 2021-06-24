using System.Collections;
using UnityEngine;

public class WomanSc : MonoBehaviour
{
    public GameObject LHObject;

    [HideInInspector]
    public Animator anim;
    
    private Vector3 ipos;
    private float sinx;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ipos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (LHObject.GetComponent<LevelHandlerSc>().GameOn 
            && !LHObject.GetComponent<LevelHandlerSc>().GameOver) // If the game is on, the game is playable
        {
            sinx = Mathf.Sin(Time.time) * 1.0f;
            transform.position = new Vector3(ipos.x + sinx, transform.position.y, transform.position.z); // Move woman around x axis
        }

    }

    public void StartAgain()
    {
        anim.SetTrigger("run");
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("OLD: " + LHObject.GetComponent<LevelHandlerSc>().Point);
        if (other.gameObject.layer == 9) // Negative object
        {
            LHObject.GetComponent<LevelHandlerSc>().Point -= 75;
            StartCoroutine(ShrinkObject(other.gameObject, 1.0f, 0.008f));
        }
        else if (other.gameObject.layer == 10) // Positive object
        {
            LHObject.GetComponent<LevelHandlerSc>().Point += 100;
            StartCoroutine(ShrinkObject(other.gameObject, 1.0f, 0.008f));
        }
        else if (other.gameObject.layer == 11) // Finish
        {
            LHObject.GetComponent<LevelHandlerSc>().EndLevel();

            Debug.Log("Total Time: " + Time.timeSinceLevelLoad);
            if (LHObject.GetComponent<LevelHandlerSc>().Point > 0) // Win 
            {
                anim.SetTrigger("win");
            }
            else // Fail
            {
                anim.SetTrigger("lose");
            }
            LHObject.GetComponent<LevelHandlerSc>().EndLevel();
        }
        Debug.Log("POINT: " + LHObject.GetComponent<LevelHandlerSc>().Point);
    }

    IEnumerator ShrinkObject(GameObject obj, float time, float speed)
    {
        for (float cur = 0.0f; cur <= time; cur += (1.0f/60.0f))
        {
            obj.transform.localScale -= new Vector3(speed, speed, speed);
            yield return null;
        }
        Destroy(obj.gameObject);
    }
}
