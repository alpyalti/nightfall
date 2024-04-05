using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public AxeScript axeScript; // Reference to the AxeScript component.
    private Animator anim; // Animator componentine referans.
    private bool isWaitingForNextClick = false; // Bir sonraki tıklama için bekleme durumu.
    private Coroutine resetHitCoroutine; // Hit parametresini sıfırlamak için kullanılacak coroutine referansı.

    private void Start()
    {
        anim = GetComponent<Animator>(); // GameObject'e bağlı Animator componentini al.
    }

    private void Update()
    {
        // Mouse'a tıklandığında hit parametresini kontrol et ve yönet.
        if (Input.GetMouseButtonDown(0))
        {
            if (isWaitingForNextClick)
            {
                // Eğer zaten bir tıklama bekleniyorsa, coroutine'i iptal et.
                if (resetHitCoroutine != null)
                {
                    StopCoroutine(resetHitCoroutine);
                }
            }

            anim.SetBool("hit", true); // Animator'de hit parametresini true yap.
            isWaitingForNextClick = true; // Bir sonraki tıklama için bekleme durumunu true yap.
            resetHitCoroutine = StartCoroutine(ResetHitParameter()); // Coroutine'i başlat.
        }
    }

    IEnumerator ResetHitParameter()
    {
        // Yarım saniye bekler.
        yield return new WaitForSeconds(0.5f);
        
        // Eğer bu süre zarfında başka bir tıklama olmadıysa, hit parametresini false yap.
        anim.SetBool("hit", false);
        isWaitingForNextClick = false; // Bekleme durumunu false yap.
    }

    public void EnableAxeCollider()
    {
        if (axeScript != null)
            axeScript.EnableAxeCollider();
    }

    public void DisableAxeCollider()
    {
        if (axeScript != null)
            axeScript.DisableAxeCollider();
    }
}
