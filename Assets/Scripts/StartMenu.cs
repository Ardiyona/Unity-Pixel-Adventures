using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private CanvasGroup titleText;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject quitButton;
    [SerializeField] private Button quitButtons;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject images;

    [SerializeField] private CanvasGroup creditText;
    [SerializeField] private GameObject creditTexts;
    [SerializeField] private CanvasGroup gameAssetsText;
    [SerializeField] private GameObject gameAssetsTexts;
    [SerializeField] private CanvasGroup nameText;
    [SerializeField] private GameObject nameTexts;
    [SerializeField] private GameObject creditBack;

    [SerializeField] private CanvasGroup levelsSelectText;
    [SerializeField] private GameObject levelsSelectTexts;
    [SerializeField] private GameObject backButton;
    [SerializeField] private GameObject levelsButton;

    [SerializeField] private Button level1;
    [SerializeField] private Button level2;
    [SerializeField] private Button level3;

    private bool changing = false;

    public void LevelsSelect()
    {
        StartCoroutine(LevelsSelectCoroutine());
    }

    private IEnumerator LevelsSelectCoroutine ()
    {
        if (!changing)
        {
            changing = true;
            level1.interactable = false;
            level2.interactable = false;
            level3.interactable = false;
            titleText.LeanAlpha(0, 1f);
            startButton.LeanMoveLocal(new Vector2(0, -410), 1f).setEaseInQuart();
            quitButton.LeanMoveLocal(new Vector2(0, -410), 1f).setEaseInQuart();
            quitButtons.interactable = false;
            credits.LeanMoveLocal(new Vector2(727, -324), 1f).setEaseInQuart();
            images.LeanMoveLocal(new Vector2(0, -280), 1f).setEaseInQuart();
            yield return new WaitForSeconds(1f);
            levelsSelectTexts.SetActive(true);
            levelsSelectText.alpha = 0;
            levelsSelectText.LeanAlpha(1, 1f);
            backButton.LeanMoveLocal(new Vector2(-480, 256), 1f).setEaseOutQuart();
            levelsButton.LeanMoveLocal(new Vector2(0, 0), 1f).setEaseOutQuart();
            yield return new WaitForSeconds(.5f);
            level1.interactable = true;
            level2.interactable = true;
            level3.interactable = true;
            changing = false;
            Debug.Log("Changing: " + changing);
        }
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCoroutine());
    }

    private IEnumerator StartGameCoroutine()
    {
        if (!changing)
        {
            changing = true;
            level1.interactable = false;
            level2.interactable = false;
            level3.interactable = false;
            levelsSelectText.LeanAlpha(0, 1f);
            backButton.LeanMoveLocal(new Vector2(-746, 256), 1f).setEaseInQuart();
            levelsButton.LeanMoveLocal(new Vector2(0, -389), 1f).setEaseInQuart();
            yield return new WaitForSeconds(1f);
            levelsSelectTexts.SetActive(false);
            titleText.LeanAlpha(1, 1f);
            startButton.LeanMoveLocal(new Vector2(0, 54), 1f).setEaseOutQuart();
            quitButton.LeanMoveLocal(new Vector2(0, -58), 1f).setEaseOutQuart();
            quitButtons.interactable = true;
            credits.LeanMoveLocal(new Vector2(551, -324), 1f).setEaseOutQuart();
            images.LeanMoveLocal(new Vector2(0, -4), 1f).setEaseOutQuart();
            yield return new WaitForSeconds(.5f);
            level1.interactable = true;
            level2.interactable = true;
            level3.interactable = true;
            changing = false;
        }
    }

    public void Credits()
    {
        StartCoroutine(CreditsCoroutine());
    }

    private IEnumerator CreditsCoroutine()
    {
        if (!changing)
        {
            changing = true;
            titleText.LeanAlpha(0, 1f);
            startButton.LeanMoveLocal(new Vector2(0, -410), 1f).setEaseInQuart();
            quitButton.LeanMoveLocal(new Vector2(0, -410), 1f).setEaseInQuart();
            credits.LeanMoveLocal(new Vector2(727, -324), 1f).setEaseInQuart();
            quitButtons.interactable = false;
            images.LeanMoveLocal(new Vector2(0, -280), 1f).setEaseInQuart();
            yield return new WaitForSeconds(1f);
            creditTexts.SetActive(true);
            creditText.LeanAlpha(1, 1f);
            gameAssetsTexts.SetActive(true);
            gameAssetsText.LeanAlpha(1, 1f);
            nameTexts.SetActive(true);
            nameText.LeanAlpha(1, 1f);
            creditBack.LeanMoveLocal(new Vector2(-480, 256), 1f).setEaseOutQuart();
            yield return new WaitForSeconds(.5f);
            changing = false;
        }
    }

    public void CreditBack()
    {
        StartCoroutine(CreditBackCoroutine());
    }

    private IEnumerator CreditBackCoroutine()
    {
        if (!changing)
        {
            changing = true;
            creditText.LeanAlpha(0, 1f);
            gameAssetsText.LeanAlpha(0, 1f);
            nameText.LeanAlpha(0, 1f);
            creditBack.LeanMoveLocal(new Vector2(-746, 256), 1f).setEaseInQuart();
            yield return new WaitForSeconds(1f);
            nameTexts.SetActive(false);
            gameAssetsTexts.SetActive(false);
            creditTexts.SetActive(false);
            titleText.LeanAlpha(1, 1f);
            startButton.LeanMoveLocal(new Vector2(0, 54), 1f).setEaseOutQuart();
            quitButton.LeanMoveLocal(new Vector2(0, -58), 1f).setEaseOutQuart();
            quitButtons.interactable = true;
            credits.LeanMoveLocal(new Vector2(551, -324), 1f).setEaseOutQuart();
            images.LeanMoveLocal(new Vector2(0, -4), 1f).setEaseOutQuart();
            yield return new WaitForSeconds(.5f);
            changing = false;
        }
    }
}