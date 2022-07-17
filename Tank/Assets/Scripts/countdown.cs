using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class countdown : MonoBehaviour
{
    public GameObject textDisplay;
    public int secondsLeft = 5;
    public bool taking = false;
    // Start is called before the first frame update
    void Start()
    {
        textDisplay.GetComponent<Text>().text = "00:"+secondsLeft;
    }

    // Update is called once per frame
    void Update()
    {
        if(!taking && secondsLeft>0){
            StartCoroutine(timeTake());
        }
        // Debug.Log(secondsLeft);
    }

    IEnumerator timeTake(){
        taking = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        textDisplay.GetComponent<Text>().text = "00:"+secondsLeft;
        taking = false;
    }
}
