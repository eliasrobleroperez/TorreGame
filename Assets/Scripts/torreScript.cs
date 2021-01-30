using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torreScript : MonoBehaviour
{
    [Header("Variables de disparo")]
    [Tooltip("Distancia maxima de disparo de la torre")]
    
    public float radioRango;
    [Tooltip("Tiempo de recarga")]
    
    public float tiempoRecarga;
    [Tooltip("Prefab del proyectil")]
    
    public GameObject PrefabProyectil;
    [Tooltip("Tiempo pasado de la ultima recarga")]
    
    private float tiempoUltimoDisparo;

    [Header("Niveles de torre")]
    [Tooltip("Nivel actual de torre")]

    public int nivelActual; 

    
    [Tooltip("Sprites de los niveles de la torre")]
    
    public Sprite[] nivelSprite;

    [Tooltip("Variable para verificar si la torre puede subir de nivel")]

    public bool seActualiza = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(tiempoUltimoDisparo >= tiempoRecarga)
        {
            tiempoUltimoDisparo = 0;
            //Encontrar todos los GameObject que tengan un collider dentro del rango de disparo
            Collider2D[] disparoColision = Physics2D.OverlapCircleAll(transform.position, radioRango);

            if(disparoColision.Length != 0){
                //Logica de los disparos con posibles blancos
                //Pandas más cercanos
                float distanciaMinima = int.MaxValue;
                int indice = -1;

                for(int i=0; i<disparoColision.Length;i++){
                    if(disparoColision[i].tag=="Enemigo"){
                        float distancia = Vector2.Distance(disparoColision[i].transform.position, this.transform.position);
                        if(distancia < distanciaMinima){
                            indice = i;
                            distanciaMinima = distancia;
                        }
                    }
                }
                if(indice < 0){
                    return;
                }
                //Existe blanco a disparar
                Transform blanco = disparoColision[indice].transform;
                Vector2 direccion = (blanco.position -this.transform.position).normalized;

                //Disparar
                //Se crea el proyectil con una instancia del Prefab del proyectil
                GameObject proyectil = GameObject.Instantiate(PrefabProyectil, this.transform.position, Quaternion.identity) as GameObject;
                proyectil.GetComponent<projectilScript>().direccion=direccion;
            }
        }
        tiempoUltimoDisparo += Time.deltaTime;
    }

    public void subirNivel(){
        if(!seActualiza){
            return;
        }

        this.nivelActual++;

        if(this.nivelActual == nivelSprite.Length){
            seActualiza = false;
        }
        //Mejorar estado de la torre
        radioRango +=1f;
        tiempoRecarga -= 0.5f;

        this.GetComponent<SpriteRenderer>().sprite = nivelSprite[nivelActual];

    }
}
