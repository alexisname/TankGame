using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class TankAction : MonoBehaviour
{
    [SerializeField]
    PlayerInput _playerInput = null;

    [SerializeField]
    float _speed = 3.0f;    

    [SerializeField]
    Transform _turret;

    [SerializeField]
    Transform _bulletEmit;

    [SerializeField]
    Transform _dirBullet;

    [SerializeField]
    GameObject _bulletPrefab;  

    [SerializeField]
    GameObject _point;


    [SerializeField]
    int _numOfPoints;

    [SerializeField]
    float _spaceBetweenPoints;

    // [SerializeField]
    // Button _turnButton;    

    
    GameObject[] _points;

    float fireSpeed = 500.0f;    
    float _shotDelay;
    float _lastShotTime;
    InputAction _fireInput;
    InputAction _steeringInput;
    public static bool _playerTurn;

    float _rotationSpeed = 0.1f;
    private const float ROTATION_MIN = 0.0f;
    private const float ROTATION_MAX = 25.0f;
    InputAction _rotateInput;
    Vector2 direction;

    // Start is called before the first frame update
    void Start()
    {
        _steeringInput = _playerInput.actions["TankMove"];
        _rotateInput = _playerInput.actions["TurretMove"];
        _fireInput = _playerInput.actions["Fire"];
        _lastShotTime = 0.0f;
        _shotDelay = 0.5f;
        _points = new GameObject[_numOfPoints];
        for(int i=0; i<_numOfPoints; i++){
            _points[i] = Instantiate(_point, _bulletEmit.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 steering = _steeringInput.ReadValue<Vector2>();
		Vector3 delta = _speed * steering * Time.deltaTime;
		transform.position = transform.position + delta;

        Vector2 firePos = _bulletEmit.position;
        Vector2 dirPos = _dirBullet.position;
        direction = dirPos-firePos;

        if(_rotateInput!=null){
            rotate();
        }

        for(int i=0; i<_numOfPoints; i++){
            _points[i].transform.position = pointPosition(i*_spaceBetweenPoints);
        }

        fire();
        
    }

    public void rotate(){
        Vector3 currRotation = _turret.localEulerAngles;
        float rotation = _rotateInput.ReadValue<Vector2>().y*_rotationSpeed;
        if (currRotation.z > 180) currRotation.z -= 360f;

        currRotation.z = Mathf.Clamp(currRotation.z + rotation, ROTATION_MIN, ROTATION_MAX);
        _turret.localRotation = Quaternion.Euler(currRotation);

    }

    Vector2 pointPosition(float t){               
        Vector2 pointPos = (Vector2)_bulletEmit.position + (direction.normalized*_speed*t) + (0.5f*new Vector2(0,-0.23f)*(t*t));
        return pointPos;
    }

    void fire(){
        if( _fireInput.ReadValue<float>() == 1.0f ){
            float timeDelta = Time.time - _lastShotTime;
            if(timeDelta>=_shotDelay){
                GameObject firedBullet = Instantiate(_bulletPrefab,_bulletEmit.position,_bulletEmit.rotation);
                firedBullet.GetComponent<Rigidbody2D>().AddForce(_bulletEmit.right*fireSpeed);
                _lastShotTime = Time.time;
            }            
        }
    }
    

    // void _button(){
    //     changeTurn();
    // }

    // void changeTurn(){
    //     if(_playerTurn){
    //         _playerTurn = false;
    //     }
    //     else{
    //         _playerTurn = true;
    //     }
    // }

    void OnTriggerEnter2D(Collider2D other) {
        Destroy(gameObject);
    }
    
}
