using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public Animator canvasAnimation;
    public string idleState;
    public bool inMenu;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (inMenu)
            {
                canvasAnimation.Play(idleState);
                inMenu = false;
            }
            else
            {
                canvasAnimation.Play("Menu");
                inMenu = true;
            }
        }

    }
}
