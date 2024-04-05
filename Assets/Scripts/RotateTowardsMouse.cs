using UnityEngine;

public class RotateTowardsMouse : MonoBehaviour
{
    public Camera playerCamera; // Assign your main camera here
    public float rotationSpeed = 5f; // Speed at which the player rotates towards the mouse cursor
    public float alignmentThreshold = 0.98f; // Threshold for alignment check, closer to 1 means more aligned

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RotatePlayerTowardsMouse();
        }
    }

    void RotatePlayerTowardsMouse()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 targetDirection = (hit.point - transform.position).normalized;
            targetDirection.y = 0; // Keep the rotation only on the Y-axis

            // Ters yönü hesapla
            Vector3 oppositeDirection = -targetDirection;

            // Check if the player is already facing the target direction closely
            if (Vector3.Dot(transform.forward, oppositeDirection) < alignmentThreshold)
            {
                Quaternion targetRotation = Quaternion.LookRotation(oppositeDirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}