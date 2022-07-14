using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    GameObject _explosion;

    [SerializeField]
    float _radius = 3.0f;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        trackMovement();
    }

    void trackMovement(){
        Vector2 direction  = rb.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }
    void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Terrain")){
            Terrain.instance.DestroyTerrain(gameObject.transform.position, _radius);
            // Instantiate(_explosion,gameObject.transform.position,Quaternion.identity);
        }
        Instantiate(_explosion,gameObject.transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
