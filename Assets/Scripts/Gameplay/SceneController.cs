using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public Goal robotGoal;
    public Goal beastGoal;
    public int nextSceneIndex;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void CheckGoals(){
        if(robotGoal.onGoal && beastGoal.onGoal){
            SceneLoader.LoadScene(nextSceneIndex);
        }
    }
}
