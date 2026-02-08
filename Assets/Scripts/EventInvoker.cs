
using UnityEngine;
using UnityEngine.Events;

public class EventInvoker : MonoBehaviour
{
    [SerializeField] private string checkForTag;
    [SerializeField] private UnityEvent onStart;
    [SerializeField] private UnityEvent onUpdate;
    [SerializeField] private UnityEvent onTriggerEnter;
    [SerializeField] private UnityEvent onTriggerStay;
    [SerializeField] private UnityEvent onTriggerExit;

    private void Start()
    {
        onStart.Invoke();
    }

    private void Update()
    {
        onUpdate.Invoke();
    }

    private void OnTriggerEnter(Collider other)
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
    private void OnTriggerStay(Collider other)
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
    private void OnTriggerExit(Collider other)
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
