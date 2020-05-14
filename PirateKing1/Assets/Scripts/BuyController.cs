using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        BuyHealth.text = saveload.healthBuy.ToString();
        BuyArmor.text = saveload.armorBuy.ToString();
        BuyFuel.text = saveload.fuelBuy.ToString();
        BuyCannon.text = saveload.cannonBuy.ToString();
        BuySpeed.text = saveload.speedBuy.ToString();

        HealthImageBar.rectTransform.localScale = new Vector3(saveload.health / saveload.maxhealth, 1, 1);
        ArmorImageBar.rectTransform.localScale = new Vector3(saveload.armor / saveload.maxarmor, 1, 1);
        FuelImageBar.rectTransform.localScale = new Vector3(saveload.fuel / saveload.maxfuel, 1, 1);

        MyShip.GetComponent<ShipController>().UnlockCannon();
    }

    

    public void OnHealthBuyButtonPressed()
    {
        if (saveload.money >= saveload.healthBuy)
        {
            saveload.money -= saveload.healthBuy;
            saveload.maxhealth += 100;
            saveload.health = saveload.maxhealth;
            saveload.healthBuy += 50;
        }

        UIInitialize();
    }

    public void OnArmorBuyButtonPressed()
    {
        if (saveload.money >= saveload.armorBuy)
        {
            saveload.money -= saveload.armorBuy;
            saveload.maxarmor += 100;
            saveload.armor = saveload.maxarmor;
            saveload.armorBuy += 50;
        }

        UIInitialize();
    }

    public void OnFuelBuyButtonPressed()
    {
        if (saveload.money >= saveload.fuelBuy)
        {
            saveload.money -= saveload.fuelBuy;
            saveload.maxfuel += 100;
            saveload.fuel = saveload.maxfuel;
            saveload.fuelBuy += 50;
        }

        UIInitialize();
    }

    public void OnSpeedBuyButtonPressed()
    {
        if (saveload.money >= saveload.speedBuy)
        {
            saveload.money -= saveload.speedBuy;
            saveload.speed += 0.005f;
            saveload.speedBuy += 500;
        }

        UIInitialize();
    }

    public void OnCannonBuyButtonPressed()
    {
        if (saveload.money >= saveload.cannonBuy)
        {
            saveload.money -= saveload.cannonBuy;
            saveload.cannonCount += 1;
            if (saveload.cannonCount >= saveload.maxcannonCount)
                saveload.cannonCount += saveload.maxcannonCount;
            saveload.cannonBuy += 1000;
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
    
}
