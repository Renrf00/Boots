using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    [Header("References")]
    private DashModule dashModule;
    private Slider slider;

    void Start()
    {
        dashModule = GameObject.FindGameObjectWithTag("Player").GetComponent<DashModule>();
        slider = GetComponent<Slider>();

        slider.maxValue = dashModule.maxDashCharge;
    }

    void Update()
    {
        slider.value = Mathf.Clamp(dashModule.currentDashCharge, 0, dashModule.maxDashCharge);
    }
}
