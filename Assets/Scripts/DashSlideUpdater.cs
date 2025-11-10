using UnityEngine;
using UnityEngine.UI;

public class SliderUpdater : MonoBehaviour
{
    [Header("References")]
    public DashModule dashModule;
    public Slider slider;

    void Start()
    {
        dashModule = GameObject.FindGameObjectWithTag("Player").GetComponent<DashModule>();
        slider = GetComponent<Slider>();

        slider.maxValue = dashModule.dashCharge;
    }

    void Update()
    {
        slider.value = Mathf.Clamp(dashModule.currentDashCharge, 0, dashModule.dashCharge);
    }
}
