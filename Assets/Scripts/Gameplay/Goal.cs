using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    
    public bool onGoal = false;
    public SceneController sceneController;

    public void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log(other.transform.name);
        onGoal = true;
        sceneController.CheckGoals();
    }

    public void OnTriggerExit2D(Collider2D other) {
        onGoal = false;
    }
}
