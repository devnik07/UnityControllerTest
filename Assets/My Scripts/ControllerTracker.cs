using UnityEngine;
using Valve.VR;

public class ControllerTracker : MonoBehaviour
{
    public SteamVR_Action_Pose poseAction;
    public SteamVR_Action_Boolean toggleZoomAction;
    public SteamVR_Action_Vector2 joystickAction;
    public float zoomSpeed = 0.04f;

    private bool zoomModeOn;
    private float zoom;
    private float toggleTimeout = 1.0f;
    private float lastToggleTime;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zoom = 1.0f;
        zoomModeOn = false;
        lastToggleTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = poseAction.GetLocalRotation(SteamVR_Input_Sources.Any);
        Vector3 position = poseAction.GetLocalPosition(SteamVR_Input_Sources.Any);

        //(q.z, q.x) = (q.x, q.z);
        transform.rotation = q;
        transform.Rotate(90.0f, 0, 0);

        position.x -= 1.5f;
        position.y += 0.75f;
        transform.position = position;

        bool toggleZoom = toggleZoomAction.GetState(SteamVR_Input_Sources.Any);
        if (Time.time - lastToggleTime >= toggleTimeout) {
            if (zoomModeOn && toggleZoom) {
                zoomModeOn = false;
                lastToggleTime = Time.time;
                print("ControllerTracker: Zoom mode deactivated");
            } else if (toggleZoom) {
                zoomModeOn = true;
                lastToggleTime = Time.time;
                print("ControllerTracker: Zoom mode activated");
            }
        }

        if (zoomModeOn) {
            float zoomInput = joystickAction.GetAxis(SteamVR_Input_Sources.Any).y;
            if (Mathf.Abs(zoomInput) > 0.1f) {
                zoom += zoomInput * zoomSpeed;
                Vector3 newScale = new(zoom, zoom, zoom);
                transform.localScale = newScale;
                print(transform.localScale);
            }
        }
    }
}
