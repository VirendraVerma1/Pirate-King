using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    public int cannonDamage;
    public GameObject cannonDamageFXShip;

    
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ship")
        {
            col.gameObject.GetComponent<BotShip>().TakeDamage(cannonDamage);
            GameObject g = Instantiate(cannonDamageFXShip, gameObject.transform.position,gameObject.transform.rotation);
            Destroy(g, 3f);
            Destroy(gameObject,3);
        }
        else if (col.tag == "MyShip")
        {
            col.gameObject.GetComponent<ShipController>().TakeDamage(cannonDamage);
            GameObject g = Instantiate(cannonDamageFXShip, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(g, 3f);
            Destroy(gameObject,3);
        }
        else 
        {
            Destroy(gameObject, 2);
        }

       // 
    }

    
}
