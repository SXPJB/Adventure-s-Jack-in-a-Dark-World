using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoController : MonoBehaviour
{

    public float velocidad = -1f;
    public Rigidbody2D rigidbody;
    public Animator animator;
    public float vida;
    float costoBala = 20;
    private AudioSource audio;
    public AudioClip golpe;
    public AudioClip muerte;

    // Start is called before the first frame update
    void Start()
    {
        vida = 100;
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("dead"))
        {
            if (vida <= 0)
            {
                animator.SetTrigger("dead");
                audio.PlayOneShot(muerte);
                vida = 1;
                Destroy(gameObject, 1.2f);
            }
        }else
        {
            return;
        }
    }

    private void FixedUpdate()
    {
        Vector2 vector = new Vector2(velocidad, 0);
        rigidbody.velocity = vector;
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            animator.SetTrigger("run");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Filp();   
    }

    void Filp()
    {
        velocidad *= -1;
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    public void RecibirBala()
    {
        vida -= costoBala;
    }

    public void Atacar()
    {
       animator.SetTrigger("atack");
        audio.PlayOneShot(golpe);
    }
}
