using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    GameObject _explosion;

    [SerializeField]
    float _radius = 3.0f;
    public int _damage = 20;
    public bool hasLanded = false;
    

    //public bool  landFlag;
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
        //Debug.Log("update: "+landFlag);
    }

    void trackMovement(){
        Vector2 direction  = rb.velocity;
        float angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle,Vector3.forward);
    }
    // void OnTriggerEnter2D(Collider2D other) {
    //     landFlag = true;
    //     Debug.Log("trigger: "+landFlag);
    //     if(other.CompareTag("Terrain")){
    //         Terrain.instance.DestroyTerrain(gameObject.transform.position, _radius);
    //         // Instantiate(_explosion,gameObject.transform.position,Quaternion.identity);
    //     }
    //     Instantiate(_explosion,gameObject.transform.position,Quaternion.identity);
    //     Destroy(gameObject);
    // }

    void OnCollisionEnter2D(Collision2D other) {
        if(other.collider.tag == "Terrain"){

            Terrain.instance.DestroyTerrain(other.contacts[0].point, _radius);
            // Instantiate(_explosion,gameObject.transform.position,Quaternion.identity);
        }
        Debug.Log("before action");
        if(other.collider.attachedRigidbody!=null){
            TankAction tankAction = other.collider.attachedRigidbody.GetComponent<TankAction>();
            if(tankAction!=null){
                tankAction.health -= _damage;
            }
        }
        
        Debug.Log("after action");
        Instantiate(_explosion,gameObject.transform.position,Quaternion.identity);
        hasLanded = true;
        Debug.Log("after landed");
    }
}
