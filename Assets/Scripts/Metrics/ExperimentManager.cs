using UnityEngine;

public class ExperimentManager : MonoBehaviour
{
    [Header("References")]
    public ExperimentMetrics metrics;
    public ExperimentLogger logger;

    [Header("Experiment")]
    public bool experimentRunning;

    public void StartExperiment()
    {
        if (metrics == null || logger == null)
        {
            Debug.LogError(
                "ExperimentManager: Metrics or Logger reference missing."
            );

            return;
        }

        metrics.StartExperiment();
        logger.StartLogging();

        experimentRunning = true;

        Debug.Log("Experiment started.");
    }

    public void StopExperiment()
    {
        if (metrics == null || logger == null)
            return;

        metrics.StopExperiment();
        logger.StopLogging();

        experimentRunning = false;

        Debug.Log("Experiment stopped.");
    }

    public void ResetExperiment()
    {
        if (metrics != null)
            metrics.ResetMetrics();

        if (logger != null)
            logger.StopLogging();

        experimentRunning = false;

        Debug.Log("Experiment reset.");
    }

    public ExperimentMetrics GetMetrics()
    {
        return metrics;
    }

    public ExperimentLogger GetLogger()
    {
        return logger;
    }

    public bool IsRunning()
    {
        return experimentRunning;
    }
}