using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FuelSystem : MonoBehaviour
{
    public float TotalFuel = 10000;
    public TextMeshProUGUI UIText;

    void OnTriggerStay(Collider col)
    {
        if (col.tag == "Ship")
        {
            TotalFuel -= 0.2f;
            UpdateUI();
            col.GetComponent<BotShip>().RechargeFuel(0.2f);
        }else if(col.tag=="MyShip")
        {
            TotalFuel -= 0.2f;
            UpdateUI();
            col.GetComponent<ShipController>().RechargeFuel(0.2f);
        }
    }

    void UpdateUI()
    {
        UIText.text = Mathf.RoundToInt( TotalFuel) + "/" + "10000";
    }
}
