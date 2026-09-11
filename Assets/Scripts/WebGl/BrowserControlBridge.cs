using UnityEngine;

public class BrowserControlBridge : MonoBehaviour
{
    [Header("Parameter Controller")]
    public ParameterController parameters;


    // =========================================================
    // SATELLITE
    // =========================================================

    public void BrowserSetSatelliteSpeed(string value)
    {
        if (parameters != null)
            parameters.SetSatelliteSpeed(value);
    }


    public void BrowserSetSatellitePositionX(string value)
    {
        if (parameters != null)
            parameters.SetSatellitePositionX(value);
    }


    public void BrowserSetSatellitePositionY(string value)
    {
        if (parameters != null)
            parameters.SetSatellitePositionY(value);
    }


    public void BrowserSetSatellitePositionZ(string value)
    {
        if (parameters != null)
            parameters.SetSatellitePositionZ(value);
    }


    public void BrowserSetLateralMotion(string value)
    {
        if (parameters != null)
            parameters.SetLateralMotion(value);
    }


    public void BrowserSetLateralAmplitude(string value)
    {
        if (parameters != null)
            parameters.SetLateralAmplitude(value);
    }


    public void BrowserSetLateralFrequency(string value)
    {
        if (parameters != null)
            parameters.SetLateralFrequency(value);
    }


    // =========================================================
    // DISTURBANCES
    // =========================================================

    public void BrowserSetDisturbancesEnabled(string value)
    {
        if (parameters != null)
            parameters.SetDisturbancesEnabled(value);
    }


    public void BrowserSetMeasurementNoiseEnabled(string value)
    {
        if (parameters != null)
            parameters.SetMeasurementNoiseEnabled(value);
    }


    public void BrowserSetMeasurementNoiseAmplitude(string value)
    {
        if (parameters != null)
            parameters.SetMeasurementNoiseAmplitude(value);
    }


    public void BrowserSetAngularJitterEnabled(string value)
    {
        if (parameters != null)
            parameters.SetAngularJitterEnabled(value);
    }


    public void BrowserSetAngularJitterAmplitude(string value)
    {
        if (parameters != null)
            parameters.SetAngularJitterAmplitude(value);
    }


    public void BrowserSetAngularJitterFrequency(string value)
    {
        if (parameters != null)
            parameters.SetAngularJitterFrequency(value);
    }


    public void BrowserSetDropoutEnabled(string value)
    {
        if (parameters != null)
            parameters.SetDropoutEnabled(value);
    }


    public void BrowserSetDropoutProbability(string value)
    {
        if (parameters != null)
            parameters.SetDropoutProbability(value);
    }


    public void BrowserSetVibrationEnabled(string value)
    {
        if (parameters != null)
            parameters.SetVibrationEnabled(value);
    }


    public void BrowserSetVibrationAmplitude(string value)
    {
        if (parameters != null)
            parameters.SetVibrationAmplitude(value);
    }


    public void BrowserSetVibrationFrequency(string value)
    {
        if (parameters != null)
            parameters.SetVibrationFrequency(value);
    }


    // =========================================================
    // PTZ
    // =========================================================

    public void BrowserSetPan(string value)
    {
        if (parameters != null)
            parameters.SetPan(value);
    }


    public void BrowserSetTilt(string value)
    {
        if (parameters != null)
            parameters.SetTilt(value);
    }


    // =========================================================
    // PAT
    // =========================================================

    public void BrowserSetAcquisitionThreshold(string value)
    {
        if (parameters != null)
            parameters.SetAcquisitionThreshold(value);
    }


    public void BrowserSetTrackingThreshold(string value)
    {
        if (parameters != null)
            parameters.SetTrackingThreshold(value);
    }


    public void BrowserSetLockThreshold(string value)
    {
        if (parameters != null)
            parameters.SetLockThreshold(value);
    }


    public void BrowserSetRequiredLockTime(string value)
    {
        if (parameters != null)
            parameters.SetRequiredLockTime(value);
    }


    public void BrowserSetMinimumConfidence(string value)
    {
        if (parameters != null)
            parameters.SetMinimumConfidence(value);
    }


    // =========================================================
    // KALMAN
    // =========================================================

    public void BrowserSetKalmanProcessNoise(string value)
    {
        if (parameters != null)
            parameters.SetKalmanProcessNoise(value);
    }


    public void BrowserSetKalmanMeasurementNoise(string value)
    {
        if (parameters != null)
            parameters.SetKalmanMeasurementNoise(value);
    }


    // =========================================================
    // FSOC
    // =========================================================

    public void BrowserSetTransmittedPower(string value)
    {
        if (parameters != null)
            parameters.SetTransmittedPower(value);
    }


    public void BrowserSetBeamDivergence(string value)
    {
        if (parameters != null)
            parameters.SetBeamDivergence(value);
    }


    public void BrowserSetReceiverDiameter(string value)
    {
        if (parameters != null)
            parameters.SetReceiverDiameter(value);
    }


    public void BrowserSetAtmosphericAttenuation(string value)
    {
        if (parameters != null)
            parameters.SetAtmosphericAttenuation(value);
    }


    public void BrowserSetTurbulenceStrength(string value)
    {
        if (parameters != null)
            parameters.SetTurbulenceStrength(value);
    }


    public void BrowserSetMaximumPointingError(string value)
    {
        if (parameters != null)
            parameters.SetMaximumPointingError(value);
    }
}