using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField]
    Camera cameraBrainP1;
    [SerializeField]
    Camera cameraBrainP2;

    [SerializeField]
    CinemachineVirtualCamera virtCameraP1;
    [SerializeField]
    CinemachineVirtualCamera virtCameraP2;

    void Start()
    {
        cameraBrainP2.gameObject.SetActive(GameManager.Instance.IsMultiplayer());
        virtCameraP2.gameObject.SetActive(GameManager.Instance.IsMultiplayer());

        if (GameManager.Instance.IsMultiplayer())
        {
            cameraBrainP1.rect = new Rect(new Vector2(0, 0), new Vector2(0.5f, 1));
            cameraBrainP2.rect = new Rect(new Vector2(0.5f, 0), new Vector2(0.5f, 1));
        }
        else
        {
            cameraBrainP1.rect = new Rect(new Vector2(0, 0), new Vector2(1, 1));
        }
    }
}
