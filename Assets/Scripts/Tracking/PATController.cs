using UnityEngine;

public class PATController : MonoBehaviour
{
    [Header("References")]
    public BeaconDetector detector;

    [Header("PAT Thresholds")]
    [Tooltip("Maximum angular error to begin acquisition.")]
    public float acquisitionThresholdDegrees = 5f;

    [Tooltip("Maximum angular error required for tracking.")]
    public float trackingThresholdDegrees = 2f;

    [Tooltip("Maximum angular error required for lock.")]
    public float lockThresholdDegrees = 1f;

    [Header("Lock Requirements")]
    [Tooltip("Time required inside the lock threshold.")]
    public float requiredLockTime = 1f;

    [Range(0f, 1f)]
    public float minimumConfidence = 0.5f;

    [Header("Current State")]
    public PATState currentState = PATState.Searching;

    [Header("Runtime Values")]
    public float lockTimer;
    public float stateTime;

    private PATState previousState;

    void Start()
    {
        currentState = PATState.Searching;
        previousState = currentState;

        lockTimer = 0f;
        stateTime = 0f;
    }

    void Update()
    {
        stateTime += Time.deltaTime;

        UpdatePATState();
    }

    private void UpdatePATState()
    {
        if (detector == null)
        {
            SetState(PATState.Searching);
            return;
        }

        bool detected = detector.IsDetected();

        float angularError = detector.GetAngularError();
        float confidence = detector.GetConfidence();

        bool validMeasurement =
            detected &&
            confidence >= minimumConfidence;

        if (!validMeasurement)
        {
            lockTimer = 0f;
            SetState(PATState.Searching);
            return;
        }

        switch (currentState)
        {
            case PATState.Searching:

                lockTimer = 0f;

                if (angularError <= acquisitionThresholdDegrees)
                {
                    SetState(PATState.Acquiring);
                }

                break;

            case PATState.Acquiring:

                if (angularError > acquisitionThresholdDegrees)
                {
                    lockTimer = 0f;
                    SetState(PATState.Searching);
                }
                else if (angularError <= trackingThresholdDegrees)
                {
                    SetState(PATState.Tracking);
                }

                break;

            case PATState.Tracking:

                if (angularError > trackingThresholdDegrees)
                {
                    lockTimer = 0f;
                    SetState(PATState.Acquiring);
                }
                else if (angularError <= lockThresholdDegrees)
                {
                    lockTimer += Time.deltaTime;

                    if (lockTimer >= requiredLockTime)
                    {
                        SetState(PATState.Locked);
                    }
                }
                else
                {
                    lockTimer = 0f;
                }

                break;

            case PATState.Locked:

                if (angularError > lockThresholdDegrees)
                {
                    lockTimer = 0f;
                    SetState(PATState.Tracking);
                }

                break;
        }
    }

    private void SetState(PATState newState)
    {
        if (currentState == newState)
            return;

        previousState = currentState;
        currentState = newState;
        stateTime = 0f;
    }
    public PATState GetState()
    {
        return currentState;
    }

    public PATState GetCurrentState()
    {
        return currentState;
    }

    public bool IsLocked()
    {
        return currentState == PATState.Locked;
    }

    public bool IsTracking()
    {
        return currentState == PATState.Tracking ||
            currentState == PATState.Locked;
    }

    public float GetLockTimer()
    {
        return lockTimer;
    }

    public float GetStateTime()
    {
        return stateTime;
    }
}