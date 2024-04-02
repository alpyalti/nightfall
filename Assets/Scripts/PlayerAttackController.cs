using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    public AxeScript axeScript; // Assign this in the inspector

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
