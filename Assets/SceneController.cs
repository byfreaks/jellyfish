using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    private IEnumerator coroutine;
    public Image fadeToBlack;
    public List<Image> buttons;
    public GameObject credits;

    private bool titleShown = false;

    public void TitleShowCredits(){
        if(titleShown){
            credits.GetComponent<Animator>().Play("Hide");
            titleShown = false;
        }
        else
        {
            credits.GetComponent<Animator>().Play("Show");
            titleShown = true;
        }
    }

    public void TitleChangeTo(int sceneId){
        FadeToBlackAndSwap(sceneId);
    }

    private void FadeToBlackAndSwap(int id){
        coroutine = FeedbackEffect(id);
        StartCoroutine( coroutine );
    }

    private IEnumerator FeedbackEffect(int id){
        var alpha = 0f;
        var duration = 1.5f;

        for (float i = 0; i < duration; i += Time.deltaTime)
        {
            alpha = i / duration;
            Debug.Log(alpha);
            fadeToBlack.color = new Color(fadeToBlack.color.r, fadeToBlack.color.g, fadeToBlack.color.b, alpha);
            buttons.ForEach( b => b.color = new Color(b.color.r, b.color.g, b.color.b, 1 - alpha));
            yield return null;
        }
        SceneManager.LoadScene(id);

        StopCoroutine(coroutine);
    }
}
