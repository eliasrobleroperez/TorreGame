using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVidaScript : MonoBehaviour
{
    [Tooltip("Vida máxima del jugador")]
    public int VidaMaxima;


    private Image relleno;

    private int vidaActual;


    // Start is called before the first frame update
    void Start()
    {
        relleno = GetComponent<Image>();
        vidaActual = VidaMaxima;

    //para probar
    vidaActual = 50;

        UpdateBarraVida();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public bool aplicarHerida(int herida){
        vidaActual -= herida;


        if(vidaActual > 0){
            UpdateBarraVida();
            return false;
        }
        vidaActual = 0;

        UpdateBarraVida();
        return true;
    }

    void UpdateBarraVida(){
        float porcentaje = vidaActual *1.0f/VidaMaxima;

        relleno.fillAmount = porcentaje;
    }
}
