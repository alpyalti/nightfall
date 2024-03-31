using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class FadeInAndLoadScene : MonoBehaviour
{
    public string sceneToLoad;
    public Image fadePanel; // Reference to the UI panel that will fade in
    public float fadeInDuration = 1f; // Duration of the fade-in effect
    public float delayBeforeLoading = 1f; // Additional delay to ensure the fade-in completes

    private bool isLoading = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLoading)
        {
            StartCoroutine(FadeInAndLoad());
        }
    }

    IEnumerator FadeInAndLoad()
    {
        isLoading = true;
        float time = 0;

        Color panelColor = fadePanel.color;
        panelColor.a = 0; // Start with a fully transparent panel
        fadePanel.color = panelColor;
        fadePanel.gameObject.SetActive(true); // Ensure the panel is active

        // Gradually change the alpha value of the panel's color to 1 over the duration
        while (time < fadeInDuration)
        {
            panelColor.a = Mathf.Lerp(0, 1, time / fadeInDuration);
            fadePanel.color = panelColor;
            time += Time.deltaTime;
            yield return null; // Wait for the next frame before continuing the loop
        }

        // Ensure the panel is fully opaque after the loop
        panelColor.a = 1;
        fadePanel.color = panelColor;

        yield return new WaitForSeconds(delayBeforeLoading); // Wait for the additional delay

        // Load the target scene
        SceneManager.LoadScene(sceneToLoad);
    }
}
