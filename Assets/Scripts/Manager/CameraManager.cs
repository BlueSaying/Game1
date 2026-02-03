using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private CinemachineVirtualCamera playerFollowCamera;
    private CinemachineVirtualCamera curVcam;

    private CameraManager()
    {
        SwitchToPlayerFollowCamera();

        EventCenter.Instance.RegisterEvent(EventType.OnSceneSwitchComplete, SwitchToPlayerFollowCamera);
    }

    public void SwitchCamera(CinemachineVirtualCamera vcam)
    {
        if (curVcam != null) curVcam.Priority = 0;
        if (vcam != null) vcam.Priority = 1;

        curVcam = vcam;
    }

    public void SwitchToPlayerFollowCamera()
    {
        playerFollowCamera = GameObject.Find("PlayerFollowCamera")?.GetComponent<CinemachineVirtualCamera>();

        if (curVcam != null) curVcam.Priority = 0;
        if (playerFollowCamera != null) playerFollowCamera.Priority = 1;

        curVcam = playerFollowCamera;
    }
}