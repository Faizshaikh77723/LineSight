using UnityEngine;

public class KalmanFilter2D
{
    private Vector4 state;

    private float[,] covariance =
        new float[4, 4];

    private float processNoise;
    private float measurementNoise;

    private bool initialized;

    public KalmanFilter2D(
        float processNoise,
        float measurementNoise)
    {
        this.processNoise =
            processNoise;

        this.measurementNoise =
            measurementNoise;

        Reset();
    }

    public void Reset()
    {
        state = Vector4.zero;

        initialized = false;

        for (int row = 0; row < 4; row++)
        {
            for (int column = 0; column < 4; column++)
            {
                covariance[row, column] =
                    row == column ? 1f : 0f;
            }
        }
    }

    public Vector2 Update(
        Vector2 measurement,
        float deltaTime)
    {
        if (!initialized)
        {
            state.x = measurement.x;
            state.y = measurement.y;

            state.z = 0f;
            state.w = 0f;

            initialized = true;

            return new Vector2(
                state.x,
                state.y
            );
        }

        Predict(deltaTime);

        UpdateMeasurement(
            measurement
        );

        return new Vector2(
            state.x,
            state.y
        );
    }

    private void Predict(float dt)
    {
        state.x +=
            state.z * dt;

        state.y +=
            state.w * dt;

        float[,] predicted =
            new float[4, 4];

        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                predicted[i, j] =
                    covariance[i, j];
            }
        }

        predicted[0, 0] +=
            dt * dt * processNoise;

        predicted[1, 1] +=
            dt * dt * processNoise;

        predicted[2, 2] +=
            processNoise;

        predicted[3, 3] +=
            processNoise;

        covariance = predicted;
    }

    private void UpdateMeasurement(
        Vector2 measurement)
    {
        float measurementVariance =
            measurementNoise;

        float innovationX =
            measurement.x - state.x;

        float innovationY =
            measurement.y - state.y;

        float gainX =
            covariance[0, 0] /
            (covariance[0, 0] +
             measurementVariance);

        float gainY =
            covariance[1, 1] /
            (covariance[1, 1] +
             measurementVariance);

        state.x +=
            gainX * innovationX;

        state.y +=
            gainY * innovationY;

        float velocityGainX =
            covariance[2, 0] /
            (covariance[0, 0] +
             measurementVariance);

        float velocityGainY =
            covariance[3, 1] /
            (covariance[1, 1] +
             measurementVariance);

        state.z +=
            velocityGainX *
            innovationX;

        state.w +=
            velocityGainY *
            innovationY;

        covariance[0, 0] *=
            1f - gainX;

        covariance[1, 1] *=
            1f - gainY;
    }

    public Vector2 GetPosition()
    {
        return new Vector2(
            state.x,
            state.y
        );
    }

    public Vector2 GetVelocity()
    {
        return new Vector2(
            state.z,
            state.w
        );

    }

    public bool IsInitialized()
    {
        return initialized;
    }
}