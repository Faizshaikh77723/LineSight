using UnityEngine;

public class BeaconDetector : MonoBehaviour
{
    [Header("Camera")]
    public Camera opticalCamera;

    [Header("Target")]
    public Transform beacon;

    [Header("Disturbances")]
    public DisturbanceModel disturbanceModel;

    [Header("Detection")]
    public bool beaconDetected;

    public Vector2 detectedViewportPosition;
    public Vector2 detectedPixelPosition;

    [Header("Detection Quality")]
    [Range(0f, 1f)]
    public float detectionConfidence;

    [Header("Image Error")]
    public Vector2 normalizedImageError;

    [Header("Angular Error")]
    public float angularError;

    void Update()
    {
        DetectBeacon();
    }

    private void DetectBeacon()
    {
        if (opticalCamera == null ||
            beacon == null)
        {
            ResetDetection();
            return;
        }

        Vector3 viewportPosition =
            opticalCamera.WorldToViewportPoint(
                beacon.position
            );

        bool visible =
            viewportPosition.z > 0f &&
            viewportPosition.x >= 0f &&
            viewportPosition.x <= 1f &&
            viewportPosition.y >= 0f &&
            viewportPosition.y <= 1f;

        if (!visible)
        {
            ResetDetection();
            return;
        }

        // Temporary detection dropout
        if (disturbanceModel != null &&
            disturbanceModel.ShouldDropDetection())
        {
            ResetDetection();
            return;
        }

        // Apply measurement noise
        Vector2 noisyViewportPosition =
            new Vector2(
                viewportPosition.x,
                viewportPosition.y
            );

        if (disturbanceModel != null)
        {
            noisyViewportPosition +=
                disturbanceModel.GetMeasurementNoise();
        }

        // Apply angular jitter as normalized image displacement
        if (disturbanceModel != null)
        {
            Vector2 jitter =
                disturbanceModel.GetAngularJitter();

            float horizontalFov =
                opticalCamera.fieldOfView *
                opticalCamera.aspect;

            float verticalFov =
                opticalCamera.fieldOfView;

            float horizontalNormalized =
                Mathf.Tan(
                    jitter.x *
                    Mathf.Deg2Rad
                ) /
                Mathf.Tan(
                    horizontalFov *
                    0.5f *
                    Mathf.Deg2Rad
                );

            float verticalNormalized =
                Mathf.Tan(
                    jitter.y *
                    Mathf.Deg2Rad
                ) /
                Mathf.Tan(
                    verticalFov *
                    0.5f *
                    Mathf.Deg2Rad
                );

            noisyViewportPosition +=
                new Vector2(
                    horizontalNormalized * 0.5f,
                    verticalNormalized * 0.5f
                );
        }

        beaconDetected = true;

        detectedViewportPosition =
            noisyViewportPosition;

        int pixelWidth =
            opticalCamera.pixelWidth;

        int pixelHeight =
            opticalCamera.pixelHeight;

        detectedPixelPosition =
            new Vector2(
                noisyViewportPosition.x *
                pixelWidth,

                noisyViewportPosition.y *
                pixelHeight
            );

        normalizedImageError =
            new Vector2(
                noisyViewportPosition.x - 0.5f,
                noisyViewportPosition.y - 0.5f
            );

        // Calculate angular error from disturbed measurement
        angularError =
            CalculateAngularError(
                normalizedImageError
            );

        detectionConfidence =
            CalculateConfidence(
                normalizedImageError,
                angularError
            );
    }

    private float CalculateAngularError(
        Vector2 imageError)
    {
        float horizontalFov =
            opticalCamera.fieldOfView *
            opticalCamera.aspect;

        float verticalFov =
            opticalCamera.fieldOfView;

        float angleX =
            Mathf.Atan(
                imageError.x * 2f *
                Mathf.Tan(
                    horizontalFov *
                    0.5f *
                    Mathf.Deg2Rad
                )
            ) *
            Mathf.Rad2Deg;

        float angleY =
            Mathf.Atan(
                imageError.y * 2f *
                Mathf.Tan(
                    verticalFov *
                    0.5f *
                    Mathf.Deg2Rad
                )
            ) *
            Mathf.Rad2Deg;

        return Mathf.Sqrt(
            angleX * angleX +
            angleY * angleY
        );
    }

    private float CalculateConfidence(
        Vector2 imageError,
        float angle)
    {
        float distance =
            imageError.magnitude;

        float confidence =
            1f -
            Mathf.Clamp01(
                distance * 2f
            );

        if (angle > 10f)
        {
            confidence *= 0.5f;
        }

        return Mathf.Clamp01(confidence);
    }

    private void ResetDetection()
    {
        beaconDetected = false;

        detectedViewportPosition =
            Vector2.zero;

        detectedPixelPosition =
            Vector2.zero;

        normalizedImageError =
            Vector2.zero;

        angularError = 0f;

        detectionConfidence = 0f;
    }

    public bool IsDetected()
    {
        return beaconDetected;
    }

    public Vector2 GetDetectedPosition()
    {
        return detectedViewportPosition;
    }

    public Vector2 GetDetectedPixelPosition()
    {
        return detectedPixelPosition;
    }

    public Vector2 GetImageError()
    {
        return normalizedImageError;
    }

    public float GetAngularError()
    {
        return angularError;
    }

    public float GetConfidence()
    {
        return detectionConfidence;
    }
}