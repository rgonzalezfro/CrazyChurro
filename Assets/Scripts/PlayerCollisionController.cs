using System.Collections;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    [Header("Crash Settings")]
    [SerializeField]
    private string _pedestrianTag = "Pedestrian";

    [SerializeField]
    private float _crashTime = 1f;

    [SerializeField]
    private GameObject _crashAnimation;

    private PlayerController playerController;

    private bool hasCrashed = false;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCrashed && collision.gameObject.CompareTag(_pedestrianTag))
        {
            _crashAnimation.SetActive(true);
            playerController.enabled = false;
            hasCrashed = true;
            StartCoroutine(EnableControls());
        }
    }

    private IEnumerator EnableControls()
    {
        yield return new WaitForSeconds(_crashTime);
        hasCrashed = false;
        playerController.enabled = true;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
