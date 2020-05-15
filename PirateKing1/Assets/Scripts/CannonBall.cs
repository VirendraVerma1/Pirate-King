using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int cannonDamage;
    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ship")
        {
            col.gameObject.GetComponent<BotShip>().TakeDamage(cannonDamage);
            Destroy(gameObject);
        }
        else if (col.tag == "MyShip")
        {
            col.gameObject.GetComponent<ShipController>().TakeDamage(cannonDamage);
            print("Touch");
            Destroy(gameObject);
        }
        else if (col.tag == "Island")
        {

        }

       // Destroy(gameObject,2);
    }

    
}
