
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventInvoker : MonoBehaviour
{
    public string checkForTag;
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerStay;
    public UnityEvent onTriggerExit;


    void OnTriggerEnter(Collider other)
    {
        if (checkForTag == "")
        {
            onTriggerEnter.Invoke();
        }
        else if (other.tag == checkForTag)
        {
            onTriggerEnter.Invoke();
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (checkForTag == "")
        {
            onTriggerStay.Invoke();
        }
        else if (other.tag == checkForTag)
        {
            onTriggerStay.Invoke();
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (checkForTag == "")
        {
            onTriggerExit.Invoke();
        }
        else if (other.tag == checkForTag)
        {
            onTriggerExit.Invoke();
        }
    }
}
