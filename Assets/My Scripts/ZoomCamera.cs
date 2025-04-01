using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public SteamVR_Action_Boolean toggleZoomAction;
    public SteamVR_Action_Vector2 joystickAction;

    private float zoom;
    private float zoomMultiplier = 5f;
    private float minZoom = 30f;
    private float maxZoom = 90f;
    private float velocity = 0f;
    private float smoothTime = 0.25f;
    private bool zoomModeOn;
    private float movementSpeed = 2f;
    private float rotationSpeed = 120.0f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zoom = cam.fieldOfView;
        zoomModeOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        bool toggleZoom = toggleZoomAction.GetState(SteamVR_Input_Sources.Any);
        if (zoomModeOn && toggleZoom) {
            zoomModeOn = false;
            print("Zoom mode deactivated");
        } else if (toggleZoom) {
            zoomModeOn = true;
            print("Zoom mode activated");
        }

        if (zoomModeOn) {
            float zoomInput = joystickAction.GetAxis(SteamVR_Input_Sources.Any).y;
            zoom -= zoomInput * zoomMultiplier;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            cam.fieldOfView = Mathf.SmoothDamp(cam.fieldOfView, zoom, ref velocity, smoothTime);
            print(zoomInput);
        } else {
            Vector2 joystickInput = joystickAction.GetAxis(SteamVR_Input_Sources.Any);
            if (Mathf.Abs(joystickInput.x) <= 0.1f) {
                joystickInput.x = 0;
            }
            if (Mathf.Abs(joystickInput.y) <= 0.1f) {
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

            print(joystickInput);
        }
    }
}
