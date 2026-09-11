using System.Globalization;
using UnityEngine;

public class ParameterController : MonoBehaviour
{
    [Header("References")]
    public SatelliteTarget satellite;
    public DisturbanceModel disturbance;
    public PTZController ptz;
    public PATController pat;
    public KalmanTracking kalman;
    public FSOCChannelModel fsoc;

    [Header("Runtime Control")]
    public bool browserControlEnabled = true;


    // =========================================================
    // SATELLITE
    // =========================================================

    public void SetSatelliteSpeed(string value)
    {
        if (!CanControl() || satellite == null)
            return;

        if (!TryParseFloat(value, out float speed))
            return;

        speed = Mathf.Max(0f, speed);

        Vector3 direction = satellite.velocity;

        if (direction.sqrMagnitude < 0.0001f)
            direction = Vector3.forward;

        satellite.velocity =
            direction.normalized * speed;

        Debug.Log(
            "Browser control: Satellite speed = " +
            speed + " m/s"
        );
    }


    public void SetSatellitePositionX(string value)
    {
        if (!CanControl() || satellite == null)
            return;

        if (!TryParseFloat(value, out float x))
            return;

        Vector3 position =
            satellite.transform.position;

        position.x = x;

        satellite.SetInitialPosition(position);

        Debug.Log(
            "Browser control: Satellite X = " +
            x
        );
    }


    public void SetSatellitePositionY(string value)
    {
        if (!CanControl() || satellite == null)
            return;

        if (!TryParseFloat(value, out float y))
            return;

        Vector3 position =
            satellite.transform.position;

        position.y = y;

        satellite.SetInitialPosition(position);

        Debug.Log(
            "Browser control: Satellite Y = " +
            y
        );
    }


    public void SetSatellitePositionZ(string value)
    {
        if (!CanControl() || satellite == null)
            return;

        if (!TryParseFloat(value, out float z))
            return;

        Vector3 position =
            satellite.transform.position;

        position.z = z;

        satellite.SetInitialPosition(position);

        Debug.Log(
            "Browser control: Satellite Z = " +
            z
        );
    }


    public void SetLateralMotion(string value)
    {
        if (!CanControl() || satellite == null)
            return;

        satellite.useLateralMotion =
            ParseBool(value);

        Debug.Log(
            "Browser control: Lateral motion = " +
            satellite.useLateralMotion
        );
    }


    public void SetLateralAmplitude(string value)
    {
        if (!CanControl() || satellite == null)
            return;

        if (!TryParseFloat(value, out float amplitude))
            return;

        satellite.lateralAmplitude =
            Mathf.Max(0f, amplitude);
    }


    public void SetLateralFrequency(string value)
    {
        if (!CanControl() || satellite == null)
            return;

        if (!TryParseFloat(value, out float frequency))
            return;

        satellite.lateralFrequency =
            Mathf.Max(0f, frequency);
    }


    // =========================================================
    // DISTURBANCES
    // =========================================================

    public void SetDisturbancesEnabled(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        disturbance.disturbancesEnabled =
            ParseBool(value);

        Debug.Log(
            "Browser control: Disturbances = " +
            disturbance.disturbancesEnabled
        );
    }


    public void SetMeasurementNoiseEnabled(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        disturbance.measurementNoiseEnabled =
            ParseBool(value);
    }


    public void SetMeasurementNoiseAmplitude(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        if (!TryParseFloat(value, out float amplitude))
            return;

        disturbance.measurementNoiseAmplitude =
            Mathf.Max(0f, amplitude);
    }


    public void SetAngularJitterEnabled(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        disturbance.angularJitterEnabled =
            ParseBool(value);
    }


    public void SetAngularJitterAmplitude(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        if (!TryParseFloat(value, out float amplitude))
            return;

        disturbance.angularJitterAmplitude =
            Mathf.Max(0f, amplitude);
    }


    public void SetAngularJitterFrequency(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        if (!TryParseFloat(value, out float frequency))
            return;

        disturbance.angularJitterFrequency =
            Mathf.Max(0f, frequency);
    }


    public void SetDropoutEnabled(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        disturbance.dropoutEnabled =
            ParseBool(value);
    }


    public void SetDropoutProbability(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        if (!TryParseFloat(value, out float probability))
            return;

        disturbance.dropoutProbability =
            Mathf.Clamp01(probability);
    }


    public void SetVibrationEnabled(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        disturbance.vibrationEnabled =
            ParseBool(value);
    }


    public void SetVibrationAmplitude(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        if (!TryParseFloat(value, out float amplitude))
            return;

        disturbance.vibrationAmplitude =
            Mathf.Max(0f, amplitude);
    }


    public void SetVibrationFrequency(string value)
    {
        if (!CanControl() || disturbance == null)
            return;

        if (!TryParseFloat(value, out float frequency))
            return;

        disturbance.vibrationFrequency =
            Mathf.Max(0f, frequency);
    }


    // =========================================================
    // PTZ
    // =========================================================

    public void SetPan(string value)
    {
        if (!CanControl() || ptz == null)
            return;

        if (!TryParseFloat(value, out float pan))
            return;

        ptz.SetPanAngle(pan);

        Debug.Log(
            "Browser control: Pan = " +
            ptz.GetPanAngle()
        );
    }


    public void SetTilt(string value)
    {
        if (!CanControl() || ptz == null)
            return;

        if (!TryParseFloat(value, out float tilt))
            return;

        ptz.SetTiltAngle(tilt);

        Debug.Log(
            "Browser control: Tilt = " +
            ptz.GetTiltAngle()
        );
    }


    // =========================================================
    // PAT
    // =========================================================

    public void SetAcquisitionThreshold(string value)
    {
        if (!CanControl() || pat == null)
            return;

        if (!TryParseFloat(value, out float threshold))
            return;

        pat.acquisitionThresholdDegrees =
            Mathf.Max(0f, threshold);
    }


    public void SetTrackingThreshold(string value)
    {
        if (!CanControl() || pat == null)
            return;

        if (!TryParseFloat(value, out float threshold))
            return;

        pat.trackingThresholdDegrees =
            Mathf.Max(0f, threshold);
    }


    public void SetLockThreshold(string value)
    {
        if (!CanControl() || pat == null)
            return;

        if (!TryParseFloat(value, out float threshold))
            return;

        pat.lockThresholdDegrees =
            Mathf.Max(0f, threshold);
    }


    public void SetRequiredLockTime(string value)
    {
        if (!CanControl() || pat == null)
            return;

        if (!TryParseFloat(value, out float time))
            return;

        pat.requiredLockTime =
            Mathf.Max(0f, time);
    }


    public void SetMinimumConfidence(string value)
    {
        if (!CanControl() || pat == null)
            return;

        if (!TryParseFloat(value, out float confidence))
            return;

        pat.minimumConfidence =
            Mathf.Clamp01(confidence);
    }


    // =========================================================
    // KALMAN
    // =========================================================

    public void SetKalmanProcessNoise(string value)
    {
        if (!CanControl() || kalman == null)
            return;

        if (!TryParseFloat(value, out float noise))
            return;

        kalman.processNoise =
            Mathf.Max(0f, noise);
    }


    public void SetKalmanMeasurementNoise(string value)
    {
        if (!CanControl() || kalman == null)
            return;

        if (!TryParseFloat(value, out float noise))
            return;

        kalman.measurementNoise =
            Mathf.Max(0f, noise);
    }


    // =========================================================
    // FSOC
    // =========================================================

    public void SetTransmittedPower(string value)
    {
        if (!CanControl() || fsoc == null)
            return;

        if (!TryParseFloat(value, out float power))
            return;

        fsoc.transmittedPower =
            Mathf.Max(0f, power);
    }


    public void SetBeamDivergence(string value)
    {
        if (!CanControl() || fsoc == null)
            return;

        if (!TryParseFloat(value, out float divergence))
            return;

        fsoc.beamDivergence =
            Mathf.Max(0f, divergence);
    }


    public void SetReceiverDiameter(string value)
    {
        if (!CanControl() || fsoc == null)
            return;

        if (!TryParseFloat(value, out float diameter))
            return;

        fsoc.receiverDiameter =
            Mathf.Max(0f, diameter);
    }


    public void SetAtmosphericAttenuation(string value)
    {
        if (!CanControl() || fsoc == null)
            return;

        if (!TryParseFloat(value, out float attenuation))
            return;

        fsoc.atmosphericAttenuationDbPerKm =
            Mathf.Max(0f, attenuation);
    }


    public void SetTurbulenceStrength(string value)
    {
        if (!CanControl() || fsoc == null)
            return;

        if (!TryParseFloat(value, out float turbulence))
            return;

        fsoc.turbulenceStrength =
            Mathf.Clamp01(turbulence);
    }


    public void SetMaximumPointingError(string value)
    {
        if (!CanControl() || fsoc == null)
            return;

        if (!TryParseFloat(value, out float error))
            return;

        fsoc.maximumPointingErrorDegrees =
            Mathf.Max(0.001f, error);
    }


    // =========================================================
    // HELPERS
    // =========================================================

    private bool CanControl()
    {
        return browserControlEnabled;
    }


    private bool ParseBool(string value)
    {
        if (string.IsNullOrEmpty(value))
            return false;

        value =
            value.Trim().ToLowerInvariant();

        return value == "1" ||
               value == "true" ||
               value == "on" ||
               value == "yes";
    }


    private bool TryParseFloat(
        string value,
        out float result)
    {
        return float.TryParse(
            value,
            NumberStyles.Float,
            CultureInfo.InvariantCulture,
            out result
        );
    }
}