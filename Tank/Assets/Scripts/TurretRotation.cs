using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretRotation : MonoBehaviour
{
    [SerializeField]
    PlayerInput _playerInput = null;

    float _rotationSpeed = 0.1f;
    private const float ROTATION_MIN = -15.0f;
    private const float ROTATION_MAX = 15.0f;
    InputAction _rotateInput;
    // Start is called before the first frame update
    void Start()
    {
        _rotateInput = _playerInput.actions["TurretMove"];
    }

    // Update is called once per frame
    void Update()
    {
        if(_rotateInput!=null){
            rotate();
        }
    }

    public void rotate(){
        Vector3 currRotation = transform.localEulerAngles;
        float rotation = _rotateInput.ReadValue<Vector2>().y*_rotationSpeed;
        if (currRotation.z > 180) currRotation.z -= 360f;

        currRotation.z = Mathf.Clamp(currRotation.z + rotation, ROTATION_MIN, ROTATION_MAX);
        transform.localRotation = Quaternion.Euler(currRotation);

    }
}
