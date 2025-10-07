using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    
    
    int vida;
    int velocity;
    private Animator miAnimacion;
    private float cooldown_weapon;
    private float cooldown_actual;
    private bool spacepressed;

    public float archerVel;




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vida = 10;
        cooldown_weapon = 1.5F;
        
        miAnimacion = GetComponent <Animator>();

    
        
       
    }

    // Update is called once per frame
    void Update()
    {
    movimientoArcher();

    ataqueArcher();


       
    }//End Update()
    
   void movimientoArcher(){

           if (!Input.anyKey&&spacepressed==false){
        miAnimacion.Play("archer_idle_front");
       }
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
            transform.eulerAngles = new Vector3(0,0,0); //Flipear el personaje junto a la referencia en la coordenada
            transform.Translate(Vector3.right * archerVel * Time.deltaTime);
 
           miAnimacion.Play("archer_run_right");

        }//End move Right
   }//EndMoveArcher


void ataqueArcher(){

if(Input.GetKeyDown(KeyCode.Space)){
miAnimacion.Play("archer_attack_side");
}
}//End ataqueArcher




}
