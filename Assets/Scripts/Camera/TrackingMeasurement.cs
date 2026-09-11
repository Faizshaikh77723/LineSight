using UnityEngine;

public class TrackingMeasurement : MonoBehaviour
{
    [Header("References")]
    public Camera opticalCamera;
    public Transform target;

    [Header("Measurement")]
    public Vector2 normalizedError;
    public float angularError;
    public bool targetInFieldOfView;

    void Update()
    {
        CalculateMeasurement();
    }

    private void CalculateMeasurement()
    {
        if (opticalCamera == null)
        {
            return;
        }

        if (target == null)
        {
            return;
        }

        Vector3 viewportPosition =
            opticalCamera.WorldToViewportPoint(
                target.position
            );

        targetInFieldOfView =
            viewportPosition.z > 0f &&
            viewportPosition.x >= 0f &&
            viewportPosition.x <= 1f &&
            viewportPosition.y >= 0f &&
            viewportPosition.y <= 1f;

        if (!targetInFieldOfView)
        {
            normalizedError = Vector2.zero;
            angularError = 0f;
            return;
        }

        normalizedError =
            new Vector2(
                viewportPosition.x - 0.5f,
                viewportPosition.y - 0.5f
            );

        Vector3 cameraDirection =
            opticalCamera.transform.forward;

        Vector3 targetDirection =
            (
                target.position -
                opticalCamera.transform.position
            ).normalized;

        angularError =
            Vector3.Angle(
                cameraDirection,
                targetDirection
            );
    }

    public Vector2 GetNormalizedError()
    {
        return normalizedError;
    }

    public float GetAngularError()
    {
        return angularError;
    }

    public bool IsTargetInFieldOfView()
    {
        return targetInFieldOfView;
    }
}