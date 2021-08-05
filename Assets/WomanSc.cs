using System.Collections;
using UnityEngine;

public class WomanSc : MonoBehaviour
{
    public GameObject LHObject;
    public GameObject ParticlesObj;

    [HideInInspector]
    public Animator anim;
    public Vector3 endPos;
    public Quaternion endRot;
    public Vector3 endsca;
    public float startWeight;
    public int posScore = 4;
    public int negScore = 12;
    public GameObject manMeshRenderer;
    private SkinnedMeshRenderer smr;
    private bool isAnimDone = true;    
    private Vector3 ipos;
    private Quaternion irot;
    private Vector3 isca;
    private float sinx;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        ipos = transform.position;
        irot = transform.rotation;
        isca = transform.localScale;
        smr = manMeshRenderer.GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LHObject.GetComponent<LevelHandlerSc>().GameOn 
            && !LHObject.GetComponent<LevelHandlerSc>().GameOver) // If the game is on, the game is playable
        {
            //sinx = Mathf.Sin(Time.time) * 1.25f;
            //transform.position = new Vector3(ipos.x + sinx, transform.position.y, transform.position.z); // Move woman around x axis
        }
    }

    public void StartAgain()
    {
        // Relocate the woman
        transform.position = ipos;
        transform.rotation = irot;
        transform.localScale = isca;
        if(anim.gameObject.activeSelf)
        {
            anim.SetTrigger("run");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9) // Negative object
        {
            gain_weight();
            StartCoroutine(ShrinkObject(other.gameObject, 1.0f, 0.008f));

        }
        else if (other.gameObject.layer == 10) // Positive object
        {
            if (isAnimDone)
            {
                StartCoroutine(WaitForAnim(1.8f));
                anim.SetTrigger("happy"); // Happy animation
            }
            lose_weight();
            StartCoroutine(ShrinkObject(other.gameObject, 1.0f, 0.008f));
            StartCoroutine(Explode(transform.position));// Particle animation
        }
        else if (other.gameObject.layer == 11) // Finish
        {
            finishLevel();
        }
        Debug.Log("POINT: " + smr.GetBlendShapeWeight(0));
    }

    IEnumerator ShrinkObject(GameObject obj, float time, float speed)
    {
        for (float cur = 0.0f; cur <= time; cur += (1.0f/60.0f))
        {
            if (obj)
                obj.transform.localScale -= new Vector3(speed, speed, speed);
            yield return null;
        }
        Destroy(obj.gameObject);
    }

    IEnumerator Explode(Vector3 pos)
    {
        GameObject firework = Instantiate(ParticlesObj, pos, Quaternion.identity);
        firework.transform.parent = transform;
        firework.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(2);
        Destroy(firework);
    }

    IEnumerator WaitForAnim(float time)
    {
        isAnimDone = false;
        yield return new WaitForSeconds(time);
        isAnimDone = true;
    }

    // End level
    void finishLevel()
    {
        // Relocate the woman
        transform.localPosition = endPos;
        transform.localRotation = endRot;
        transform.localScale = endsca;

        Debug.Log("Total Time: " + Time.timeSinceLevelLoad);
        
        LevelHandlerSc sc = LHObject.GetComponent<LevelHandlerSc>();
        sc.weight = smr.GetBlendShapeWeight(0);
        // Start the animation
        if (sc.weight <= sc.winLimit) // Win 
        {
            anim.SetTrigger("win");
        }
        else // Fail
        {
            anim.SetTrigger("lose");
        }
        sc.weight = startWeight;
        smr.SetBlendShapeWeight(0, startWeight);
        sc.EndLevel();
    }

    // Gain weight
    void gain_weight()
    {
        float num = smr.GetBlendShapeWeight(0);
        num += negScore;
        if (num > 100)
            num = 100;
        
        smr.SetBlendShapeWeight(0, num);
    }

    // Lose weight
    void lose_weight()
    {
        float num = smr.GetBlendShapeWeight(0);
        num -= posScore;
        if (num < 0)
            num = 0;
        
        smr.SetBlendShapeWeight(0, num);
    }
}
