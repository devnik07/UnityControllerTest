using UnityEngine;
using Valve.VR;

public class ControllerTracker2 : MonoBehaviour
{
    public SteamVR_Action_Pose poseAction;
    public SteamVR_Action_Vector2 moveAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = poseAction.GetLocalRotation(SteamVR_Input_Sources.Any);
        Vector3 position = poseAction.GetLocalPosition(SteamVR_Input_Sources.Any);
        Vector2 joystickInput = moveAction.GetAxis(SteamVR_Input_Sources.Any);

        transform.rotation = q;
        transform.Rotate(90.0f, 0, 0);
        transform.position = position;

        print(joystickInput);
    }
}
