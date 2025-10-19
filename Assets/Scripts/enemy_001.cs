using Unity.VisualScripting;
using UnityEngine;

public class enemy_001 : MonoBehaviour
{
public enum Estados {patrol,chase,attack,explode};
public Estados mystate;

public GameObject Player, explosion_Effect;
public GameObject point1,point2;
public Transform point3; //Prueba: Uso de transform para ahorrar un paso con GameObject
public GameObject bala;

public Animator miAnimacionTree;

public float enemySpeed;
private int arbol_LastPoint;

public int distance_Low,distance_Med,distance_High;    //Iniciadas en Unity  
private Vector3 ultimaPosicion, mov_direccion;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       miAnimacionTree = GetComponent<Animator>();

        mystate = Estados.patrol;
        arbol_LastPoint = 1;
        ultimaPosicion = transform.position; 
     

    }

    // Update is called once per frame
    void Update()
    {

     calcular_Direccion_mov();
     
        ultimaPosicion = transform.position;

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
        updateAnimation();
        miAnimacionTree.speed = 0.6f;
        enemySpeed = 1;
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
if(Vector3.Distance(transform.position,Player.transform.position) < distance_Med) {
    mystate=Estados.chase;
}
}//End patroling()

private void chasing(){
        updateAnimation();
        miAnimacionTree.speed = 1.5f;
        transform.position = Vector3.MoveTowards(transform.position,Player.transform.position, enemySpeed * Time.deltaTime);
    enemySpeed = 6;

        if (Vector3.Distance(transform.position,Player.transform.position) <= distance_Low) {
    mystate=Estados.attack;
    
    }   
    if(Vector3.Distance(transform.position,Player.transform.position) > distance_High) {mystate=Estados.patrol;}

}//End chasing()

private void attacking(){
        miAnimacionTree.speed = 1f;
        /*if(imshooting)
        {
        InvokeRepeating(nameof(funcionBala), 0.0f,3.0f);
        } */

        miAnimacionTree.Play("tree_Explode");


      
}//End attacking()


void exploding(){
     
        Destroy(gameObject);

       if (explosion_Effect != null)
        {
           GameObject explosion_Ins = Instantiate(explosion_Effect, transform.position, Quaternion.identity);
            Destroy(explosion_Ins,0.05f);
        }
        else Debug.LogWarning("No se asignó el prefab de círculo de explosión en el inspector.");
    }

 
    


    private void calcular_Direccion_mov()
    {
        if (transform.position != ultimaPosicion)   //Esto lo entiendo como una fragmentacion del transform.position.
        {                                           //Ya que al igualar ultimaposicion a transform.position podremos notar cambios en la polarizacion del eje.(+ o -)
            mov_direccion = (transform.position - ultimaPosicion).normalized;
        }
        else{ mov_direccion = Vector3.zero; }
        
    }


    void updateAnimation()
    {
         if (mov_direccion == Vector3.zero) { miAnimacionTree.Play("tree_Idle"); }
       

            bool mov_derecha = mov_direccion.x > 0;
            bool mov_izquierda = mov_direccion.x < 0;
            bool mov_arriba = mov_direccion.y > 0;
            bool mov_abajo = mov_direccion.y < 0;




        bool movimientoHorizontal = Mathf.Abs(mov_direccion.x) > Mathf.Abs(mov_direccion.y);

        if (movimientoHorizontal)
        {
            // Movimiento horizontal (derecha/izquierda)
            if (mov_direccion.x > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                miAnimacionTree.Play("tree_running_side");
            }
            else
            {
                
                transform.eulerAngles = new Vector3(0, 180, 0);
                miAnimacionTree.Play("tree_running_side");
            }
        }
        else
        {
            // Movimiento vertical (arriba/abajo)
            if (mov_direccion.y > 0)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                miAnimacionTree.Play("tree_running_back");
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                miAnimacionTree.Play("tree_running_front");
            }
        }
    }


    private void funcionBala(){

Instantiate(bala,transform.position,transform.rotation);

}

}//End Class
