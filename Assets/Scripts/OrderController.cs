using UnityEngine;
using UnityEngine.UI;

public class OrderController : MonoBehaviour
{
    public string playerTag;
    public float fulfillingTime;
    public int orderValue;

    public GameObject UIOrder;
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

    private void Init()
    {
        UISlider.maxValue = fulfillingTime;
        UISlider.value = 0;
        activeOrder = true;
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
    }
}