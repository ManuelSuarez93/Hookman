using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class DoorScript : MonoBehaviour
{
    [Tooltip("Variable para chequear si se conecta el gancho al boton")]
    public bool Connect = false;
    [Tooltip("Velocidad a la que la puerta vuelve")]
    public float closeSpeed;
    [Tooltip("Velocidad a la que se abre la puerta")]
    public float openSpeed;
    

    private Vector2 _open = new Vector2(0,1);


    //Posicion original de la puerta a donde volver
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //Si se conecta la puerta se mueve, al desconectar (vuelva a donde esta)
        if (Connect)
        {
            if(transform.localScale.x > 0 && transform.localScale.y > 0)
            {
                StopAllCoroutines();
                StartCoroutine(openDoor());
            }
            else
            {
                StopAllCoroutines();

            }
        }
        else
        {
            StartCoroutine(closeDoor());
        }
        
    }

    public void DoorConnect(bool connecting)
    {
        Connect = connecting;
    }

    IEnumerator openDoor()
    {
        //Vector2 newPos = new Vector2(transform.localScale.x - direction.x * Time.deltaTime * mirrorSpeed, transform.localScale.y - direction.y * Time.deltaTime * mirrorSpeed );
        transform.localScale = Vector2.Lerp(transform.localScale, _open, openSpeed * Time.fixedDeltaTime * 0.1f);
        yield return new WaitForSeconds(0.1f);
    }

    IEnumerator closeDoor()
    {
        while(transform.localScale.x < 1 || transform.localScale.y < 1)
        {
            //Vector2 newPos = new Vector2(transform.localScale.x + direction.x * Time.fixedUnscaledDeltaTime * closeSpeed, transform.localScale.y + direction.y * Time.fixedUnscaledDeltaTime * closeSpeed);
            transform.localScale = Vector2.Lerp(transform.localScale, Vector2.one, closeSpeed*Time.fixedDeltaTime * 0.1f);
            yield return new WaitForSeconds(0.1f);
        }
        
       
    }
}
