using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    public AxeScript axeScript;
    private Animator anim;
    private bool isWaitingForNextClick = false;
    private Coroutine resetHitCoroutine;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isWaitingForNextClick)
            {
                if (resetHitCoroutine != null)
                {
                    StopCoroutine(resetHitCoroutine);
                }
            }

            anim.SetBool("hit", true);
            isWaitingForNextClick = true;
            resetHitCoroutine = StartCoroutine(ResetHitParameter());
        }
    }

    IEnumerator ResetHitParameter()
    {
        yield return new WaitForSeconds(0.7f);
        
        anim.SetBool("hit", false);
        isWaitingForNextClick = false;
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
