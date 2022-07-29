using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ResetNumOne : MonoBehaviour
{
    
    TextMeshProUGUI reset;
    int resetLimit;


    // Start is called before the first frame update
    void Start()
    {   
       
        reset = GetComponent<TextMeshProUGUI>();

    }
    void Update(){
        resetLimit = BattleManager.resetLimitOne;
        reset.SetText($"Player One Reset Left: "+resetLimit);
    }
}