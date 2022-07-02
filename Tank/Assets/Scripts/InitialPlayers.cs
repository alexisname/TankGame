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
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_tankPrefabOne,_locationOne.position,Quaternion.identity);
        Instantiate(_tankPrefabTwo,_locationTwo.position,Quaternion.identity);
    }

    // Update is called once per frame   
}
