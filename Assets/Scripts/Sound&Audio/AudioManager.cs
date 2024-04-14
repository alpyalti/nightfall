using UnityEngine;
using FMODUnity; // Ensure you have this namespace to use FMOD classes

public class AudioManager : MonoBehaviour
{
    // FMOD VCA reference
    private FMOD.Studio.VCA masterVCA;

    private void Start()
    {
        // Get the VCA reference from FMOD
        // The path should be "vca:/Master" if your VCA is named "Master"
        masterVCA = RuntimeManager.GetVCA("vca:/Master");
        RuntimeManager.PlayOneShot("event:/MainMenuMusic");
    }

    // Public method to set the volume of the Master VCA
    // Volume is expected to be between 0 (silent) and 1 (full volume)
    public void SetMasterVolume(float volume)
    {
        if (masterVCA.isValid())
        {
            masterVCA.setVolume(volume);
        }
        else
        {
            Debug.LogError("Master VCA is not valid.");
        }
    }
}
