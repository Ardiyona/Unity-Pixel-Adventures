using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTransition : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject dPanel;
    [SerializeField] private GameObject panelButton;
    [SerializeField] private GameObject levelsPanel;
    [SerializeField] private GameObject levelsPanelButton;
    [SerializeField] private GameObject dLevelsPanel;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void StartGameTransition()
    {
        StartCoroutine(StartGame());
    }

    public void LevelsSelectTransition()
    {
        StartCoroutine(LevelsSelect());
    }

    public IEnumerator LevelsSelect()
    {
        panel.SetActive(false);
        panelButton.SetActive(true);
        anim.SetTrigger("SMStart");
        yield return new WaitForSeconds(1f);
        dPanel.SetActive(false);
        if (!dPanel.activeSelf)
        {
            dLevelsPanel.SetActive(true);
            anim.SetTrigger("LStart");
            yield return new WaitForSeconds(1f);
            if (!levelsPanel.activeSelf)
            {
                levelsPanel.SetActive(true);
            }
        }
    }

    public IEnumerator StartGame()
    {
        levelsPanel.SetActive(false);
        levelsPanelButton.SetActive(true);
        anim.SetTrigger("LStart");
        yield return new WaitForSeconds(1f);
        dLevelsPanel.SetActive(false);
        dPanel.SetActive(true);
        anim.SetTrigger("SMEnd");
        if (!panel.activeSelf)
        {
            panel.SetActive(true);
        }
    }
}
