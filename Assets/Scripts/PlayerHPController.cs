using SuperMaxim.Messaging;
using System.Collections;
using UnityEngine;

public class PlayerHPController : MonoBehaviour
{
    public bool isPlayer2;

    [Header("HP Settings")]
    [SerializeField]
    private int maxHp = 3;


    [Header("Crash Settings")]
    [SerializeField]
    private string _pedestrianTag = "Pedestrian";

    [SerializeField]
    private float _crashMinSpeed = 2f;

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

        Messenger.Default.Publish(new SetHPPayload(currentHp, playerController.Id));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCrashed && collision.gameObject.CompareTag(_pedestrianTag) && collision.relativeVelocity.magnitude > _crashMinSpeed)
        {
            _crashAnimation.SetActive(true);
            playerController.enabled = false;
            hasCrashed = true;
            currentHp--;
            Messenger.Default.Publish(new SetHPPayload(currentHp, playerController.Id));
            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Crash");
            if (currentHp > 0)
            {
                StartCoroutine(EnableControls());
            }
            else
            {
                StartCoroutine(FreezePosition());
                Messenger.Default.Publish(new EndGamePayload());
            }
        }
    }

    public bool IsAlive()
    {
        return currentHp > 0;
    }

    private IEnumerator EnableControls()
    {
        yield return new WaitForSeconds(_crashTime);
        hasCrashed = false;
        playerController.enabled = true;
    }

    private IEnumerator FreezePosition()
    {
        yield return new WaitForSeconds(_crashTime);
        GetComponent<Rigidbody2D>().freezeRotation = true;
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
