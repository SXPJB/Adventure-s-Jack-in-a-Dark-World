using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Text tiempoText;
    public float tiempo;
    private float tiempoInicial;
    private JackController jackController;
    // Start is called before the first frame update

    // Update is called once per frame

    private void Start()
    {
        jackController = GameObject.Find("Jack").GetComponent<JackController>();
        tiempoInicial = 3;
    }

    void Update()
    {
        tiempo -= Time.deltaTime;
        tiempoText.text = "" + tiempo.ToString("f0");
        if (!hayTiempo()|| jackController.numVidas<0)
        {
            if (jackController.numVidas > 0)
            {
                jackController.respawn();
                tiempo = tiempoInicial;

            }
            else
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +1);
            }
        }
    }

    public bool hayTiempo()
    {
        bool hayTiempo = true;
        if (tiempo < 0)
        {
            hayTiempo = false;
        }
        return hayTiempo;
    }

}
