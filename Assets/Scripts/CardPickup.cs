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
        gm = GameManager.gameManagerInstance;
        doorController = GameObject.FindGameObjectWithTag("Door").GetComponent<DoorController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (gm.GetRoomNumber() == -1)
            return;

        if (other.tag == "Player")
        {
            gm.levelStates.ElementAt(gm.GetRoomNumber()).complete = true;
            gm.stopTimer = true;
            gm.currentTime = Time.timeSinceLevelLoad - gm.subtractToTimer;

            if (gm.levelStates.ElementAt(gm.GetRoomNumber()).timeSeconds > Time.timeSinceLevelLoad - gm.subtractToTimer || gm.levelStates.ElementAt(gm.GetRoomNumber()).timeSeconds == 0)
            {
                gm.levelStates.ElementAt(gm.GetRoomNumber()).timeSeconds = Time.timeSinceLevelLoad - gm.subtractToTimer;
                gm.subtractToTimer = 0;
            }

            UIAnimator.Play("LevelComplete");
            FMODPickUp.Play();
            Destroy(gameObject);
            doorController.SetDoorUnlocked(true);
        }
    }
}
