using UnityEngine;
using Valve.VR;

public class MoveCamera : MonoBehaviour
{
    public SteamVR_Action_Boolean toggleZoomAction;
    public SteamVR_Action_Vector2 joystickAction;
    private float joystickDeadzone = 0.1f;
    private float movementSpeed = 2f;
    private float rotationSpeed = 120.0f;
    private bool zoomModeOn;
    private float toggleTimeout = 1.0f;
    private float lastToggleTime;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zoomModeOn = false;
        lastToggleTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        bool toggleZoom = toggleZoomAction.GetState(SteamVR_Input_Sources.Any);
        if (Time.time - lastToggleTime >= toggleTimeout) {
            if (zoomModeOn && toggleZoom) {
                zoomModeOn = false;
                lastToggleTime = Time.time;
                print("MoveCamera: Zoom mode deactivated");
            } else if (toggleZoom) {
                zoomModeOn = true;
                lastToggleTime = Time.time;
                print("MoveCamera: Zoom mode activated");
            }
        }

        if (!zoomModeOn) {
            Vector2 joystickInput = joystickAction.GetAxis(SteamVR_Input_Sources.Any);
            if (Mathf.Abs(joystickInput.x) <= joystickDeadzone) {
                joystickInput.x = 0;
            }
            if (Mathf.Abs(joystickInput.y) <= joystickDeadzone) {
                joystickInput.y = 0;
            }

            /** Unity's x and z direction corresponds to the OpenVR's -y and x axis respectively */
            //Vector3 movementDirection = transform.right * (-1) * joystickInput.y + transform.forward * joystickInput.x;
            //movementDirection.y = 0;
            //transform.position += movementDirection * movementSpeed * Time.deltaTime;
            
            Vector3 moveStraight = transform.right * (-1) * joystickInput.y;
            transform.position += moveStraight * movementSpeed * Time.deltaTime;

            float turn = joystickInput.x * rotationSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            transform.rotation *= turnRotation;
        }
    }
}
