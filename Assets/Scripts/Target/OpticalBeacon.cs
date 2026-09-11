using UnityEngine;

public class OpticalBeacon : MonoBehaviour
{
    [Header("Optical Source Parameters")]

    [Min(0f)]
    public float opticalPower = 1.0f;

    [Min(0f)]
    public float beamDivergence = 0.001f;

    public float wavelengthNm = 1550f;

    [Header("Beacon State")]

    public bool isActive = true;

    [Header("Visual Settings")]

    public Light beaconLight;

    private Renderer beaconRenderer;

    void Start()
    {
        beaconRenderer = GetComponent<Renderer>();

        UpdateVisualState();
    }

    void Update()
    {
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        if (beaconLight != null)
        {
            beaconLight.enabled = isActive;
        }

        if (beaconRenderer != null)
        {
            beaconRenderer.enabled = true;
        }
    }

    public float GetOpticalPower()
    {
        if (!isActive)
            return 0f;

        return opticalPower;
    }

    public float GetBeamDivergence()
    {
        return beamDivergence;
    }

    public float GetWavelength()
    {
        return wavelengthNm;
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void SetBeaconState(bool active)
    {
        isActive = active;
    }
}