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
    private float _cooldownDuration = 5f;
    [SerializeField]
    private bool _cooldownFeedback;
    private float _cooldownTime;
    private bool _inCooldown;

    public GameObject UIOrder;
    public GameObject UICooldown;
    public Slider UISlider;

    [Header("Debug")]
    public float timeCounter;
    public bool fulfillingOrder;
    public bool activeOrder;

    private GameObject playerFulfilling;


    private void Start()
    {
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

    private void Update()
    {
        if (_inCooldown)
        {
            _cooldownTime += Time.deltaTime;
            if (_cooldownTime >= _cooldownDuration)
            {
                _cooldownTime = _cooldownDuration;
                _inCooldown = false;
                if (_cooldownFeedback)
                {
                    UICooldown.SetActive(false);
                }
            }
        }
    }

    private void Init()
    {
        UISlider.maxValue = fulfillingTime;
        UISlider.value = 0;
    }

    internal void Activate()
    {
        Init();
        StartCoroutine(ActivateOrderDelayed());
    }

    public bool CanActivate()
    {
        return !(activeOrder || _inCooldown);
    }

    private IEnumerator ActivateOrderDelayed()
    {
        yield return new WaitForSeconds(delayToActivate);
        activeOrder = true;
        UIOrder.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (activeOrder && collision.CompareTag(playerTag) && !fulfillingOrder)
        {
            timeCounter = 0;
            fulfillingOrder = true;
            playerFulfilling = collision.gameObject;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
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

    private void CompleteOrder()
    {
        activeOrder = false;
        UIOrder.SetActive(false);
        playerFulfilling.GetComponent<PlayerScoreController>()?.AddScore(orderValue);

        fulfillingOrder = false;
        playerFulfilling = null;

        _inCooldown = true;
        _cooldownTime = 0;
        if (_cooldownFeedback)
        {
            UICooldown.SetActive(true);
        }
    }
}