using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Island : MonoBehaviour
{
    public int money = 100;
    public int health = 50;
    public int fuel = 200;
    public int armor = 50;

    public TextMeshProUGUI moneyUI;
    public TextMeshProUGUI healthUI;
    public TextMeshProUGUI fuelUI;
    public TextMeshProUGUI armorUI;

    public GameObject Canvas;
    public Animator anim;

    public GameObject[] Chests;
    void Start()
    {
        money = Random.Range(500, 2000);
        health = Random.Range(200, 1000);
        fuel = Random.Range(400, 2000);
        armor = Random.Range(200, 1000);

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
            money -= money;
            health -= health;
            fuel -= fuel;
            armor -= armor;
            anim.Play("Explode");
            foreach (GameObject g in Chests)
            Destroy(g, 2);
            Destroy(Canvas, 2);
        }
        else if (col.gameObject.tag == "MyShip")
        {
            Canvas.SetActive(true);
            col.gameObject.GetComponent<ShipController>().GetChestThings(health, armor, fuel, money);
            money -= money;
            health -= health;
            fuel -= fuel;
            armor -= armor;
            anim.Play("Explode");
            foreach (GameObject g in Chests)
                Destroy(g, 2);

            Destroy(Canvas, 2);
        }


    }
}
