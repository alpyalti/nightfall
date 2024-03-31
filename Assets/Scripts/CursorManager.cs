using UnityEngine;

public class CursorManager : MonoBehaviour
{
    void Start()
    {
        // Hide the cursor at the start of the game
        Cursor.visible = false;

        // Optionally, lock the cursor to the center of the screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void OnApplicationFocus(bool hasFocus)
    {
        // Hide the cursor when the game window regains focus
        if (hasFocus)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
