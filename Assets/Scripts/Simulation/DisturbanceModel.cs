using UnityEngine;

public class DisturbanceModel : MonoBehaviour
{
    [Header("Master Control")]
    public bool disturbancesEnabled = true;

    [Header("Measurement Noise")]
    public bool measurementNoiseEnabled = true;

    [Tooltip("Maximum normalized image-space measurement noise.")]
    public float measurementNoiseAmplitude = 0.01f;

    [Header("Angular Jitter")]
    public bool angularJitterEnabled = true;

    [Tooltip("Maximum angular jitter in degrees.")]
    public float angularJitterAmplitude = 0.15f;

    [Tooltip("Frequency of angular jitter.")]
    public float angularJitterFrequency = 8f;

    [Header("Detection Dropout")]
    public bool dropoutEnabled = true;

    [Range(0f, 1f)]
    [Tooltip("Probability of temporary detection loss per frame.")]
    public float dropoutProbability = 0.002f;

    [Header("Vibration")]
    public bool vibrationEnabled = true;

    [Tooltip("Small positional vibration amplitude in meters.")]
    public float vibrationAmplitude = 0.01f;

    [Tooltip("Vibration frequency in Hz.")]
    public float vibrationFrequency = 12f;

    private float simulationTime;

    void Update()
    {
        simulationTime += Time.deltaTime;
    }

    public Vector2 GetMeasurementNoise()
    {
        if (!disturbancesEnabled ||
            !measurementNoiseEnabled)
        {
            return Vector2.zero;
        }

        return new Vector2(
            Random.Range(
                -measurementNoiseAmplitude,
                measurementNoiseAmplitude
            ),
            Random.Range(
                -measurementNoiseAmplitude,
                measurementNoiseAmplitude
            )
        );
    }

    public Vector2 GetAngularJitter()
    {
        if (!disturbancesEnabled ||
            !angularJitterEnabled)
        {
            return Vector2.zero;
        }

        float x =
            Mathf.Sin(
                simulationTime *
                angularJitterFrequency *
                2f *
                Mathf.PI
            ) *
            angularJitterAmplitude;

        float y =
            Mathf.Cos(
                simulationTime *
                angularJitterFrequency *
                1.37f *
                2f *
                Mathf.PI
            ) *
            angularJitterAmplitude;

        return new Vector2(x, y);
    }

    public bool ShouldDropDetection()
    {
        if (!disturbancesEnabled ||
            !dropoutEnabled)
        {
            return false;
        }

        return Random.value <
               dropoutProbability;
    }

    public Vector3 GetVibrationOffset()
    {
        if (!disturbancesEnabled ||
            !vibrationEnabled)
        {
            return Vector3.zero;
        }

        float x =
            Mathf.Sin(
                simulationTime *
                vibrationFrequency *
                2f *
                Mathf.PI
            );

        float y =
            Mathf.Sin(
                simulationTime *
                vibrationFrequency *
                1.31f *
                2f *
                Mathf.PI
            );

        float z =
            Mathf.Cos(
                simulationTime *
                vibrationFrequency *
                0.83f *
                2f *
                Mathf.PI
            );

        return new Vector3(
            x,
            y,
            z
        ) * vibrationAmplitude;
    }

    public float GetMeasurementNoiseAmplitude()
    {
        return measurementNoiseAmplitude;
    }

    public float GetJitterAmplitude()
    {
        return angularJitterAmplitude;
    }
}