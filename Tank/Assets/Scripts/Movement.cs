using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField]
    PlayerInput _playerInput = null;
    [SerializeField]
    float _speed = 5.0f;

    InputAction _steeringInput;
    // Start is called before the first frame update
    void Start()
    {
        _steeringInput = _playerInput.actions["TankMove"];
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 steering = _steeringInput.ReadValue<Vector2>();
		Vector3 delta = _speed * steering * Time.deltaTime;
		transform.position = transform.position + delta;
    }
}
