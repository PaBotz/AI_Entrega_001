using UnityEngine;

public class enemy_001 : MonoBehaviour
{
public enum Estados {patrol,chase,attack,explode};
public Estados mystate;

public GameObject Player;
public GameObject point1,point2;
public Transform point3; //Prueba: Uso de transform para ahorrar un paso con GameObject
public GameObject bala;

public Animator miAnimacionTree;

public float enemySpeed;
private int arbol_LastPoint;
public int distance_Low,distance_Med,distance_High; //Iniciadas en Unity



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       miAnimacionTree = GetComponent<Animator>();

        mystate = Estados.patrol;
        arbol_LastPoint = 1;
        enemySpeed = 4;
     

    }

    // Update is called once per frame
    void Update()
    {

    switch (mystate)
{
    case Estados.patrol:
    patroling();
    break;
     case Estados.chase:
    chasing();
    break;

 case Estados.attack:
    attacking();
    break;

    case Estados.explode:
    exploding();
    break;

    default:
    print("Ningun estado seleccionado");
break;

}//End switch

    }//End Update

private void patroling(){ 
    //Point to point move
    if (arbol_LastPoint == 1){
       transform.position = Vector3.MoveTowards(transform.position,point1.transform.position,enemySpeed * Time.deltaTime); //Accede al transform del objeto que lleve el script 
       
         if(Vector3.Distance(transform.position,point1.transform.position) < 0.05f){ //Si El enemigo esta sobre el punto "x", cambia de direccion al punto "y"
        arbol_LastPoint = 2;
       }
    } 
    
    if (arbol_LastPoint == 2){
       transform.position = Vector3.MoveTowards(transform.position,point2.transform.position,enemySpeed * Time.deltaTime);

       if(Vector3.Distance(transform.position,point2.transform.position) < 0.05f){ // 0.05f = 0.05 metros; Todas las mediadas de Unity están en metros
        arbol_LastPoint = 3;
       }
    } 

    if(arbol_LastPoint == 3){
        transform.position = Vector3.MoveTowards(transform.position,point3.position,enemySpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position,point3.position) <0.05f){
            arbol_LastPoint = 1;
        }
    }
 //change State
if(Vector3.Distance(transform.position,Player.transform.position) < distance_High) {
    mystate=Estados.chase;
}
}//End patroling()

private void chasing(){
    transform.position = Vector3.MoveTowards(transform.position,Player.transform.position, enemySpeed * Time.deltaTime);

    if(Vector3.Distance(transform.position,Player.transform.position) <= distance_Med) {
    mystate=Estados.attack;
    
    }   
    if(Vector3.Distance(transform.position,Player.transform.position) > distance_High) {mystate=Estados.patrol;}

}//End chasing()

private void attacking(){

/*if(imshooting)
{
InvokeRepeating(nameof(funcionBala), 0.0f,3.0f);
} */

    if(Vector3.Distance(transform.position,Player.transform.position) <= distance_High && Vector3.Distance(transform.position,Player.transform.position)> distance_Med) 
    {
        mystate = Estados.chase;
    }

    if(Vector3.Distance(transform.position,Player.transform.position) > distance_High) 
    { 
        mystate = Estados.patrol;
    }

    if (Vector3.Distance(transform.position,Player.transform.position) < distance_Med){
        mystate = Estados.explode;
    }
    
}//End attacking()


void exploding(){
// si pasa una distancia minima el enemy:
//Crecera
for(int i=0;i< 30;i++){
transform.scale ++;
}


//Desparezcera

//Se dibujará un círculo que crezca rápidamente para simular una explosión en un rango


}


private void funcionBala(){

Instantiate(bala,transform.position,transform.rotation);

}

}//End Class
