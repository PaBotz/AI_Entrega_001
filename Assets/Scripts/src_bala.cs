using UnityEngine;

public class src_bala : MonoBehaviour
{
  public GameObject Player;
  private Vector3 personajeUbicacion;
  private int velocidad;

    void Start()
    {
      

      velocidad = 5;

    }//END START

   
    void Update()
    {
       personajeUbicacion = Player.transform.position;
       transform.Translate(personajeUbicacion.normalized * velocidad * Time.deltaTime);
      Debug.Log("personajeUbi " + personajeUbicacion);
    }//END UPDATE
}//END CLASS
