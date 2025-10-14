using UnityEngine;

public class tree_RPG : MonoBehaviour
{
public enum Estados {patrol,chase,attack};
public Estados mystate;

public GameObject Player;
public GameObject point1,point2;
public Transform point3;
public GameObject bala;
public Animator miAnimacionTree;
public bool imshooting;


public float enemySpeed;
public int enemyVision, arbolPoint;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       miAnimacionTree = GetComponent<Animator>();

        mystate = Estados.patrol;
        arbolPoint = 1;
        enemySpeed = 4;
        enemyVision = 3;
        

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

    default:
    print("Ningun estado seleccionado");
break;

}//End switch

    }//End Update

private void patroling(){ 
    //Point to point move
    if (arbolPoint == 1){
       transform.position = Vector3.MoveTowards(transform.position,point1.transform.position,enemySpeed * Time.deltaTime); //Accede al transform del objeto que lleve el script
       
         if(Vector3.Distance(transform.position,point1.transform.position) < 0.05f){ //Si El enemigo esta sobre el punto "x", cambia de direccion al punto "y"
        arbolPoint = 2;
       }
    } 
    
    if (arbolPoint == 2){
       transform.position = Vector3.MoveTowards(transform.position,point2.transform.position,enemySpeed * Time.deltaTime);

       if(Vector3.Distance(transform.position,point2.transform.position) < 0.05f){ // 0.05f = 0.05 metros; Todas las mediadas de Unity están en metros
        arbolPoint = 3;
       }
    } 

    if(arbolPoint == 3){
        transform.position = Vector3.MoveTowards(transform.position,point3.position,enemySpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position,point3.position) <0.05f){
            arbolPoint = 1;
        }
    }
 //change State
if(Vector3.Distance(transform.position,Player.transform.position) <enemyVision) {
    mystate=Estados.chase;
}
}//End patroling()

private void chasing(){
    transform.position = Vector3.MoveTowards(transform.position,Player.transform.position, enemySpeed * Time.deltaTime);

    if(Vector3.Distance(transform.position,Player.transform.position) <= 1) {
    mystate=Estados.attack;
    
    }   
    if(Vector3.Distance(transform.position,Player.transform.position) >enemyVision) {mystate=Estados.patrol;}

}//End chasing()

private void attacking(){

/*if(imshooting)
{
InvokeRepeating(nameof(funcionBala), 0.0f,3.0f);
} */
    if(Vector3.Distance(transform.position,Player.transform.position) <=enemyVision && Vector3.Distance(transform.position,Player.transform.position)>1 ) {mystate=Estados.chase;}

    if(Vector3.Distance(transform.position,Player.transform.position) >enemyVision) { mystate=Estados.patrol;}
    
}//End attacking()

private void funcionBala(){

Instantiate(bala,transform.position,transform.rotation);

}

}//End Class
