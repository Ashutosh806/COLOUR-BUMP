using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager singleton;
    public bool GameStarted{get;private set;}
    public bool GameEnded{get;private set;}
    [SerializeField] float slowMotionFactor = 0.1f;
    [SerializeField] Transform startTransform;
    [SerializeField] Transform goalTransform;
    [SerializeField] BallMovement ball;
    
    public float EntireDistance {get; private set;}
    public float DistanceLeft {get; set;}

    private void Start() 
    {
      EntireDistance = goalTransform.position.z -startTransform.position.z;  
    }

    private void Awake() 
    {
      if(singleton == null)
      {
        singleton = this;
      }  
      else if(singleton!=this)
      {
        Destroy(gameObject);
      } 

      Time.timeScale = 1f;
      Time.fixedDeltaTime = 0.02f; 
    }
    
    public void StartGame()
    {
      GameStarted = true;
    }

    public void EndGame(bool win)
    {
      GameEnded = true;
      Debug.Log("Game Ended");

      if(!win)
      {
        Invoke("RestartGame",2*slowMotionFactor);
        Time.timeScale = slowMotionFactor;
        Time.fixedDeltaTime = 0.02f*Time.timeScale;
      }
      else
      {
        Invoke("LoadNextLevel",2);
      }
    }

    public void LoadNextLevel()
    {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      int nextSceneIndex = currentSceneIndex + 1;
      if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
      {
        nextSceneIndex = 0; 
      }
      SceneManager.LoadScene(nextSceneIndex);
    }

    public void RestartGame()
    {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void Update()
    {
      DistanceLeft = Vector3.Distance(ball.transform.position,goalTransform.position);

      if(DistanceLeft > EntireDistance)
       DistanceLeft = EntireDistance;
      if(ball.transform.position.z > goalTransform.position.z)
       DistanceLeft = 0;

      Debug.Log("Travelled Distance " + DistanceLeft + "Entire Distance " + EntireDistance); 
    }
}
