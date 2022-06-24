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
    float _shotDelay;
    float _lastShotTime;
    InputAction _fireInput;
    // Start is called before the first frame update
    void Start()
    {
        _fireInput = _playerInput.actions["Fire"];
        _lastShotTime = 0.0f;
        _shotDelay = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if( _fireInput.ReadValue<float>() == 1.0f ){
            float timeDelta = Time.time - _lastShotTime;
            if(timeDelta>=_shotDelay){
                GameObject firedBullet = Instantiate(_bulletPrefab,_turret.position,_turret.rotation);
                firedBullet.GetComponent<Rigidbody2D>().AddForce(_turret.right*fireSpeed);
                _lastShotTime = Time.time;
            }            
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }
    
}
   

