
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject LHObject;
    public GameObject CameraObj;
    public GameObject PlayerObject;
    public float thrust = 150f;
    public float PlayerDefaultSpeed;
    public float SlowDownRateUp = 0.95f;
    public float SlowDownRatePressed = 0.85f;

    public Rigidbody PlayerRigidbody;

    private Vector2 lastMousePos;
    private bool moving = false;
    private bool firstTouch = true;
    private bool buttonUp = false;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDefaultSpeed = CameraObj.GetComponent<CameraSc>().CameraSpeed;
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            buttonUp = true;
        }
    }

    void FixedUpdate()
    {
        if (LHObject.GetComponent<LevelHandlerSc>().GameOn
            && !LHObject.GetComponent<LevelHandlerSc>().GameOver)
        {
            moving = true;
        }
        else
        {
            moving = false;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        
        if (!moving)
        {
            return;
        }
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 deltaPos = Vector2.zero;

        if (Input.GetMouseButton(0))
        {
            if (firstTouch)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out hit))
                {
                    //Create a moveTarget if hit
                    PlayerObject.transform.position =  new Vector3(hit.point.x, 0.7F, hit.point.z);
                }
                firstTouch = false;
            }
            else
            {
                Vector2 currentMousePos = Input.mousePosition;
                if (lastMousePos == Vector2.zero)
                {
                    lastMousePos = currentMousePos;
                }
                deltaPos = currentMousePos - lastMousePos;
                lastMousePos = currentMousePos;

                Vector3 force = new Vector3(deltaPos.x * thrust, 0, deltaPos.y * thrust * 1.35F);
                PlayerRigidbody.velocity = Vector3.zero;
                PlayerRigidbody.AddForce(force, ForceMode.Acceleration);

                float cc = 20.0f; float cc_n = cc * (-1.0f);
                if (PlayerRigidbody.velocity.x >= cc) PlayerRigidbody.velocity = new Vector3(cc, PlayerRigidbody.velocity.y, PlayerRigidbody.velocity.z);
                if (PlayerRigidbody.velocity.y >= cc) PlayerRigidbody.velocity = new Vector3(PlayerRigidbody.velocity.x, cc, PlayerRigidbody.velocity.z);
                if (PlayerRigidbody.velocity.z >= cc) PlayerRigidbody.velocity = new Vector3(PlayerRigidbody.velocity.x, PlayerRigidbody.velocity.y, cc);
                if (PlayerRigidbody.velocity.x <= cc_n) PlayerRigidbody.velocity = new Vector3(cc_n, PlayerRigidbody.velocity.y, PlayerRigidbody.velocity.z);
                if (PlayerRigidbody.velocity.y <= cc_n) PlayerRigidbody.velocity = new Vector3(PlayerRigidbody.velocity.x, cc_n, PlayerRigidbody.velocity.z);
                if (PlayerRigidbody.velocity.z <= cc_n) PlayerRigidbody.velocity = new Vector3(PlayerRigidbody.velocity.x, PlayerRigidbody.velocity.y, cc_n);
            }
        }

        if (buttonUp)
        {
            buttonUp = false;
            firstTouch = true;
            PlayerRigidbody.velocity = Vector3.zero;
            lastMousePos = Vector2.zero;
        }
        transform.position += transform.forward * PlayerDefaultSpeed * Time.deltaTime;
    }
}
