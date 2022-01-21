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
            StartCoroutine(FadeOut(robot.spriteRenderer));
            StartCoroutine(FadeOut(beast.spriteRenderer));
            StartCoroutine(TeleportEffect());
            StartCoroutine(NextScene());
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

    IEnumerator TeleportEffect(){
        yield return new WaitForSeconds(0.5f);
        robot.goalTeleportEffect.Play();
        beast.goalTeleportEffect.Play();
    }

    IEnumerator NextScene(){
        yield return new WaitForSeconds(4f);
        SceneLoader.LoadScene(nextSceneIndex);
    }


}
