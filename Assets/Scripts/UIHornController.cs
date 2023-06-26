using UnityEngine;
using UnityEngine.UI;

public class UIHornController : MonoBehaviour
{
    [SerializeField]
    private Image background;

    private float _duration;
    private bool _inCooldown;

    private float _time;

    void Update()
    {
        if (_inCooldown)
        {
            _time += Time.deltaTime;
            if (_time >= _duration)
            {
                _time = _duration;
                _inCooldown = false;
            }
            background.fillAmount = _time / _duration;
        }
    }

    public void StartCoodown(float duration)
    {
        background.fillAmount = 0;
        _duration = duration;
        _inCooldown = true;
        _time = 0;
    }
}
