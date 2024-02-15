using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelTransition : MonoBehaviour
{

    private Animator anim;

    [SerializeField] GameObject panel;
    [SerializeField] GameObject panelAnimation;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        PanelAnimation();
    }

    private void PanelAnimation()
    {
        if (panel.activeSelf == false)
        {
            anim.SetTrigger("Start");
        } else if (panel.activeSelf == true)
        {
            anim.SetTrigger("End");
        }
    }

    IEnumerator PanelActivation()
    {
        yield return new WaitForSeconds(1f);
        panel.SetActive(true);
    }
}
