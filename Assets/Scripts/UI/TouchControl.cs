using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchControl : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public event Action OnPress;
    public event Action OnRelease;

    public void OnPointerDown(PointerEventData eventData)
    {
        OnPress?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnRelease?.Invoke();
    }
}
