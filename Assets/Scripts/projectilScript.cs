using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectilScript : MonoBehaviour
{
    // Start is called before the first frame update

    [Tooltip("Daño que recibira el enenmigo")]
    public float herida;

    [Tooltip("velocidad del proyectil")]

    public float velocidad=1f;

    [Tooltip("Direccion en la que apunta el proyectil")]


    public Vector3 direccion;

    [Tooltip("Tiempo de vida del proyectil en segundos")]

    public float tiempoDuracion=10f;

    //agregue 28 enero
    private Rigidbody2D rb2d;

    void Start()
    {
        //agregue 28 de enero
        rb2d= GetComponent<Rigidbody2D>();


        //Normalizacion del proyectil

        direccion= direccion.normalized;

        //Rotamos el proyectil
        float anguloRad= Mathf.Atan2(direccion.y, direccion.x);

        float anguloDeg = anguloRad * Mathf.Rad2Deg;
        

        transform.rotation = Quaternion.AngleAxis(anguloDeg, Vector3.forward);


        // Pre programar destrucción del proyectil
       Destroy (gameObject, tiempoDuracion);
        
    }

    // Update is called once per frame
    // se cambio 28 de enero
    void FixedUpdate()
    {
        // s=v*t

    
        //transform.position += (velocidad* direccion)*Time.deltaTime;    
        rb2d.MovePosition(transform.position+(velocidad*direccion)* Time.fixedDeltaTime);
    }
}
