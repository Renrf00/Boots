using System;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    [SerializeField] private Animator UIAnimation;
    [SerializeField] private string idleState;
    [SerializeField] private bool inMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inMenu)
            {
                GameManager.LockCursor(idleState == "LevelSelect" ? false : true);
                try
                {
                    FindFirstObjectByType<PlayerController>().DisableCameraMove(false);
                }
                catch (Exception)
                {
                    Console.WriteLine("No PlayerController found");
                }

                UIAnimation.Play(idleState);
                inMenu = false;
            }
            else
            {
                GameManager.LockCursor(false);
                try
                {
                    FindFirstObjectByType<PlayerController>().DisableCameraMove(true);
                }
                catch (Exception)
                {
                    Console.WriteLine("No PlayerController found");
                }

                UIAnimation.Play("Menu");
                inMenu = true;
            }
        }
    }
}
