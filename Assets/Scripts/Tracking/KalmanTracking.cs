using UnityEngine;

public class KalmanTracking : MonoBehaviour
{
    [Header("References")]
    public BeaconDetector detector;

    [Header("Kalman Parameters")]
    [Tooltip("Higher values allow the filter to respond faster to target motion.")]
    public float processNoise = 0.01f;

    [Tooltip("Higher values make the filter trust measurements less.")]
    public float measurementNoise = 0.05f;

    [Header("Output")]
    public Vector2 rawMeasurement;
    public Vector2 filteredMeasurement;
    public Vector2 estimatedVelocity;

    private KalmanFilter2D filter;

    void Start()
    {
        CreateFilter();
    }

    void Update()
    {
        if (detector == null)
            return;

        if (!detector.IsDetected())
        {
            return;
        }

        UpdateFilter();
    }

    private void CreateFilter()
    {
        filter =
            new KalmanFilter2D(
                processNoise,
                measurementNoise
            );
    }

    private void UpdateFilter()
    {
        rawMeasurement =
            detector.GetImageError();

        filteredMeasurement =
            filter.Update(
                rawMeasurement,
                Time.deltaTime
            );

        estimatedVelocity =
            filter.GetVelocity();
    }

    public Vector2 GetFilteredError()
    {
        return filteredMeasurement;
    }

    public Vector2 GetEstimatedVelocity()
    {
        return estimatedVelocity;
    }

    public bool IsInitialized()
    {
        return filter != null &&
               filter.IsInitialized();
    }

    public void ResetFilter()
    {
        CreateFilter();

        rawMeasurement = Vector2.zero;

        filteredMeasurement = Vector2.zero;

        estimatedVelocity = Vector2.zero;
    }
}