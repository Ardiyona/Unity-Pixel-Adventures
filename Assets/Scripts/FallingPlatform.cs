using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private float fallDelay = 1f;
    private float destroyDelay = 1.5f;

    [SerializeField] private Rigidbody2D rb;

    private Animator anim;

    private PlayerMovement PM;

    private void Start()
    {
        anim = GetComponent<Animator>();
        PM = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && PM.IsGrounded())
        {
            collision.gameObject.transform.SetParent(transform);
            StartCoroutine(Fall());
        }
    }

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        anim.SetTrigger("Fall");
        rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
