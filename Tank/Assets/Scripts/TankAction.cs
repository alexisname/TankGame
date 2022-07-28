using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

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

    // [SerializeField]
    // GameObject _enemyBulletPrefab;

    [SerializeField]
    GameObject _explosion;

    [SerializeField]
    GameObject _point;

    [SerializeField]
    int _numOfPoints;

    [SerializeField]
    float _spaceBetweenPoints;

    [SerializeField]
    float fireSpeed = 500.0f;

    [SerializeField]
    HealthBar healthBar;

    public bool hasFired;
    public int resetLimit = 2;
    public bool hasReset;
    
    
    GameObject[] _points;

        
    float _shotDelay;
    float _lastShotTime;
    float _lastResetTime;
    InputAction _fireInput;
    InputAction _steeringInput;
    InputAction _resetInput;
    Bullet EnemyBullet;
    

    public List<GameObject> firedBullets = new List<GameObject>();

    float _rotationSpeed = 0.1f;
    const float ROTATION_MIN = 0.0f;
    const float ROTATION_MAX = 25.0f;

    public int health = 100;
    public int numOfShot = 1;
    public int shots = 1;
    public bool isTurn = false;

    bool facingRight;

    float bulletgravity;
    
    InputAction _rotateInput;
    Vector2 direction;
    SpriteRenderer srPoints;
    //HealthBar healthbar;

    // Start is called before the first frame update
    void Start()
    {
        //healthbar = _health.GetComponent<HealthBar>();
        _steeringInput = _playerInput.actions["TankMove"];
        _rotateInput = _playerInput.actions["TurretMove"];
        _fireInput = _playerInput.actions["Fire"];
        _resetInput = _playerInput.actions["Reset"];

        _lastShotTime = 0.0f;
        _shotDelay = 0.5f;
        _points = new GameObject[_numOfPoints];

        // EnemyBullet = _enemyBulletPrefab.GetComponent<Bullet>();
        bulletgravity = _bulletPrefab.GetComponent<Rigidbody2D>().gravityScale;
        
        for(int i=0; i<_numOfPoints; i++){
            _points[i] = Instantiate(_point, _bulletEmit.position, Quaternion.identity);
        }
        if(gameObject.transform.localScale.x>0){
            facingRight = true;
        }
        else if(gameObject.transform.localScale.x<0){
            facingRight = false;
        }
        srPoints = _point.GetComponent<SpriteRenderer>();
        healthBar.setMaxHealth(health);
    }

    // Update is called once per frame
    void Update()
    {
        // int idxRemove = -1;
        // GameObject fb;
        // for(int i=0; i<firedBullets.Count; i++){
        //     fb = (GameObject)firedBullets[i];
        //     if(fb.GetComponent<Bullet>().hasLanded){                            
        //         Destroy(fb);
        //         idxRemove = i;
        //         break;  
        //     }
        // }
        // if(idxRemove>=0) {
        //     firedBullets.RemoveAt(idxRemove);
        // }
        foreach(GameObject fb in firedBullets){
            if(fb.GetComponent<Bullet>().hasLanded){
                Destroy(fb);
            }
        }
        firedBullets.RemoveAll(bulletLanded);
        bool bulletLanded(GameObject fb){
            return fb.GetComponent<Bullet>().hasLanded;
        }
        
        healthBar.SetHealth(health);
        float tankAngle = gameObject.transform.eulerAngles.z;
        
        if(health<=0){
                Instantiate(_explosion,gameObject.transform.position,Quaternion.identity);
                Destroy(gameObject);
                for(int i=0; i<_numOfPoints; i++){
                Destroy(_points[i]);
                }
            }

        if(!isTurn){
            for(int i=0; i<_numOfPoints; i++){
            _points[i].SetActive(false);
            }
            hasFired = false;
            hasReset = false;
            //Debug.Log("reset changed");
            numOfShot = shots;
        }
        
        if(isTurn){
            for(int i=0; i<_numOfPoints; i++){
            _points[i].SetActive(true);
            }
            //numOfShot = shots;
            // Debug.Log(tankAngle);
            Vector2 steering = _steeringInput.ReadValue<Vector2>();
            bool isUpDown;
            if(tankAngle>60f && tankAngle<300f){
                isUpDown = true;
            }
            else{
                isUpDown = false;
            }
            if(!isUpDown){
                Vector3 delta = _speed * steering * Time.deltaTime;
                transform.position = transform.position + delta;
            }
            
            

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


            if(steering.x>0 && !facingRight && !isUpDown){
                flip();
                _bulletEmit.rotation *= Quaternion.Euler(0,0,180);
                // if(_bulletEmit.localEulerAngles.z==0){
                //     _bulletEmit.rotation *= Quaternion.Euler(0,0,180); 
                // }
                // if(_bulletEmit.localEulerAngles.z==180){
                //     _bulletEmit.rotation *= Quaternion.Euler(0,0,-180); 
                // }       
                    
            }
            if(steering.x<0 && facingRight && !isUpDown){
                flip(); 
                _bulletEmit.rotation *= Quaternion.Euler(0,0,180);
                // if(_bulletEmit.localEulerAngles.z==0){
                //     _bulletEmit.rotation *= Quaternion.Euler(0,0,180); 
                // }
                // if(_bulletEmit.localEulerAngles.z==180){
                //     _bulletEmit.rotation *= Quaternion.Euler(0,0,-180); 
                // }            
            } 
            if(!hasReset && _resetInput.ReadValue<float>()==1.0f && resetLimit>0){                            
                resetTank();
                resetLimit--;
                hasReset = true;               
            }
        }                
    }

    public void rotate(){
        Vector3 currRotation = _turret.localEulerAngles;
        float rotation = _rotateInput.ReadValue<Vector2>().y*_rotationSpeed;
        if (currRotation.z > 180) currRotation.z -= 360f;

        currRotation.z = Mathf.Clamp(currRotation.z + rotation, ROTATION_MIN, ROTATION_MAX);
        _turret.localRotation = Quaternion.Euler(currRotation);

    }

    public void resetTank(){
        gameObject.transform.eulerAngles = new Vector3(0,0,0);
    }

    Vector2 pointPosition(float t){
        Vector2 p = _bulletEmit.position;               
        Vector2 pointPos = p + (direction.normalized*fireSpeed*t) + (0.5f*Physics2D.gravity*(t*t)*bulletgravity);
        return pointPos;
    }

    void fire(){
        if(numOfShot>0 && _fireInput.ReadValue<float>() == 1.0f){
            float timeDelta = Time.time - _lastShotTime;
            if(timeDelta>=_shotDelay){
                GameObject firedBullet = Instantiate(_bulletPrefab,_bulletEmit.position,_bulletEmit.rotation);
                firedBullets.Add(firedBullet);
                hasFired = true;
                firedBullet.GetComponent<Rigidbody2D>().AddForce(_bulletEmit.right*fireSpeed/* *mass */, ForceMode2D.Impulse);
                numOfShot--;
                //Debug.Log(numOfShot);
                _lastShotTime = Time.time;
            }            
        }
    }

    void flip(){
        Vector3 currScale = gameObject.transform.localScale;
        currScale.x *= -1;
        gameObject.transform.localScale = currScale;
        facingRight = !facingRight;
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

    // void OnTriggerEnter2D(Collider2D _bulletPrefab) {

        
    //     health -= 20;
        
    // // }
    // void OnCollisionEnter2D(Collision2D other){
    //     Bullet bullet = other.collider.GetComponent<Bullet>();
    //     if(bullet!=null){
    //         health -= bullet._damage;
    //     }                                 
    // }
    
}