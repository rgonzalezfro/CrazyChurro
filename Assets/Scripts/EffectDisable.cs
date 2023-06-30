using UnityEngine;

public class EffectDisable : MonoBehaviour
{
   private void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
