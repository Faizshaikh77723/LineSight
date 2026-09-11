using UnityEngine;

public class ExperimentMetrics : MonoBehaviour
{
    [Header("Scenario")]
    public string scenarioName = "Unknown";

    [Header("References")]
    public BeaconDetector detector;
    public PATController patController;
    public FSOCChannelModel fsoc;
    public DisturbanceModel disturbance;

    [Header("Experiment")]
    public bool experimentRunning;

    public float experimentTime;

    [Header("PAT Metrics")]
    public float acquisitionTime = -1f;
    public float lockDuration;
    public float trackingAvailability;
    public float detectionAvailability;

    public float rmsPointingError;
    public float averagePointingError;
    public float maximumPointingError;

    [Header("FSOC Metrics")]
    public float linkAvailability;
    public float averageReceivedPower;
    public float minimumReceivedPower = float.MaxValue;
    public float averageLinkQuality;
    public float minimumLinkQuality = float.MaxValue;

    [Header("Event Counters")]
    public int detectionDropouts;
    public int lockLosses;

    private float pointingErrorSquaredSum;
    private float pointingErrorSum;

    private float receivedPowerSum;
    private float linkQualitySum;

    private float totalMeasurementTime;

    private float trackingTime;
    private float detectionTime;
    private float linkAvailableTime;

    private bool wasLocked;
    private bool wasDetected;

    private int sampleCount;

    public void SetScenarioName(string name)
    {
        scenarioName = name;
    }

    public void StartExperiment()
    {
        ResetMetrics();

        experimentRunning = true;
    }

    public void StopExperiment()
    {
        experimentRunning = false;

        CalculateFinalMetrics();
    }

    private void Update()
    {
        if (!experimentRunning)
            return;

        CollectSample();
    }

    private void CollectSample()
    {
        float dt = Time.deltaTime;

        experimentTime += dt;
        totalMeasurementTime += dt;

        sampleCount++;

        CollectPATMetrics(dt);
        CollectFSOCMetrics(dt);
        CollectEventMetrics();
    }

    private void CollectPATMetrics(float dt)
    {
        if (detector == null || patController == null)
            return;

        bool detected = detector.IsDetected();
        bool tracking = patController.IsTracking();
        bool locked = patController.IsLocked();

        float error = detector.GetAngularError();

        if (detected)
        {
            detectionTime += dt;

            pointingErrorSum += error;
            pointingErrorSquaredSum += error * error;

            averagePointingError =
                pointingErrorSum /
                Mathf.Max(experimentTime, 0.000001f);

            rmsPointingError =
                Mathf.Sqrt(
                    pointingErrorSquaredSum /
                    Mathf.Max(experimentTime, 0.000001f)
                );

            if (error > maximumPointingError)
                maximumPointingError = error;
        }

        if (tracking)
            trackingTime += dt;

        if (locked)
            lockDuration += dt;

        if (acquisitionTime < 0f && locked)
        {
            acquisitionTime = experimentTime;
        }
    }

    private void CollectFSOCMetrics(float dt)
    {
        if (fsoc == null)
            return;

        float receivedPower =
            fsoc.GetReceivedPower();

        float linkQuality =
            fsoc.GetLinkQuality();

        receivedPowerSum += receivedPower;
        linkQualitySum += linkQuality;

        averageReceivedPower =
            receivedPowerSum /
            Mathf.Max(experimentTime, 0.000001f);

        averageLinkQuality =
            linkQualitySum /
            Mathf.Max(experimentTime, 0.000001f);

        if (receivedPower < minimumReceivedPower)
            minimumReceivedPower = receivedPower;

        if (linkQuality < minimumLinkQuality)
            minimumLinkQuality = linkQuality;

        if (fsoc.linkAvailable)
            linkAvailableTime += dt;
    }

    private void CollectEventMetrics()
    {
        if (detector != null)
        {
            bool detected = detector.IsDetected();

            if (wasDetected && !detected)
                detectionDropouts++;

            wasDetected = detected;
        }

        if (patController != null)
        {
            bool locked = patController.IsLocked();

            if (wasLocked && !locked)
                lockLosses++;

            wasLocked = locked;
        }
    }

    private void CalculateFinalMetrics()
    {
        if (experimentTime > 0f)
        {
            detectionAvailability =
                detectionTime /
                experimentTime;

            trackingAvailability =
                trackingTime /
                experimentTime;

            linkAvailability =
                linkAvailableTime /
                experimentTime;
        }

        if (minimumReceivedPower == float.MaxValue)
            minimumReceivedPower = 0f;

        if (minimumLinkQuality == float.MaxValue)
            minimumLinkQuality = 0f;
    }

    public void ResetMetrics()
    {
        experimentRunning = false;

        experimentTime = 0f;

        acquisitionTime = -1f;
        lockDuration = 0f;

        trackingAvailability = 0f;
        detectionAvailability = 0f;
        linkAvailability = 0f;

        rmsPointingError = 0f;
        averagePointingError = 0f;
        maximumPointingError = 0f;

        averageReceivedPower = 0f;
        minimumReceivedPower = float.MaxValue;

        averageLinkQuality = 0f;
        minimumLinkQuality = float.MaxValue;

        detectionDropouts = 0;
        lockLosses = 0;

        pointingErrorSquaredSum = 0f;
        pointingErrorSum = 0f;

        receivedPowerSum = 0f;
        linkQualitySum = 0f;

        totalMeasurementTime = 0f;

        trackingTime = 0f;
        detectionTime = 0f;
        linkAvailableTime = 0f;

        wasLocked = false;
        wasDetected = false;

        sampleCount = 0;
    }

    public float GetExperimentTime()
    {
        return experimentTime;
    }

    public float GetAcquisitionTime()
    {
        return acquisitionTime;
    }

    public float GetLockDuration()
    {
        return lockDuration;
    }

    public float GetRMSPointingError()
    {
        return rmsPointingError;
    }

    public float GetMaximumPointingError()
    {
        return maximumPointingError;
    }

    public float GetDetectionAvailability()
    {
        return detectionAvailability;
    }

    public float GetTrackingAvailability()
    {
        return trackingAvailability;
    }

    public float GetLinkAvailability()
    {
        return linkAvailability;
    }
}