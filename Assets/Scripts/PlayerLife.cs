using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{

    private Animator anim;
    private Rigidbody2D myRigidBody2D;

    public bool died = false;

    [SerializeField] private AudioSource deathSoundEffect;

    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Die();
        }
    }

    public void Die()
    {
        deathSoundEffect.Play();
        myRigidBody2D.bodyType = RigidbodyType2D.Static;
        died = true;
        anim.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
