using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class ResetNumTwo : MonoBehaviour
{
    // Start is called before the first frame update    TextMeshProUGUI reset;
    TextMeshProUGUI reset;
    int resetLimit;


    // Start is called before the first frame update
    void Start()
    {   
        
        reset = GetComponent<TextMeshProUGUI>();

    }
    void Update(){
        resetLimit = BattleManager.resetLimitTwo;
        reset.SetText($"Player One Reset Left: "+resetLimit);
    }
}
