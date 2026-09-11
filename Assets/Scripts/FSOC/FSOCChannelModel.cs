using UnityEngine;

public class FSOCChannelModel : MonoBehaviour
{
    [Header("References")]
    public Transform transmitter;
    public Transform receiver;

    public BeaconDetector detector;
    public DisturbanceModel disturbanceModel;

    [Header("Link Thresholds")]
    [Range(0f, 1f)]
    public float linkAvailableThreshold = 0.20f;

    [Range(0f, 1f)]
    public float linkDegradedThreshold = 0.05f;

    public bool linkAvailable;
    public bool linkDegraded;

    [Header("Transmitter")]
    [Tooltip("Transmitted optical power in watts.")]
    public float transmittedPower = 1.0f;

    [Tooltip("Transmitter optical efficiency.")]
    [Range(0f, 1f)]
    public float transmitterEfficiency = 0.8f;

    [Header("Beam")]
    [Tooltip("Beam divergence in radians.")]
    public float beamDivergence = 0.001f;

    [Header("Receiver")]
    [Tooltip("Receiver aperture diameter in meters.")]
    public float receiverDiameter = 0.10f;

    [Range(0f, 1f)]
    public float receiverEfficiency = 0.8f;

    [Header("Atmosphere")]
    [Tooltip("Atmospheric attenuation in dB/km.")]
    public float atmosphericAttenuationDbPerKm = 0.2f;

    [Header("Pointing")]
    [Tooltip("Maximum allowable pointing error for normalized link quality.")]
    public float maximumPointingErrorDegrees = 5f;

    [Header("Turbulence")]
    [Range(0f, 1f)]
    public float turbulenceStrength = 0.10f;

    [Header("Outputs")]
    public float rangeMeters;
    public float pointingErrorDegrees;
    public float geometricLoss;
    public float pointingLoss;
    public float atmosphericLoss;
    public float turbulenceFactor;
    public float receivedPower;
    public float linkQuality;

    void Update()
    {
        CalculateLink();
    }

    private void CalculateLink()
    {
        if (transmitter == null ||
            receiver == null)
        {
            ResetOutputs();
            return;
        }

        CalculateRange();

        CalculatePointingError();

        CalculateGeometricLoss();

        CalculatePointingLoss();

        CalculateAtmosphericLoss();

        CalculateTurbulence();

        CalculateReceivedPower();

        CalculateLinkQuality();
    }

    private void CalculateRange()
    {
        rangeMeters =
            Vector3.Distance(
                transmitter.position,
                receiver.position
            );
    }

    private void CalculatePointingError()
    {
        if (detector != null)
        {
            pointingErrorDegrees =
                detector.GetAngularError();
        }
        else
        {
            pointingErrorDegrees = 0f;
        }

        if (disturbanceModel != null)
        {
            Vector2 jitter =
                disturbanceModel.GetAngularJitter();

            float jitterMagnitude =
                jitter.magnitude;

            pointingErrorDegrees +=
                jitterMagnitude;
        }
    }

    private void CalculateGeometricLoss()
    {
        if (rangeMeters <= 0f)
        {
            geometricLoss = 0f;
            return;
        }

        float beamRadius =
            rangeMeters *
            beamDivergence;

        float beamArea =
            Mathf.PI *
            beamRadius *
            beamRadius;

        float receiverArea =
            Mathf.PI *
            Mathf.Pow(
                receiverDiameter * 0.5f,
                2f
            );

        geometricLoss =
            receiverArea /
            Mathf.Max(
                beamArea,
                0.000001f
            );

        geometricLoss =
            Mathf.Clamp01(
                geometricLoss
            );
    }

    private void CalculatePointingLoss()
    {
        float normalizedError =
            pointingErrorDegrees /
            Mathf.Max(
                maximumPointingErrorDegrees,
                0.001f
            );

        pointingLoss =
            Mathf.Exp(
                -2f *
                normalizedError *
                normalizedError
            );

        pointingLoss =
            Mathf.Clamp01(
                pointingLoss
            );
    }

    private void CalculateAtmosphericLoss()
    {
        float rangeKm =
            rangeMeters / 1000f;

        float attenuationDb =
            atmosphericAttenuationDbPerKm *
            rangeKm;

        atmosphericLoss =
            Mathf.Pow(
                10f,
                -attenuationDb / 10f
            );

        atmosphericLoss =
            Mathf.Clamp01(
                atmosphericLoss
            );
    }

    private void CalculateTurbulence()
    {
        if (turbulenceStrength <= 0f)
        {
            turbulenceFactor = 1f;
            return;
        }

        float randomFactor =
            Random.Range(
                -turbulenceStrength,
                turbulenceStrength
            );

        turbulenceFactor =
            1f + randomFactor;

        turbulenceFactor =
            Mathf.Clamp(
                turbulenceFactor,
                0f,
                1f
            );
    }

    private void CalculateReceivedPower()
    {
        receivedPower =
            transmittedPower *
            transmitterEfficiency *
            receiverEfficiency *
            geometricLoss *
            pointingLoss *
            atmosphericLoss *
            turbulenceFactor;

        receivedPower =
            Mathf.Max(
                receivedPower,
                0f
            );
    }

    private void CalculateLinkQuality()
{
    float normalizedPower =
        receivedPower /
        Mathf.Max(
            transmittedPower,
            0.000001f
        );

    linkQuality =
        Mathf.Clamp01(
            normalizedPower
        );

    linkAvailable =
        linkQuality >=
        linkAvailableThreshold;

    linkDegraded =
        linkQuality >=
        linkDegradedThreshold &&
        linkQuality <
        linkAvailableThreshold;
}
    private void ResetOutputs()
    {
        rangeMeters = 0f;
        pointingErrorDegrees = 0f;
        geometricLoss = 0f;
        pointingLoss = 0f;
        atmosphericLoss = 0f;
        turbulenceFactor = 0f;
        receivedPower = 0f;
        linkQuality = 0f;
    }

    public float GetReceivedPower()
    {
        return receivedPower;
    }

    public float GetLinkQuality()
    {
        return linkQuality;
    }

    public float GetRange()
    {
        return rangeMeters;
    }

    public float GetPointingError()
    {
        return pointingErrorDegrees;
    }
}