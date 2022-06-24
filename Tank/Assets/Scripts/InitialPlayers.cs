using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPlayers : MonoBehaviour
{
    [SerializeField]
    GameObject _tankPrefabOne;
    
    [SerializeField]
    GameObject _tankPrefabTwo;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_tankPrefabOne,new Vector3(-5,0,0),Quaternion.identity);
        Instantiate(_tankPrefabTwo,new Vector3(1,0,0),Quaternion.identity);
    }

    // Update is called once per frame   
}
