
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public GameObject Camera;
    public float thrust = 150f;
    public float PlayerDefaultSpeed;
    public float SlowDownRateUp = 0.95f;
    public float SlowDownRatePressed = 0.85f;

    public Rigidbody PlayerRigidbody;

    private Vector2 lastMousePos;
    private bool moving = true;

    // Start is called before the first frame update
    void Start()
    {
        PlayerDefaultSpeed = Camera.GetComponent<CameraSc>().CameraSpeed;
    }

    void FixedUpdate()
    {
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
            Vector2 currentMousePos = Input.mousePosition;
            if (lastMousePos == Vector2.zero)
            {
                lastMousePos = currentMousePos;
            }

            deltaPos = currentMousePos - lastMousePos;
            lastMousePos = currentMousePos;

            Vector3 force = new Vector3(deltaPos.x, 0, deltaPos.y) * thrust;
            PlayerRigidbody.AddForce(force);
            PlayerRigidbody.velocity *= SlowDownRatePressed;
            //velocity.text = force.magnitude.ToString();
        }
        else
        {
            lastMousePos = Vector2.zero;
            PlayerRigidbody.velocity *= SlowDownRateUp;
        }

        transform.position += transform.forward * PlayerDefaultSpeed * Time.deltaTime;
    }
}
