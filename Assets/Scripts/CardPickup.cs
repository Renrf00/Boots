using System.Linq;
using FMODUnity;
using UnityEngine;

public class CardPickup : MonoBehaviour
{
    [Header("References")]
    private GameManager gm;
    private DoorController doorController;
    [SerializeField] private Animator UIAnimator;
    [SerializeField] private StudioEventEmitter FMODPickUp;

    void Start()
    {
        doorController = GameObject.FindGameObjectWithTag("Door").GetComponent<DoorController>();
    }

    void OnTriggerEnter(Collider other)
    {
        gm = FindFirstObjectByType<GameManager>();

        if (gm.GetRoomNumber() == -1)
            return;

        if (other.tag == "Player")
        {
            gm.levelStates.ElementAt(gm.GetRoomNumber()).complete = true;

            if (gm.levelStates.ElementAt(gm.GetRoomNumber()).timeSeconds > Time.timeSinceLevelLoad - gm.subtractToTimer || gm.levelStates.ElementAt(gm.GetRoomNumber()).timeSeconds == 0)
            {
                gm.levelStates.ElementAt(gm.GetRoomNumber()).timeSeconds = Time.timeSinceLevelLoad - gm.subtractToTimer;
                gm.subtractToTimer = 0;
            }

            FMODPickUp.Play();
            Destroy(gameObject);
            doorController.SetDoorUnlocked(true);
        }
    }
}
