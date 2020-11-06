using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaerPlataforma : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private PolygonCollider2D polygonCollider2D;
    public float dilayCaida = 1f;
    public float respawnDelay = 5f;
    private Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
        start = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("Caer", dilayCaida);
            Invoke("Repaw", dilayCaida + respawnDelay);
        }
    }

    public void Caer()
    {
       rigidbody.isKinematic = false;
       polygonCollider2D.isTrigger = true;
    }

    public void Repaw()
    {
        transform.position = start;
        rigidbody.isKinematic = true;
        rigidbody.velocity = Vector3.zero;
        polygonCollider2D.isTrigger = false;
    }
}
