using Cinemachine;
using UnityEngine;

public class AutoCameraTarget : MonoBehaviour
{
    [SerializeField]
    Player target;

    CinemachineVirtualCamera virtcamera;

    void Start()
    {
        if (target == Player.One || GameManager.Instance.IsMultiplayer())
        {
            var player = GameManager.Instance.GetPlayer(target);
            virtcamera = GetComponent<CinemachineVirtualCamera>();
            virtcamera.LookAt = player.transform;
            virtcamera.Follow = player.transform;
        }
    }
}
