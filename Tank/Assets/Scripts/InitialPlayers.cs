using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialPlayers : MonoBehaviour
{
    [SerializeField]
    GameObject _tankPrefab;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_tankPrefab,new Vector3(1,0,0),Quaternion.identity);
    }

    // Update is called once per frame   
}
