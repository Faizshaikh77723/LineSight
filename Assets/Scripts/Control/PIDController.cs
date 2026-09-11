using UnityEngine;

[System.Serializable]
public class PIDController
{
    [Header("PID Gains")]
    public float Kp = 100f;
    public float Ki = 0f;
    public float Kd = 10f;

    [Header("Output")]
    public float outputLimit = 60f;

    private float integral;

    private float previousError;

    private bool initialized;

    public float Update(
        float error,
        float deltaTime)
    {
        if (deltaTime <= 0f)
            return 0f;

        if (!initialized)
        {
            previousError =
                error;

            initialized = true;
        }

        integral +=
            error * deltaTime;

        float derivative =
            (error - previousError) /
            deltaTime;

        float output =
            Kp * error +
            Ki * integral +
            Kd * derivative;

        output =
            Mathf.Clamp(
                output,
                -outputLimit,
                outputLimit
            );

        previousError =
            error;

        return output;
    }

    public void Reset()
    {
        integral = 0f;

        previousError = 0f;

        initialized = false;
    }
}