using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) 
    {
      BallMovement ball = other.GetComponent<BallMovement>();
      if(!ball || GameManager.singleton.GameEnded)
      {
        return;
      }

      Debug.Log("Goal");
      GameManager.singleton.EndGame(true);    
    }
}
