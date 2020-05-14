using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class chest : MonoBehaviour
{
    public int money = 100;
    public int health = 50;
    public int fuel = 400;
    public int armor = 50;

    public TextMeshProUGUI moneyUI;
    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI fuelUI;
    public TextMeshProUGUI armorUI;

    public GameObject Canvas;
    public Animator anim;
    void Start()
    {
        moneyUI.text = money.ToString();
        healthUI.text = health.ToString();
        fuelUI.text = fuel.ToString();
        armorUI.text = armor.ToString();
        Canvas.SetActive(false);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Ship")
        {
            Canvas.SetActive(true);
            col.gameObject.GetComponent<BotShip>().GetChestThings(health, armor, fuel, money);
            anim.Play("Explode");
            Destroy(gameObject,2);
        }
        else if (col.gameObject.tag == "MyShip")
        {
            Canvas.SetActive(true);
            col.gameObject.GetComponent<ShipController>().GetChestThings(health, armor, fuel, money);
            anim.Play("Explode");
            Destroy(gameObject,2);
        }

        
    }
}
