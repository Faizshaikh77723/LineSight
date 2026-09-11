using UnityEngine;

public class VirtualOpticalCamera : MonoBehaviour
{
    [Header("Camera")]
    public Camera opticalCamera;

    [Header("Target")]
    public Transform target;

    [Header("Measurement")]
    public bool targetVisible;
    public Vector3 targetViewportPosition;
    public Vector3 targetScreenPosition;

    void Update()
    {
        if (opticalCamera == null)
        {
            return;
        }

        if (target == null)
        {
            return;
        }

        UpdateTargetMeasurement();
    }

    private void UpdateTargetMeasurement()
    {
        Vector3 viewportPosition =
            opticalCamera.WorldToViewportPoint(
                target.position
            );

        targetViewportPosition =
            viewportPosition;

        targetScreenPosition =
            opticalCamera.WorldToScreenPoint(
                target.position
            );

        targetVisible =
            viewportPosition.z > 0f &&
            viewportPosition.x >= 0f &&
            viewportPosition.x <= 1f &&
            viewportPosition.y >= 0f &&
            viewportPosition.y <= 1f;
    }

    public bool IsTargetVisible()
    {
        return targetVisible;
    }

    public Vector3 GetViewportPosition()
    {
        return targetViewportPosition;
    }

    public Vector3 GetScreenPosition()
    {
        return targetScreenPosition;
    }
}