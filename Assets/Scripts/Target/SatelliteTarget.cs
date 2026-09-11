using UnityEngine;

public class SatelliteTarget : MonoBehaviour
{
    [Header("Target Velocity")]
    public Vector3 velocity =
        new Vector3(0f, 0f, -2f);

    [Header("Optional Lateral Motion")]
    public bool useLateralMotion = true;

    public float lateralAmplitude = 2f;

    public float lateralFrequency = 0.5f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private float simulationTime;

    void Start()
    {
        SaveInitialState();
    }

    void Update()
    {
        simulationTime += Time.deltaTime;

        float lateralOffset =
            useLateralMotion
            ? Mathf.Sin(
                simulationTime *
                lateralFrequency
            ) * lateralAmplitude
            : 0f;

        transform.position =
            initialPosition +
            velocity * simulationTime +
            Vector3.right *
            lateralOffset;
    }

    public void SaveInitialState()
    {
        initialPosition =
            transform.position;

        initialRotation =
            transform.rotation;

        simulationTime = 0f;
    }

    public void ResetTarget()
    {
        transform.position =
            initialPosition;

        transform.rotation =
            initialRotation;

        simulationTime = 0f;
    }

    public void SetInitialPosition(
        Vector3 position
    )
    {
        initialPosition = position;

        transform.position =
            position;

        simulationTime = 0f;
    }

    public Vector3 GetInitialPosition()
    {
        return initialPosition;
    }
}