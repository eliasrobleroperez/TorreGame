using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoScript : MonoBehaviour
{
    [Tooltip("Vida del panda")]
    public float vida;
    public float velocidad;
    private Animator animator;
//agregue 28 de enero
    private Rigidbody2D rb2d ;

//Hash representando los nombre los trigger del animator del panda
    private int animatorMuerteTrigger= Animator.StringToHash("MorirTrigger");
    private int animatorComerTrigger= Animator.StringToHash("ComerTrigger");
    private int animatorGolpeTrigger= Animator.StringToHash("GolpearTrigger");

    //agregue 28 de enero es para GameManeger
    //es una variable compartida por todos los pandas
    private static GameManager gameManager;
    //waypoint actual al que se dirige el panda
    private int currentWayPointNumber;
    //umbral a partir del cual se considera que se alcanzo el waypoint
    private const float waypointThreshold= 0.001f;

    void Start()
    {
        //agregue 28 de enero
        if (gameManager == null){
            gameManager= FindObjectOfType<GameManager>();
        }

        animator= GetComponent<Animator>();
        //agregue 28 de enero
        rb2d= GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        //agregue 28 de enero
        //comprobar si el panda llego al waypoint final (pastel)
        //si el panda llego, se activa la animacion comer y el elimar script del panda
        if(currentWayPointNumber == gameManager.waypoints.Length){
            Comer();
            return;
        }
        //si el panda no esta en el waypoint final, hay que calcular
        //hay que calcular la distancia que existe entr el panda y 
        //el waypoint que se dirige
        float distance= Vector2.Distance(this.transform.position,gameManager.waypoints[currentWayPointNumber]);
        // si panda esta cerca del waypoint. ir al waypoint
        if(distance <= waypointThreshold){
            currentWayPointNumber++;
        }
        else {
            MoveToward(gameManager.waypoints[currentWayPointNumber]);

        }

    }
    private void MoveToward(Vector3 destino){
         //espacio= velocidad * tiempo
        float espacio= velocidad * Time.fixedDeltaTime;
        //this.transform.position= Vector3.MoveTowards(this.transform.position, destino, espacio);
     
        //agregue 28 de enero
        rb2d.MovePosition(Vector3.MoveTowards(this.transform.position, destino,espacio));
     }

     private void Golpe(float herida)
     {
         this.vida -= herida;
         if(this.vida >0)
         {
             this.animator.SetTrigger(animatorGolpeTrigger);
         }
         else
         {
             this.animator.SetTrigger(animatorMuerteTrigger);
         }
     }          
     private void Comer(){
         this.animator.SetTrigger(animatorComerTrigger);
        //agregue 28 de enero
        Destroy(this);
     }   

    // agregue 28 de enero
     void OnTriggerEnter2D(Collider2D otroCollider){
         if(otroCollider.tag == "Proyectil"){
             Golpe(otroCollider.GetComponent<projectilScript>().herida);

         }

     }
   
}
