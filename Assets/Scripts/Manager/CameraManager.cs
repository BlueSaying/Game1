using Cinemachine;
using UnityEngine;

public class CameraManager : Singleton<CameraManager>
{
    private CinemachineVirtualCamera curVcam;
    private CinemachineVirtualCamera playerFollowCamera;

    private CameraManager()
    {
        playerFollowCamera = PlayerManager.Instance.transform.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();

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
        if (curVcam != null) curVcam.Priority = 0;
        if (playerFollowCamera != null) playerFollowCamera.Priority = 1;

        curVcam = playerFollowCamera;
    }
}