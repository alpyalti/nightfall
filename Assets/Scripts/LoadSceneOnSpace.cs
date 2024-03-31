using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnSpace : MonoBehaviour
{
    public string sceneToLoad;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
