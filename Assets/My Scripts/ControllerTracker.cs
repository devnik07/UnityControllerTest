using UnityEngine;
using Valve.VR;

public class ControllerTracker : MonoBehaviour
{
    public uint deviceId = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (uint i = 0; i < OpenVR.k_unMaxTrackedDeviceCount; i++)
        {
            var deviceClass = OpenVR.System.GetTrackedDeviceClass(i);
            if (deviceClass != ETrackedDeviceClass.Invalid)
            {
                Debug.Log($"Device {i}: {deviceClass}");
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OpenVR.System != null)
        {
            var poses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];
            var gamePoses = new TrackedDevicePose_t[OpenVR.k_unMaxTrackedDeviceCount];
            
            OpenVR.Compositor.GetLastPoses(poses, gamePoses);

            if (poses[deviceId].bDeviceIsConnected && poses[deviceId].bPoseIsValid)
            {
                var trackedDevicePose = poses[deviceId];
                var transformMatrix = new SteamVR_Utils.RigidTransform(trackedDevicePose.mDeviceToAbsoluteTracking);

                // Set the GameObject's position and rotation
                transform.position = transformMatrix.pos;
                transform.rotation = transformMatrix.rot;
                transform.Rotate(90.0f, 0, 0);
                Debug.Log($"{transform.rotation.eulerAngles[0]}, {transform.rotation.eulerAngles[1]}, {transform.rotation.eulerAngles[2]}");
            }
        }
    }
}
               