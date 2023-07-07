using System.Collections.Generic;
using UnityEngine;

public class UIHPController : MonoBehaviour
{
    [SerializeField]
    private GameObject hpIconPrefab;

    [SerializeField]
    private Transform hpIconContainer;

    private List<GameObject> liveIcons;

    void Start()
    {
        liveIcons = new List<GameObject>();
    }

    public void SetHP(int lives)
    {
        ClearUI();
        AddHP(lives);
    }

    private void AddHP(int amount)
    {
        if (amount < 0)
        {
            for (int i = 0; i < -amount; i++)
            {
                if (liveIcons.Count > 0)
                {
                    Destroy(liveIcons[liveIcons.Count]);
                    liveIcons.RemoveAt(liveIcons.Count);
                }
            }
        }
        else if (amount > 0)
        {
            for (int i = 0; i < amount; i++)
            {
                var icon = Instantiate(hpIconPrefab, hpIconContainer);
                liveIcons.Add(icon);
            }
        }
    }

    private void ClearUI()
    {
        if (liveIcons.Count == 0 && hpIconContainer.childCount > 0)
        {
            for (int i = hpIconContainer.childCount - 1; i >= 0; i--)
            {
                Destroy(hpIconContainer.GetChild(i).gameObject);
            }
        }

        foreach (var icon in liveIcons)
        {
            Destroy(icon);
        }
        liveIcons.Clear();
    }
}
