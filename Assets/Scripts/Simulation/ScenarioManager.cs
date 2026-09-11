using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public enum ScenarioType
    {
        Nominal,
        SensorNoise,
        AngularJitter,
        DetectionDropout,
        LongRange,
        SevereEnvironment
    }

    public ExperimentMetrics metrics;

    [Header("References")]
    public DisturbanceModel disturbance;
    public FSOCChannelModel fsoc;
    public SatelliteTarget target;

    [Header("Current Scenario")]
    public ScenarioType currentScenario =
        ScenarioType.Nominal;

    public void ApplyScenario(ScenarioType scenario)
    {
        currentScenario = scenario;
        ExperimentMetrics metrics =
            GetComponent<ExperimentMetrics>();
        if (metrics != null)
        {
            metrics.SetScenarioName(
                scenario.ToString()
            );
        }

        switch (scenario)
        {
            case ScenarioType.Nominal:
                ApplyNominal();
                break;

            case ScenarioType.SensorNoise:
                ApplySensorNoise();
                break;

            case ScenarioType.AngularJitter:
                ApplyAngularJitter();
                break;

            case ScenarioType.DetectionDropout:
                ApplyDetectionDropout();
                break;

            case ScenarioType.LongRange:
                ApplyLongRange();
                break;

            case ScenarioType.SevereEnvironment:
                ApplySevereEnvironment();
                break;
        }

        Debug.Log(
            "Scenario applied: " +
            scenario
        );
    }

    private void ApplyNominal()
    {
        if (disturbance != null)
        {
            disturbance.disturbancesEnabled = false;

            disturbance.measurementNoiseEnabled = false;
            disturbance.angularJitterEnabled = false;
            disturbance.dropoutEnabled = false;
            disturbance.vibrationEnabled = false;
        }

        if (fsoc != null)
        {
            fsoc.atmosphericAttenuationDbPerKm = 0.2f;
            fsoc.turbulenceStrength = 0f;
        }

        if (target != null)
        {
        target.SetInitialPosition(
            new Vector3(0f, 0f, 100f)
        );

        target.velocity =
            new Vector3(0f, 0f, 1f);

        target.lateralAmplitude = 3f;
        target.lateralFrequency = 0.5f;
        }
    }

    private void ApplySensorNoise()
    {
        if (disturbance != null)
        {
            disturbance.disturbancesEnabled = true;

            disturbance.measurementNoiseEnabled = true;
            disturbance.measurementNoiseAmplitude = 0.02f;

            disturbance.angularJitterEnabled = false;
            disturbance.dropoutEnabled = false;
            disturbance.vibrationEnabled = false;
        }

        if (fsoc != null)
        {
            fsoc.turbulenceStrength = 0f;
        }
    }

    private void ApplyAngularJitter()
    {
        if (disturbance != null)
        {
            disturbance.disturbancesEnabled = true;

            disturbance.measurementNoiseEnabled = false;

            disturbance.angularJitterEnabled = true;
            disturbance.angularJitterAmplitude = 0.5f;
            disturbance.angularJitterFrequency = 8f;

            disturbance.dropoutEnabled = false;
            disturbance.vibrationEnabled = true;
        }

        if (fsoc != null)
        {
            fsoc.turbulenceStrength = 0.05f;
        }
    }

    private void ApplyDetectionDropout()
    {
        if (disturbance != null)
        {
            disturbance.disturbancesEnabled = true;

            disturbance.measurementNoiseEnabled = true;
            disturbance.measurementNoiseAmplitude = 0.01f;

            disturbance.angularJitterEnabled = false;

            disturbance.dropoutEnabled = true;
            disturbance.dropoutProbability = 0.02f;

            disturbance.vibrationEnabled = false;
        }

        if (fsoc != null)
        {
            fsoc.turbulenceStrength = 0.05f;
        }
    }

    private void ApplyLongRange()
    {
        if (disturbance != null)
        {
            disturbance.disturbancesEnabled = true;

            disturbance.measurementNoiseEnabled = true;
            disturbance.measurementNoiseAmplitude = 0.01f;

            disturbance.angularJitterEnabled = true;
            disturbance.angularJitterAmplitude = 0.2f;

            disturbance.dropoutEnabled = false;
            disturbance.vibrationEnabled = false;
        }

        if (fsoc != null)
        {
            fsoc.atmosphericAttenuationDbPerKm = 0.3f;
            fsoc.turbulenceStrength = 0.10f;
        }

        if (target != null)
        {
            target.velocity =
                new Vector3(0f, 0f, 2f);
        }
    }

    private void ApplySevereEnvironment()
    {
        if (disturbance != null)
        {
            disturbance.disturbancesEnabled = true;

            disturbance.measurementNoiseEnabled = true;
            disturbance.measurementNoiseAmplitude = 0.03f;

            disturbance.angularJitterEnabled = true;
            disturbance.angularJitterAmplitude = 0.75f;
            disturbance.angularJitterFrequency = 10f;

            disturbance.dropoutEnabled = true;
            disturbance.dropoutProbability = 0.03f;

            disturbance.vibrationEnabled = true;
            disturbance.vibrationAmplitude = 0.02f;
            disturbance.vibrationFrequency = 15f;
        }

        if (fsoc != null)
        {
            fsoc.atmosphericAttenuationDbPerKm = 0.5f;
            fsoc.turbulenceStrength = 0.20f;
        }

        if (target != null)
        {
            target.velocity =
                new Vector3(1f, 0f, -1.5f);

            target.lateralAmplitude = 3f;
            target.lateralFrequency = 0.8f;
        }
    }

    public void ApplyNominalScenario()
    {
        ApplyScenario(ScenarioType.Nominal);
    }

    public void ApplySensorNoiseScenario()
    {
        ApplyScenario(ScenarioType.SensorNoise);
    }

    public void ApplyAngularJitterScenario()
    {
        ApplyScenario(ScenarioType.AngularJitter);
    }

    public void ApplyDetectionDropoutScenario()
    {
        ApplyScenario(ScenarioType.DetectionDropout);
    }

    public void ApplyLongRangeScenario()
    {
        ApplyScenario(ScenarioType.LongRange);
    }

    public void ApplySevereEnvironmentScenario()
    {
        ApplyScenario(ScenarioType.SevereEnvironment);
    }
}