using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour
{
    public string sceneToLoad;
    void OnTriggerEnter(Collider other)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
