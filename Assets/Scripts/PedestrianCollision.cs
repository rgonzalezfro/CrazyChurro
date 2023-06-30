using System;
using UnityEngine;

public class PedestrianCollision : MonoBehaviour
{
    public event Action OnCollision;

    public void TriggerCollision()
    {
        OnCollision?.Invoke();
    }
}
