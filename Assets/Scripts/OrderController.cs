using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    public string playerTag;
    public float fulfillingTime;
    public int orderValue;
    public float delayToActivate = 1f;

    [Header("Cooldown Settings")]
    [SerializeField]
    private float _cooldownDuration = 15f;
    private bool _inCooldown;

    [Header("Feedback Settings")]
    [SerializeField]
    private bool _completionFeedback;
    [SerializeField]
    private float _completionFeedbackDuration = 2f;

    public GameObject UIOrder;
    public GameObject UICooldown;
    public Slider UISlider;

    [Header("Debug")]
    public float timeCounter;
    public bool fulfillingOrder;
    public bool activeOrder;

    private GameObject playerFulfilling;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Init();
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    IEnumerator ShowFeedback()
    {
        if (_completionFeedback)
        {
            UICooldown.SetActive(true);
            yield return new WaitForSeconds(_completionFeedbackDuration);
            UICooldown.SetActive(false);
        }
    }

    IEnumerator StartCooldown()
    {
        _inCooldown = true;
        yield return new WaitForSeconds(_cooldownDuration);
        _inCooldown = false;
    }

    private void Init()
    {
        UISlider.maxValue = fulfillingTime;
        UISlider.value = 0;
    }

    internal void Activate()
    {
        Init();
        StartCoroutine(ActivateOrder());
    }

    public bool CanActivate()
    {
        return !(activeOrder || _inCooldown);
    }

    private IEnumerator ActivateOrder()
    {
        yield return new WaitForSeconds(delayToActivate);
        activeOrder = true;
        UIOrder.SetActive(true);
        spriteRenderer.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckStartFullfilling(collision);
    }

    private void CheckStartFullfilling(Collider2D collision)
    {
        if (activeOrder && collision.CompareTag(playerTag) && !fulfillingOrder)
        {
            timeCounter = 0;
            fulfillingOrder = true;
            playerFulfilling = collision.gameObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckStartFullfilling(collision);

        if (activeOrder && collision.gameObject == playerFulfilling)
        {
            timeCounter += Time.deltaTime;
            UISlider.value = timeCounter;
            if (timeCounter >= fulfillingTime)
            {
                CompleteOrder();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (activeOrder && collision.gameObject == playerFulfilling)
        {
            fulfillingOrder = false;
            playerFulfilling = null;
            timeCounter = 0;
            UISlider.value = timeCounter;
        }
    }

    private void CompleteOrder()
    {
        activeOrder = false;
        UIOrder.SetActive(false);
        playerFulfilling.GetComponent<PlayerScoreController>()?.AddScore(orderValue);

        fulfillingOrder = false;
        playerFulfilling = null;
        spriteRenderer.enabled = false;

        StartCoroutine(StartCooldown());
        StartCoroutine(ShowFeedback());
    }
}