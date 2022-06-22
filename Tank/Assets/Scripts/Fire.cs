using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Fire : MonoBehaviour
{
    [SerializeField]
    PlayerInput _playerInput = null;

    [SerializeField]
    Transform _turret;

    [SerializeField]
    GameObject _bulletPrefab;

    float fireSpeed = 500.0f;
    InputAction _fireInput;
    // Start is called before the first frame update
    void Start()
    {
        _fireInput = _playerInput.actions["Fire"];
    }

    // Update is called once per frame
    void Update()
    {
        fire();
    }
    void fire(){
        if( _fireInput.ReadValue<float>() == 1.0f ){
            GameObject firedBullet = Instantiate(_bulletPrefab,_turret.position,_turret.rotation);
            firedBullet.GetComponent<Rigidbody2D>().AddForce(_turret.right*fireSpeed);
        }
        
        
    }
}
