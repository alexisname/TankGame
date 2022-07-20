using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPlayers : MonoBehaviour
{
    [SerializeField]
    GameObject _tankPrefabOne;
    
    [SerializeField]
    GameObject _tankPrefabTwo;

    [SerializeField]
    Transform _locationOne;

    [SerializeField]
    Transform _locationTwo;

    [SerializeField]
    GameObject countDown;

    [SerializeField]
    GameObject _bulletOne;

    [SerializeField]
    GameObject _bulletTwo;

    TankAction playerOneAction;
    TankAction playerTwoAction;
    
    countdown countdown;
    // Bullet bulletOneEx;
    // Bullet bulletTwoEx;
 
    public float turnTime = 5.0f;
    float lastTurnTime = 0f;
    
    void Start()
    {
        GameObject playerOne = Instantiate(_tankPrefabOne,_locationOne.position,Quaternion.identity);
        playerOneAction = playerOne.GetComponent<TankAction>();
        GameObject playerTwo = Instantiate(_tankPrefabTwo,_locationTwo.position,Quaternion.identity);
        playerTwoAction = playerTwo.GetComponent<TankAction>();
        playerOneAction.isTurn = true;
        playerTwoAction.isTurn = false;
        countdown = countDown.GetComponent<countdown>();        
    }

    // Update is called once per frame   
    void Update() {
        // bulletOneEx = _bulletOne.GetComponent<Bullet>();
        // bulletTwoEx = _bulletTwo.GetComponent<Bullet>();
        // Debug.Log(bulletOneEx.land);
        // Debug.Log(bulletTwoEx.land);
        float timeDelta = Time.time - lastTurnTime;
        if(timeDelta>=turnTime || (playerOneAction.hasFired && playerOneAction.firedBullet==null) || (playerTwoAction.hasFired && playerTwoAction.firedBullet==null)){
            changeTurn();
            // bulletOneEx.land = false;
            // bulletTwoEx.land = false;
            lastTurnTime = Time.time;
        }
    }
    void changeTurn(){
        countdown.secondsLeft = 10;
        if(playerOneAction.isTurn){
            playerOneAction.isTurn = false;
            playerTwoAction.isTurn = true;
        }
        else{
            playerOneAction.isTurn = true;
            playerTwoAction.isTurn = false;
        }
    }
}
