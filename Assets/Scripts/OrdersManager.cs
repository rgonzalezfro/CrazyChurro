using SuperMaxim.Messaging;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrdersManager : MonoBehaviour
{
    public int maxActiveOrders = 3;
    public float maxDistanceToHear = 15;

    private List<OrderController> activeOrders = new List<OrderController>();
    private List<OrderController> ordersList = new List<OrderController>();

    void Start()
    {
        ordersList.AddRange(FindObjectsByType<OrderController>(FindObjectsSortMode.None).ToList());
        Messenger.Default.Subscribe<HornSoundPayload>(ActivateRandomOrders);
    }

    private void OnDestroy()
    {
        Messenger.Default.Unsubscribe<HornSoundPayload>(ActivateRandomOrders);
    }

    void ActivateRandomOrders(HornSoundPayload payload)
    {
        activeOrders.Clear();
        foreach (OrderController order in ordersList)
        {
            if (order.activeOrder)
            {
                activeOrders.Add(order);
            }
        }

        // Determine the number of orders to activate
        int numOrdersToActivate = Random.Range(0, maxActiveOrders + 1 - activeOrders.Count);

        // Randomly select and activate orders
        for (int i = 0; i < numOrdersToActivate; i++)
        {
            OrderController randomOrder = GetRandomOrder(payload.playerPosition);
            if (randomOrder != null)
            {
                Debug.Log("ORDER ACTIVATED");
                randomOrder.Activate();
                activeOrders.Add(randomOrder);
            }
        }
    }

    OrderController GetRandomOrder(Vector3 playerPos)
    {
        if (ordersList.Count == 0)
        {
            return null;
        }

        List<OrderController> ordersInRange = new List<OrderController>();
        foreach (var order in ordersList)
        {
            if (Vector3.Distance(order.transform.position, playerPos) < maxDistanceToHear)
            {
                ordersInRange.Add(order);
            }
        }

        if (ordersInRange.Count == 0)
        {
            return null;
        }

        int randomIndex = Random.Range(0, ordersInRange.Count);
        OrderController randomOrder = ordersInRange[randomIndex];

        // Ensure the randomly selected order is not already active
        if (activeOrders.Contains(randomOrder))
        {
            return null;
        }

        return randomOrder;
    }
}
