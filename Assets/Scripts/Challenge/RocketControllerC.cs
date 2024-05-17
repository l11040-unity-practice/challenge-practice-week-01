using UnityEngine;
using UnityEngine.InputSystem;

public class RocketControllerC : MonoBehaviour
{
    private EnergySystemC _energySystem;
    private RocketMovementC _rocketMovement;

    private bool _isMoving;
    private float _movementDirection;

    private readonly float ENERGY_TURN = 0.5f;
    private readonly float ENERGY_BURST = 2f;

    private void Awake()
    {
        _energySystem = GetComponent<EnergySystemC>();
        _rocketMovement = GetComponent<RocketMovementC>();
    }

    private void FixedUpdate()
    {
        if (!_isMoving) return;

        if (!_energySystem.UseEnergy(Time.fixedDeltaTime * ENERGY_TURN)) return;

        _rocketMovement.ApplyMovement(_movementDirection);
    }

    // OnMove 구현
    // private void OnMove...
    private void OnMove(InputValue value)
    {
        Vector2 direction = value.Get<Vector2>();
        if (direction != null && direction.magnitude > 0)
        {
            _movementDirection = direction.x;
            _isMoving = true;
        }
        else
        {
            _isMoving = false;
        }
    }


    // OnBoost 구현
    // private void OnBoost...
    private void OnBoost(InputValue value)
    {
        if (_energySystem.UseEnergy(ENERGY_BURST))
        {
            _rocketMovement.ApplyBoost();
        }
    }
}