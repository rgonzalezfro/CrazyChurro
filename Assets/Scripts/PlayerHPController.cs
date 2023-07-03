using SuperMaxim.Messaging;
using System.Collections;
using UnityEngine;

public class PlayerHPController : MonoBehaviour
{
    [Header("HP Settings")]
    [SerializeField]
    private int maxHp = 3;


    [Header("Crash Settings")]
    [SerializeField]
    private string _pedestrianTag = "Pedestrian";

    [SerializeField]
    private float _crashTime = 1f;

    [SerializeField]
    private GameObject _crashAnimation;

    private PlayerController playerController;
    private bool hasCrashed = false;
    private int currentHp = 0;


    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        currentHp = maxHp;

        Messenger.Default.Publish(new SetHPPayload(currentHp));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCrashed && collision.gameObject.CompareTag(_pedestrianTag))
        {
            _crashAnimation.SetActive(true);
            playerController.enabled = false;
            hasCrashed = true;
            currentHp--;
            Messenger.Default.Publish(new SetHPPayload(currentHp));
            if (currentHp > 0)
            {
                StartCoroutine(EnableControls());
            }
            else
            {
                Messenger.Default.Publish(new EndGamePayload());
            }
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
