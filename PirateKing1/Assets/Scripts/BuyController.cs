using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BuyController : MonoBehaviour
{
    public GameObject MyShip;

    [Header("Shop Update")]
    public Text BuyHealth;
    public Text BuyArmor;
    public Text BuyFuel;
    public Text BuyCannon;
    public Text BuySpeed;

    [Header("Shop Stats")]
    public Text HealthText;
    public Text ArmorText;
    public Text FuelText;
    public Text SpeedText;
    public Text CannonCountText;

    public Image HealthImageBar;
    public Image ArmorImageBar;
    public Image FuelImageBar;

    public Text MoneyText;
    void Start()
    {
        UIInitialize();
    }

    void UIInitialize()
    {
        MoneyText.text = saveload.money.ToString() ;
        HealthText.text = saveload.health + "/" + saveload.maxhealth;
        ArmorText.text = saveload.armor + "/" + saveload.maxarmor;
        FuelText.text = saveload.fuel + "/" + saveload.maxfuel;
        SpeedText.text = saveload.speed.ToString();
        CannonCountText.text = saveload.cannonCount+"/"+saveload.maxcannonCount;

        BuyHealth.text = (saveload.healthlvl*100).ToString();
        BuyArmor.text = (saveload.armorlvl*100).ToString();
        BuyFuel.text = (saveload.fuellvl*100).ToString();
        BuyCannon.text = (saveload.cannonlvl*1000).ToString();
        BuySpeed.text = (saveload.speedlvl*500).ToString();

        HealthImageBar.rectTransform.localScale = new Vector3(saveload.health / saveload.maxhealth, 1, 1);
        ArmorImageBar.rectTransform.localScale = new Vector3(saveload.armor / saveload.maxarmor, 1, 1);
        FuelImageBar.rectTransform.localScale = new Vector3(saveload.fuel / saveload.maxfuel, 1, 1);

        MyShip.GetComponent<ShipController>().UnlockCannon();
    }

    

    public void OnHealthBuyButtonPressed()
    {
        if (saveload.money >= saveload.healthlvl*100)
        {
            saveload.money -= saveload.healthlvl * 100;
            saveload.maxhealth += 100;
            saveload.health = saveload.maxhealth;
            saveload.healthlvl ++;
        }

        UIInitialize();
    }

    public void OnArmorBuyButtonPressed()
    {
        if (saveload.money >= saveload.armorlvl * 100)
        {
            saveload.money -= saveload.armorlvl * 100;
            saveload.maxarmor += 100;
            saveload.armor = saveload.maxarmor;
            saveload.armorlvl++;
        }

        UIInitialize();
    }

    public void OnFuelBuyButtonPressed()
    {
        if (saveload.money >= saveload.fuellvl * 100)
        {
            saveload.money -= saveload.fuellvl * 100;
            saveload.maxfuel += 200;
            saveload.fuel = saveload.maxfuel;
            saveload.fuellvl ++;
        }

        UIInitialize();
    }

    public void OnSpeedBuyButtonPressed()
    {
        if (saveload.money >= saveload.speedlvl*500)
        {
            saveload.money -= saveload.speedlvl*500;
            saveload.speed += 0.005f;
            saveload.speedlvl ++;
        }

        UIInitialize();
    }

    public void OnCannonBuyButtonPressed()
    {
        if (saveload.money >= saveload.cannonlvl)
        {
            saveload.money -= saveload.cannonlvl;
            saveload.cannonCount += 1;
            if (saveload.cannonCount >= saveload.maxcannonCount)
                saveload.cannonCount += saveload.maxcannonCount;
            saveload.cannonlvl ++;
        }

        UIInitialize();
    }
    [Header("Shop Pannel")]
    public GameObject ShopPannel;

    public void OnShopOpenButtonPressed()
    {
        ShopPannel.SetActive(true);
        UIInitialize();
    }

    public void OnShopCloseButtonPressed()
    {
        ShopPannel.SetActive(false);
    }

    public void OnReloadButtonGame()
    {
        SceneManager.LoadScene(0);
    }
    
}
