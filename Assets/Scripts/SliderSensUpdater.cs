using UnityEngine;
using UnityEngine.UI;

public class SliderSensUpdater : MonoBehaviour
{
    public void UpdateNumber(ScriptableNumber scriptableNumber)
    {
        scriptableNumber.number = GetComponent<Slider>().value;
    }
}
