using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class JackController : MonoBehaviour
{

    private Rigidbody2D rigidbody2D = null;
    private Animator animator = null;
    public int maxVel = 5;
    public bool haciaDerecha=true;
    public bool jumpuing = false;
    public float yJumpForce = 6.5f;
    Vector2 jumpForce;
    public bool isOnTheFloor;
    public GameObject balaCalabaza;
    public Slider slider;
    public int energia;
    public int costoAtaque = 10;
    private Vector3 posicionInicial;
    public Text vidasTxt;
    public int numVidas;
    AudioSource audio;
    public AudioClip salto;
    public AudioClip lanzar;
    public AudioClip caida;
    public bool estasMuestro;
    
    // Start is called before the first frame update
    void Start()
    {
        energia = 100;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        posicionInicial = transform.position;
        numVidas = 3;
        audio = GetComponent<AudioSource>();
        estasMuestro = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetTrigger("slide");
            audio.PlayOneShot(lanzar);
        }
        slider.value = energia;
        vidasTxt.text = numVidas.ToString();
    }

    public void RecibirAtaque()
    {
        energia -= costoAtaque;
    }

    void FixedUpdate()
    {
        movinetoHorizontal();
        VerificarInputParaSaltar();
        veficicarSiTieneVida();
    }
    
    void movinetoHorizontal()
    {
        float velocity = Input.GetAxis("Horizontal");
        Vector2 vector = new Vector2(0, rigidbody2D.velocity.y);
        velocity *= maxVel;
        vector.x = velocity;
        rigidbody2D.velocity = vector;
        animator.SetFloat("run", Mathf.Abs(vector.x));
        if (haciaDerecha && velocity < 0)
        {
            haciaDerecha = false;
            Flip();
        }
        else if (!haciaDerecha && velocity > 0)
        {
            haciaDerecha = true;
            Flip();
        }
    }

    private void VerificarInputParaSaltar()
    {
        isOnTheFloor = rigidbody2D.velocity.x == 0;

        if (Input.GetAxis("Jump") > 0.1f)
        {
            if (!jumpuing && isOnTheFloor)
            {
                animator.SetTrigger("jump");
                jumpForce.x = 0f;
                jumpForce.y = yJumpForce;
                rigidbody2D.AddForce(jumpForce,ForceMode2D.Impulse);
                jumpuing = true;
                audio.PlayOneShot(salto);
            }
        }
        else
        {
            jumpuing = false;
        }
    }

    void Flip()
    {
        var s = transform.localScale;
        s.x *= -1;
        transform.localScale = s;
    }

    public void EmitirCalabaza()
    {
        GameObject calabazaCopy = Instantiate(balaCalabaza);
        calabazaCopy.transform.position = transform.position;
        calabazaCopy.GetComponent<BalaCalabaza>().direccion = new Vector3(transform.localScale.x, 0, 0);
        energia -= 5;
    }
    void veficicarSiTieneVida()
    {
        if (energia <= 0&&!estasMuestro)
        {
            estasMuestro = true;
            if (!animator.GetCurrentAnimatorStateInfo(0).IsName("dead"))
            {
                audio.PlayOneShot(caida);
                animator.SetTrigger("dead");       
                energia = 1;
            }
            Invoke("respawn", 1f);
        }
        else
        {
            estasMuestro= false;
            return;
        }
    }

    private void OnBecameInvisible()
    {
        respawn();
    }

    public void respawn()
    {
        transform.position = posicionInicial;
        numVidas--;
        if (energia >= 0||energia <= 100)
        {
            energia = 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
        }
    }
}
