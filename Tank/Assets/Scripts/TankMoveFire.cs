using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TankMoveFire : MonoBehaviour
{
    [SerializeField]
    PlayerInput _playerInput = null;

    [SerializeField]
    float _speed = 5.0f;    

    [SerializeField]
    Transform _turret;

    [SerializeField]
    GameObject _bulletPrefab;  

    [SerializeField]
    Button _turnButton;    

    float fireSpeed = 500.0f;    
    float _shotDelay;
    float _lastShotTime;
    InputAction _fireInput;
    InputAction _steeringInput;
    public static bool _playerTurn;

    // Start is called before the first frame update
    void Start()
    {
        _steeringInput = _playerInput.actions["TankMove"];
        _fireInput = _playerInput.actions["Fire"];
        _lastShotTime = 0.0f;
        _shotDelay = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 steering = _steeringInput.ReadValue<Vector2>();
		Vector3 delta = _speed * steering * Time.deltaTime;
		transform.position = transform.position + delta;

        fire();
        
    }

    void fire(){
        if( _fireInput.ReadValue<float>() == 1.0f ){
            float timeDelta = Time.time - _lastShotTime;
            if(timeDelta>=_shotDelay){
                GameObject firedBullet = Instantiate(_bulletPrefab,_turret.position,_turret.rotation);
                firedBullet.GetComponent<Rigidbody2D>().AddForce(_turret.right*fireSpeed);
                _lastShotTime = Time.time;
            }            
        }
    }

    void _button(){
        changeTurn();
    }

    void changeTurn(){
        if(_playerTurn){
            _playerTurn = false;
        }
        else{
            _playerTurn = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }
    
}
