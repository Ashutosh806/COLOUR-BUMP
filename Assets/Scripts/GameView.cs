using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameView : MonoBehaviour
{
    [SerializeField] Image fillBarProgress;
    private float lastValue;
    
    void Update()
    {
       if(!GameManager.singleton.GameStarted)
       {
        return;
       } 
       float travelledDistance = GameManager.singleton.EntireDistance - GameManager.singleton.DistanceLeft;
       float value = travelledDistance/GameManager.singleton.EntireDistance;

       if(GameManager.singleton.gameObject && value < lastValue)
       {
        return;
       }

        fillBarProgress.fillAmount = Mathf.Lerp(fillBarProgress.fillAmount,value,5*Time.deltaTime);
        lastValue = value;
    }
}
