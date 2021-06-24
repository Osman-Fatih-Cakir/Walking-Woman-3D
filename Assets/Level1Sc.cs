
using UnityEngine;

public class Level1Sc : MonoBehaviour
{
    private Transform tr1, tr2, tr3;

    private Vector3 sp1, sp2, sp3;

    // Start is called before the first frame update
    void Start()
    {
        sp1 = GetComponent<Transform>().GetChild(0).gameObject.transform.position;
        sp2 = GetComponent<Transform>().GetChild(1).gameObject.transform.position;
        sp3 = GetComponent<Transform>().GetChild(2).gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float constant = Mathf.Sin(Time.time*1);
        float x1 = constant * 1f;
        float x2 = constant * (-1f);
        float x3 = constant * 1f;

        if (GetComponent<Transform>().GetChild(0).gameObject.GetComponent<SubLevelSc>().IsActive)
        {
            GetComponent<Transform>().GetChild(0).gameObject.transform.position = new Vector3(sp1.x + x1, sp1.y, sp1.z);
        }

        if (GetComponent<Transform>().GetChild(1).gameObject.GetComponent<SubLevelSc>().IsActive)
        {
            GetComponent<Transform>().GetChild(1).gameObject.transform.position = new Vector3(sp2.x + x2, sp2.y, sp2.z);
        }

        if (GetComponent<Transform>().GetChild(2).gameObject.GetComponent<SubLevelSc>().IsActive)
        {
            GetComponent<Transform>().GetChild(2).gameObject.transform.position = new Vector3(sp3.x + x3, sp3.y, sp3.z);
        }
    }
}
