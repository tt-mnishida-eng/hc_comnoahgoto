using UnityEngine;
using UnityEngine.UI;
public class Gauge : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    public float MaxValue { get { return slider.maxValue; } set { slider.maxValue = value; } }
    public float MinValue { get { return slider.minValue; } set { slider.minValue = value; } }
    public float Value { private get { return slider.value; } set { slider.value = value; } }
    void Awake()
    {
        Initialize();
    }
    void Initialize()
    {
        slider.interactable = false;
        slider.minValue = 0;
        slider.maxValue = 1;
    }
}
