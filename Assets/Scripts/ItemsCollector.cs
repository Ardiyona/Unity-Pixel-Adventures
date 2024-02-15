using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsCollector : MonoBehaviour
{
    [SerializeField] private AudioSource itemCollectedSound;

    private Animator anim;

    private bool collected = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !collected)
        {
            collected = true;
            itemCollectedSound.Play();
            anim.SetTrigger("collected");
            FindObjectOfType<GameManager>().CollectItem();
            Invoke("DestroyItem",.3f);
        }
    }

    private void DestroyItem()
    {
        Destroy(gameObject);
        collected = false;
    }
}
