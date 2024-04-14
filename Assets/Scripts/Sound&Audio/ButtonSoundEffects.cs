using UnityEngine;
using UnityEngine.EventSystems; // Required for handling UI events
using FMODUnity; // FMOD Unity integration namespace

public class ButtonSoundEffects : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [SerializeField]
    private string hoverSoundEvent = "event:/UIHover"; // FMOD event path for hover sound
    [SerializeField]
    private string clickSoundEvent = "event:/UIClick"; // FMOD event path for click sound

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Play hover sound when the mouse enters the button area
        RuntimeManager.PlayOneShot(hoverSoundEvent);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Play click sound when the button is clicked
        RuntimeManager.PlayOneShot(clickSoundEvent);
    }
}
