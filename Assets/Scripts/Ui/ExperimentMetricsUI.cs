using UnityEngine;
using TMPro;

public class ExperimentMetricsUI : MonoBehaviour
{
    public ExperimentMetrics metrics;
    public TMP_Text metricsText;

    void Update()
    {
        if (metrics == null || metricsText == null)
            return;

        string acquisitionText =
            metrics.GetAcquisitionTime() < 0f
            ? "N/A"
            : metrics.GetAcquisitionTime().ToString("F2") + " s";

        metricsText.text =
            "EXPERIMENT METRICS\n" +
            "TIME: " +
            metrics.GetExperimentTime().ToString("F1") +
            " s\n" +

            "ACQUISITION: " +
            acquisitionText +
            "\n" +

            "LOCK DURATION: " +
            metrics.GetLockDuration().ToString("F2") +
            " s\n" +

            "RMS ERROR: " +
            metrics.GetRMSPointingError().ToString("F3") +
            " deg\n" +

            "MAX ERROR: " +
            metrics.GetMaximumPointingError().ToString("F3") +
            " deg\n" +

            "DETECTION AVAIL: " +
            (metrics.GetDetectionAvailability() * 100f).ToString("F1") +
            "%\n" +

            "TRACKING AVAIL: " +
            (metrics.GetTrackingAvailability() * 100f).ToString("F1") +
            "%\n" +

            "LINK AVAIL: " +
            (metrics.GetLinkAvailability() * 100f).ToString("F1") +
            "%\n" +

            "DROPOUTS: " +
            metrics.detectionDropouts +
            "\n" +

            "LOCK LOSSES: " +
            metrics.lockLosses;
    }
}