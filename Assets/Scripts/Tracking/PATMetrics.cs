using UnityEngine;

public class PATMetrics : MonoBehaviour
{
    [Header("References")]
    public PATController patController;

    [Header("Timing")]
    public float acquisitionTime;

    public float lockDuration;

    private float acquisitionStartTime;
    private float lockStartTime;

    private bool acquisitionStarted;
    private bool lockStarted;

    private PATState previousState;

    void Start()
    {
        if (patController != null)
        {
            previousState =
                patController.GetCurrentState();
        }
    }

    void Update()
    {
        if (patController == null)
            return;

        PATState currentState =
            patController.GetCurrentState();

        DetectStateChanges(
            currentState
        );

        previousState =
            currentState;
    }

    private void DetectStateChanges(
        PATState currentState)
    {
        if (currentState == PATState.Acquiring &&
            previousState == PATState.Searching)
        {
            acquisitionStartTime =
                Time.time;

            acquisitionStarted = true;
        }

        if (currentState == PATState.Locked &&
            !lockStarted)
        {
            if (acquisitionStarted)
            {
                acquisitionTime =
                    Time.time -
                    acquisitionStartTime;
            }

            lockStartTime =
                Time.time;

            lockStarted = true;
        }

        if (currentState != PATState.Locked)
        {
            lockStarted = false;
        }
    }

    public float GetAcquisitionTime()
    {
        return acquisitionTime;
    }
}