using UnityEngine;

public class WaveTrigger : MonoBehaviour
{
    public WaveManager waveManager;

    private void OnTriggerEnter(Collider other)
{
    Debug.Log($"Trigger entered by: {other.gameObject.name}");
    waveManager.StartWaves();
    Destroy(gameObject);
}


}
