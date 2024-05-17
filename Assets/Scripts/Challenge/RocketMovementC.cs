using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class RocketMovementC : MonoBehaviour
{
    private Rigidbody2D _rb2d;
    private readonly float SPEED = 10f;
    private readonly float ROTATIONSPEED = 0.02f;

    private float highScore = -1;

    public static Action<float> OnHighScoreChanged;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!(highScore < transform.position.y)) return;
        highScore = transform.position.y;
        OnHighScoreChanged?.Invoke(highScore);
    }

    public void ApplyMovement(float inputX)
    {
        Rotate(inputX);
    }

    public void ApplyBoost()
    {
        _rb2d.AddForce(transform.up * SPEED, ForceMode2D.Impulse);
    }

    private void Rotate(float inputX)
    {
        float rotationZ;

        if (inputX < 0)
        {
            rotationZ = 90f;
        }
        else if (inputX > 0)
        {
            rotationZ = -90f;
        }
        else
        {
            return;
        }

        Quaternion targetRotation = Quaternion.Euler(0, 0, rotationZ);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.1f);
    }
}