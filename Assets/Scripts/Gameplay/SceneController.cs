using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    
    public Robot robot;
    public Goal robotGoal;
    public Beast beast;
    public Goal beastGoal;
    
    public int nextSceneIndex;

    public void CheckGoals(){
        if(robotGoal.onGoal && beastGoal.onGoal){
            robot.levelCleared = true;
            beast.levelCleared = true;
            StartCoroutine(FadeOut(robot.spriteRenderer));
            StartCoroutine(FadeOut(beast.spriteRenderer));
            // SceneLoader.LoadScene(nextSceneIndex);
        }
    }

    IEnumerator FadeOut(SpriteRenderer mySprite){
        Color tmpColor = mySprite.color;

        while(tmpColor.a >= 0f){
            tmpColor.a -= Time.deltaTime / 2f;
            mySprite.color = tmpColor;

            if(tmpColor.a <= 0f){
                tmpColor.a =0f;
            }

            yield return null;
        }

        mySprite.color = tmpColor;
    }


}
