using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField] float thrust = 150f;
    [SerializeField] Rigidbody rb;
    [SerializeField] float wallDistance = 5f,minCamDistance = 3f; 
    private Vector2 lastMousePos;

    void Update()
    {
       Vector2 deltaPos = Vector2.zero;

       if (Input.GetMouseButton(0))
       {
         GameManager.singleton.StartGame();

         Vector2 currentMousePos = Input.mousePosition;

         if(lastMousePos == Vector2.zero)
          lastMousePos=currentMousePos;
        
         deltaPos = currentMousePos - lastMousePos;
         lastMousePos=currentMousePos;
         Vector3 force = new Vector3(deltaPos.x,0,deltaPos.y) * thrust;
         rb.AddForce(force*Time.deltaTime);
       } 
       else
       {
        lastMousePos= Vector2.zero;
       }
    }

    void FixedUpdate() 
    {
      if(GameManager.singleton.GameEnded)
      {
        return;
      }

      if(GameManager.singleton.GameStarted)
      {
        rb.MovePosition(transform.position + Vector3.forward*5*Time.fixedDeltaTime);
      }   
    }

    void LateUpdate() 
    {
      Vector3 pos = transform.position;
      if(transform.position.x<-wallDistance)
      {
       pos.x = -wallDistance;
      } 
      else if(transform.position.x>wallDistance)
      { 
        pos.x = wallDistance;
      }
      
      if(transform.position.z < Camera.main.transform.position.z + minCamDistance)
      {
        pos.z = Camera.main.transform.position.z + minCamDistance;
      }

      transform.position = pos; 
    }

    void OnCollisionEnter(Collision other) 
    {
       if(GameManager.singleton.GameEnded)
       {
         return;
       }
       if(other.gameObject.tag== "Death")
       {
          GameManager.singleton.EndGame(false);
       }  
    }
}
