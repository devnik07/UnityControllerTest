using UnityEngine;
using Valve.VR;

public class ControllerTracker : MonoBehaviour
{
    public SteamVR_Action_Pose poseAction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = poseAction.GetLocalRotation(SteamVR_Input_Sources.Any);
        Vector3 position = poseAction.GetLocalPosition(SteamVR_Input_Sources.Any);

        //(q.z, q.x) = (q.x, q.z);
        transform.rotation = q;
        transform.Rotate(90.0f, 0, 0);
        transform.position = position;
    }
}
