using UnityEngine.UI;
using UnityEngine;

public class ProgressbarSc : MonoBehaviour
{
    public GameObject WomanObj;
    public GameObject FinishObj;
    private float distance;
    private float initPos;
    // Start is called before the first frame update
    void Start()
    {
        initPos = WomanObj.transform.position.z;
        distance = Mathf.Abs(FinishObj.transform.position.z - initPos);
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the progress bar value
        float current = Mathf.Abs(WomanObj.transform.position.z - initPos);
        GetComponent<Slider>().value = (current / distance) * 100;
    }
}
