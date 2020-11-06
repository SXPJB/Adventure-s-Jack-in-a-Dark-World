using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtackController : MonoBehaviour
{
    Collider2D atacandoA;
    public float probabilidadAtaque = 1f;
    EnemigoController enemigoController;

    // Start is called before the first frame update
    void Start()
    {
        enemigoController = GameObject.Find("Hero").GetComponent<EnemigoController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Jack") && atacandoA == null)
        {
            JackController jackController = collision.gameObject.GetComponent<JackController>();
            DesidirAtacar(collision);
            jackController.RecibirAtaque();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision = atacandoA)
        {
            atacandoA = null;
        }
    }

    private void DesidirAtacar(Collider2D collision)
    {
        if(Random.value < probabilidadAtaque)
        {
            Atacar();
            atacandoA = collision;
        }
    }

    private void Atacar()
    {
        enemigoController.Atacar();
    }


}
