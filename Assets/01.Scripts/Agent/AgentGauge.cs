using UnityEngine;
using UnityEngine.UI;

public class AgentGauge : MonoBehaviour
{
    private GameObject _canvas;
    private Slider _slider;

    private void Awake() 
    {
        _canvas = transform.Find("GaugeCanvas").gameObject;
        _slider = _canvas.transform.GetChild(0).GetComponent<Slider>();
    }

    private void Start() 
    {
        _canvas.SetActive(false);
    }

    public void SliderValue(float value)
    {
        _slider.value = value;
    }

    public void CanvasEnable(bool value)
    {
        _canvas.SetActive(value);
    }
}
