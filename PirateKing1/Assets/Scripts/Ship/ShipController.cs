using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour
{
    //Ship Stats Variable
    

    [SerializeField]
    private LineRenderer line;
    
    [Header("Ship Cannons")]
    public GameObject[] ShipCannonsGo;
    public GameObject CannonBallGameObject;

    public float movementSpeed = 10;
    public float acceleration = 2;
    public float time = 0.05f;
    private int totalEnemy;

    void Awake()
    {
        saveload.money = 2000;
        saveload.health = 100;
        saveload.maxhealth = 100;
        saveload.armor = 100;
        saveload.maxarmor = 100;
        saveload.maxfuel = 600;
        saveload.fuel = 600;
        saveload.speed = 2;
        saveload.maxcannonCount = ShipCannonsGo.Length;
        saveload.cannonCount = 2;
        UnlockCannon();
        GameObject[] go = GameObject.FindGameObjectsWithTag("Ship");
        totalEnemy = go.Length;
    }

    public void UnlockCannon()
    {
        for (int i = 0; i < ShipCannonsGo.Length; i++)
        {
            ShipCannonsGo[i].SetActive(false);
        }

        for (int i = 0; i < saveload.cannonCount; i++)
        {
            ShipCannonsGo[i].SetActive(true);
        }

    }


    void Start()
    {
        //line.positionCount = 0;
        
        
        islowFuel=false;
        UpdateUI();
        //line = GetComponentInChildren<LineRenderer>();
        StartCoroutine(ReduceFuel());
    }

    bool islowFuel = false;
    IEnumerator ReduceFuel()
    {
        int n = 1;
        while (n > 0)
        {
            yield return new WaitForSeconds(1);
            //print(fuel);
            saveload.fuel -= 1;
            if (saveload.fuel < 200)
            {
                //low fuel
                //go to the fuelStaton
                islowFuel = true;

            }
            else if (saveload.fuel > 900)
            {
                //high fuel

                islowFuel = false;
            }
            UpdateUI();
        }
    }

    #region ShipMovement

    public Joystick joystick;
    public bool isAndroid;
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        if (isAndroid)
        {
            moveHorizontal = joystick.Horizontal;
            moveVertical = joystick.Vertical;
        }

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), time);


        transform.Translate(movement * movementSpeed * acceleration * Time.deltaTime, Space.World);

        CheckEnemy();
    }


    #endregion

    public Text TotalEnemy;
    public GameObject GameWonPannel;

    void CheckEnemy()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("Ship");
        TotalEnemy.text = go.Length + "/" + totalEnemy;

        if (go.Length < 1)
        {
            //game won
            GameWonPannel.SetActive(true);
        }
    }

    

    #region Ship Fire
    

    public void OnFireButtonDown()
    {
        //linerender direction on hold button
        
        
    }

    public void OnFireButtonUp()
    {
        //stop linerendrer and shoot
        for (int i = 0; i < saveload.cannonCount; i++)
        {
            GameObject go = Instantiate(CannonBallGameObject, ShipCannonsGo[i].transform.position + new Vector3(0, 0.6f, 0), ShipCannonsGo[i].transform.rotation);
            go.GetComponent<CannonBall>().cannonDamage = 10;
            go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            go.transform.GetComponent<Rigidbody>().AddForce(-go.transform.forward * 15, ForceMode.Impulse);
        }
            
    }

    #endregion

    #region other public methods
    public GameObject GameLoosePannel;
    public void TakeDamage(float damagea)
    {
        if (saveload.armor > damagea)
            saveload.armor -= damagea;
        else
        {
            saveload.armor = 0;
            saveload.health -= damagea;
        }
        if (saveload.health < 0)
        {
            //sink TODO
            GameLoosePannel.SetActive(true);
            Destroy(gameObject);
        }
        
    }

    public void RechargeFuel(float no)
    {
        if (saveload.fuel < saveload.maxfuel)
        {
            saveload.fuel += no;
        }
        else
        {
            saveload.fuel = saveload.maxfuel;
        }
    }

    [Header("UI")]
    public Image HealthBarUI;
    public Image ArmorBarUI;
    public Image FuelBarUI;
    public Text MoneyUI;

    public void UpdateUI()
    {
        HealthBarUI.rectTransform.localScale = new Vector3(saveload.health / saveload.maxhealth, 1, 1);
        ArmorBarUI.rectTransform.localScale = new Vector3(saveload.armor / saveload.maxarmor, 1, 1);
        FuelBarUI.rectTransform.localScale = new Vector3(saveload.fuel / saveload.maxfuel, 1, 1);
        MoneyUI.text = saveload.money.ToString();
    }

    public void GetChestThings(int health1,int armor1,int fuel1,int money1)
    {
        saveload.health += health1;
        saveload.armor += armor1;
        saveload.fuel += fuel1;
        saveload.money += money1;

        if (saveload.health > saveload.maxhealth)
            saveload.health = saveload.maxhealth;
        if (saveload.armor > saveload.maxarmor)
            saveload.armor = saveload.maxarmor;
        if (saveload.fuel > saveload.maxfuel)
            saveload.fuel = saveload.maxfuel;

        UpdateUI();
    }

    #endregion

}
