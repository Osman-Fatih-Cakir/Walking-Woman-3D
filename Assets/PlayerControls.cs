
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public float constant = 0.1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        foreach(Touch touch in Input.touches)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            GetComponent<Rigidbody>().AddForce(
                new Vector3(touch.deltaPosition.x * constant*constant, 0, touch.deltaPosition.y * constant*constant),
                ForceMode.VelocityChange
            );
        }
    }
}
