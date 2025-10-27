using UnityEngine;

public class src_bala : MonoBehaviour
{
  public GameObject Player;
  private Vector3 direccion;
  public int velocidad;

    void Start()
    {

        direccion = (Player.transform.position - transform.position).normalized; //Se resta para saber en que punto parte la bala y donde está el personaje (a donde se dirije)
                                                                                 //Además, se normaliza, siendo un valor siempre de 1 en total delntro del Vector3.

    }//END START

   
    void Update()
    {
       transform.Translate(direccion.normalized * velocidad * Time.deltaTime, Space.World);
       
    }//END UPDATE
}//END CLASS
