using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopIsland : MonoBehaviour
{
    public GameObject ShopButton;

    void Start()
    {
        ShopButton.SetActive(false);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Ship")
        {
            col.gameObject.GetComponent<BotShip>().isEnableShop=true;
            col.gameObject.GetComponent<BotShip>().ShopThings();
        }
        else if (col.tag == "MyShip")
        {
            ShopButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Ship")
        {
            col.gameObject.GetComponent<BotShip>().isEnableShop=false;
        }
        else if (col.tag == "MyShip")
        {
            ShopButton.SetActive(false);
        }
    }
}
