using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDestroy : MonoBehaviour
{
    [SerializeField]
    float _delayTime;
    // Start is called before the first frame update
    void FixedUpdate() {
        Destroy(gameObject,_delayTime);
    }
}
