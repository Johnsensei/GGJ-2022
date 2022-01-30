using System.Collections;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    
    public Robot robot;
    public Goal robotGoal;
    public GameObject robotGoalTeleporter;
    public Beast beast;
    public Goal beastGoal;
    public GameObject beastGoalTeleporter;

    public int nextSceneIndex;

    public void CheckGoals(){
        if(robotGoal.onGoal && beastGoal.onGoal){
            // Debug.Log("Robot: " + robotGoal.onGoal + " Beast: " + beastGoal.onGoal);
            StartCoroutine(FadeOut(robot.spriteRenderer));
            StartCoroutine(FadeOut(beast.spriteRenderer));
            // robot.GetComponent<IFader>().Fade(false);
            // beast.GetComponent<IFader>().Fade(false);
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
        robotGoalTeleporter.SetActive(true);
        beastGoalTeleporter.SetActive(true);
    }

    IEnumerator NextScene(){
        yield return new WaitForSeconds(4f);
        SceneLoader.LoadScene(nextSceneIndex);
    }


}
