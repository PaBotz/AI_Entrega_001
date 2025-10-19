using UnityEngine;

public class player_001 : MonoBehaviour
{
    
    
   // int vida;
    int velocity;
    private Animator miAnimacion;
    private float cooldown_actual;
    private bool spacepressed;

    public float archerVel;

    public enum Estados{caminar,idle,atacar}; //UNA LINEA EN LUGAR DE DOS
    public Estados myEstadoP;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //vida = 10;
        
        myEstadoP = Estados.idle;

        miAnimacion = GetComponent <Animator>();

       
    }

    // Update is called once per frame
    void Update()
    {
    switch (myEstadoP){
case Estados.caminar:
caminanding();
break;
case Estados.idle:
idling();
break;
case Estados.atacar:
atacanding();
break;
    }


       
    }//End Update()
    

   //void movimientoArcher(){
   void caminanding(){

        if (Input.GetKey(KeyCode.W)){
           transform.Translate(Vector3.up * archerVel * Time.deltaTime);
           if(!Input.GetKey(KeyCode.A)&&!Input.GetKey(KeyCode.D)){
           miAnimacion.Play("archer_run_back");
           }
        }//End move Left
  
        if (Input.GetKey(KeyCode.A)){
            transform.eulerAngles = new Vector3(0,180,0); //Flipear el personaje junto a la referencia en la coordenada
           transform.Translate(Vector3.right * archerVel * Time.deltaTime);
           miAnimacion.Play("archer_run_right");
           
        }//End move left

        if (Input.GetKey(KeyCode.S)){
           transform.Translate(Vector3.down * archerVel * Time.deltaTime);
           if(!Input.GetKey(KeyCode.A)&&!Input.GetKey(KeyCode.D)){
           miAnimacion.Play("archer_run_front");
           }
        }//End move down

        if (Input.GetKey(KeyCode.D)){
            transform.eulerAngles = new Vector3(0,0,0); //Flipear el personaje junto a la referencia en la coordenada; Gira el gizmo del personaje
            transform.Translate(Vector3.right * archerVel * Time.deltaTime);
 
           miAnimacion.Play("archer_run_right");

        }//End move Right

        if(!Input.anyKey){
         myEstadoP = Estados.idle;
        }
        if(Input.GetKeyDown(KeyCode.Space)){
         myEstadoP = Estados.atacar;
        }
   }//END void caminanding()


void idling(){

miAnimacion.Play("archer_idle_front");

       if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){
         myEstadoP = Estados.caminar;
       }
        if(Input.GetKeyDown(KeyCode.Space)){
         myEstadoP = Estados.atacar;
        }
}//End atacanding()


void atacanding(){


miAnimacion.Play("archer_attack_side");


}//End atacanding


/*
Esta funcion está enlazada en a nuestra animación de ataque en la ventana de animation.
Tenemos un evento que inicia cuando finaliza la animación.
Este Evento tiene como opciones los estados que tengamos asociados al script, el estado escogido será el que se reproducirá tras acabar el evento, volviendo así al bucle de estados.
*/
public void setState(Estados newState){
   myEstadoP = newState;
}

}//END CLASS
