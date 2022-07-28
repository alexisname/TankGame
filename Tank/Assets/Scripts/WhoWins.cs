using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class WhoWins : MonoBehaviour
{
    
    public TextMeshProUGUI winner;
    public static int winnerIdx;


    // Start is called before the first frame update
    void Start()
    {   
        winnerIdx = BattleManager.winnerIdx;
        winner = GetComponent<TextMeshProUGUI>();

    }
    void Update(){
        // winner.SetText($"yes");
            if(winnerIdx==1){
                winner.SetText($"Player One WON");
            }
            if(winnerIdx==2){
                winner.SetText($"Player Two WON");
            }
    }

    // Update is called once per frame

}
