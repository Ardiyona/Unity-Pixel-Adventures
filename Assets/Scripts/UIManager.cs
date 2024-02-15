using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Animator startMenuAnimator;
    public Animator levelsMenuAnimator;
    public float transitionDuration = 1f;  // Durasi transisi, bisa disesuaikan

    public void TransitionToLevelsMenu()
    {
        StartCoroutine(TransitionCoroutine(startMenuAnimator, levelsMenuAnimator));
    }

    public void TransitionToStartMenu()
    {
        StartCoroutine(TransitionCoroutine(levelsMenuAnimator, startMenuAnimator));
    }

    private System.Collections.IEnumerator TransitionCoroutine(Animator fromAnimator, Animator toAnimator)
    {
        fromAnimator.SetTrigger("End");
        yield return new WaitForSeconds(transitionDuration);
        fromAnimator.gameObject.SetActive(false);

        toAnimator.gameObject.SetActive(true);
        toAnimator.SetTrigger("Start");
    }
}
