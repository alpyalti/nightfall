using UnityEngine;
using UnityEngine.SceneManagement; // Needed for loading scenes
using UnityEngine.UI; // Needed for UI elements like Buttons and Panels

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel; // Assign in the Inspector

    public void PlayGame()
    {
        // Load your game scene here. Replace "GameScene" with the actual name of your game scene
        SceneManager.LoadScene("IntroScene");
    }


    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
