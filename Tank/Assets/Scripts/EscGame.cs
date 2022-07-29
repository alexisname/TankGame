using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EscGame : MonoBehaviour
{
    [SerializeField]
    PlayerInput _escInput = null;
    
    InputAction escape;
    // Start is called before the first frame update
    void Start()
    {
        escape = _escInput.actions["Escape"];
    }

    // Update is called once per frame
    void Update()
    {
        if(escape.ReadValue<float>()==1.0f){
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex-1);
        }
    }
}
