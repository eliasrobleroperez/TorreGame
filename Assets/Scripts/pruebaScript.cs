using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pruebaScript : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("Valor de variables: ")]

    public int datos1 = 4;

    [Header("Valor mostrar: ")]

    public int valorMostrar = 5;

    [Range(-3,3)]

    public int valorRango;

    [Tooltip("Nombre del enemigo")]

    public string nombreEnemigo;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
