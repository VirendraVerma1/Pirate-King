using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;


public class BotShip : MonoBehaviour
{
    private float health;
    private float maxHealth;
    private float armor;
    private float maxArmor;
    private float speed;
    private float money;
    public float fuel;
    private float fuelTank;

    public NavMeshAgent agent;
    public GameObject[] ShipCannonsGo;
    public GameObject CannonBallGameObject;
    public Transform SideView;

    
    private float Cannontimer = 0;
    private float Cannoncounter = 2;
    private float FollowTimer = 0;
    private float FollowCounter = 1000;

    private bool islowFuel = false;
    void Start()
    {
        health = 100;
        maxHealth = 100;
        armor = 100;
        maxArmor = 100;
        speed = 2;
        money = 100;
        fuel = 1000;
        fuelTank = 1000;

        Cannontimer = 2;
        Cannoncounter = 1;
        FollowTimer = 0;
        FollowCounter = 1000;

        islowFuel = false;

        FuelStatonChecker();
        StartCoroutine(ReduceFuel());
        StartCoroutine(UpdateEffect());
    }

    IEnumerator ReduceFuel()
    {
        int n = 1;
        while (n>0)
        {
            yield return new WaitForSeconds(1);
            //print(fuel);
            fuel -= 1;
            if (fuel < 200)
            {
                //low fuel
                //go to the fuelStaton
                islowFuel = true;
                
                float distance = Vector3.Distance(gameObject.transform.position, NearestFuelStation.transform.position);
                print(distance);
                if (distance > 10)
                {
                    agent.enabled = true;
                    SetPathOfBot(NearestFuelStation.transform.position);
                }
                else
                {
                    agent.enabled = false;
                }
            }
            else if (fuel > 900)
            {
                //high fuel
                
                islowFuel = false;
            }
            
        }
    }

    
    void Update()
    {
        RefreshThings();

        if (!islowFuel)
        {
            CheckIfEnemyClose();


            //if following for a long time
            FollowTimer -= Time.deltaTime;
            if (FollowTimer < 0 && NearestDistance > 30)
            {
                FollowTimer = FollowCounter;
                //Change enemy
                GetRandomEnemy();
            }

            //Cannon Shoot
            Cannontimer -= Time.deltaTime;
            if (CheckEnemyInfrontOfCannon() && fireCannon)
            {
                inFrontOfCannon = true;
            }
            else
            {
                inFrontOfCannon = false;
            }
            if (Cannontimer < 0 && fireCannon == true && CheckEnemyInfrontOfCannon())
            {
                Cannontimer = Cannoncounter;
                FireCannon();
            }
            
        }
        UpdateUI();
    }

    #region Get and Check Nearest Fuel Staton

    private GameObject[] FuelStaton;
    private float[] distanceFromAllFuelStation;
    private GameObject NearestFuelStation;

    void FuelStatonChecker()
    {
        GetAllFuelStatons();
        CheckFuelStatonDistance();
        GetSortestDistanceFuelStation();
    }

    void GetAllFuelStatons()
    {
        FuelStaton = GameObject.FindGameObjectsWithTag("FuelStaton");

    }

    void CheckFuelStatonDistance()
    {
        distanceFromAllFuelStation = new float[FuelStaton.Length];
        for (int i = 0; i < FuelStaton.Length; i++)
        {
            distanceFromAllFuelStation[i] = 999999;
        }

        for (int i = 0; i < FuelStaton.Length; i++)
        {
            distanceFromAllFuelStation[i] = Vector3.Distance(gameObject.transform.position, FuelStaton[i].transform.position);
        }
    }

    void GetSortestDistanceFuelStation()
    {
        float minDistance = 99999;

        for (int i = 0; i < FuelStaton.Length; i++)
        {

            if (minDistance > distanceFromAllFuelStation[i])
            {
                minDistance = distanceFromAllFuelStation[i];
                NearestFuelStation = FuelStaton[i];
            }

        }
    }

    #endregion

    #region Check And Get Enemy

    
    private GameObject[] Enemies;
    private GameObject[] AllEnemies;
    private float[] distanceFromAll;
    private GameObject NearestEnemey;
    private float NearestDistance = 100;

    void RefreshThings()
    {
        GetAllEnemy();
        CheckEnemyDistance();
        GetSortestDistanceEnemy();
        //SetPathOfBot();
    }

    void GetAllEnemy()
    {
        AllEnemies = GameObject.FindGameObjectsWithTag("Ship");
        GameObject me=GameObject.FindGameObjectWithTag("MyShip");

        Enemies = new GameObject[AllEnemies.Length + 1];
        int d = 0;
        for (int i = 0; i < AllEnemies.Length; i++)
        {
            Enemies[d] = AllEnemies[i];
            d++;
        }
        Enemies[d] = me;
    }

    void CheckEnemyDistance()
    {
        distanceFromAll=new float[Enemies.Length+1];
        for (int i = 0; i < Enemies.Length; i++)
        {
            distanceFromAll[i] = 999999;
        }

        for (int i = 0; i < Enemies.Length; i++)
        {
            distanceFromAll[i] = Vector3.Distance(gameObject.transform.position,Enemies[i].transform.position);
        }
        
    }

    void GetSortestDistanceEnemy()
    {
        float minDistance = 99999;
         
        for (int i = 0; i < Enemies.Length; i++)
        {

            if (minDistance > distanceFromAll[i] && Enemies[i]!=gameObject)
            {
                minDistance = distanceFromAll[i];
                NearestEnemey = Enemies[i];
            }
            
        }
    }


    

    #endregion

    #region When Close to Enemy
    bool inFrontOfCannon = false;
    bool fireCannon = false;
    void CheckIfEnemyClose()
    {
        float distance = Vector3.Distance(gameObject.transform.position, NearestEnemey.transform.position);
        NearestDistance = distance;
        if (NearestEnemey == null)
        {
            GetRandomEnemy();
        }
        
        if (distance < 15)
        {

            agent.enabled = false;
            //print(distance);
            //transform.LookAt(NearestEnemey.transform.position);
            
            if (!inFrontOfCannon)
            {
                transform.Rotate(Vector3.one * 15 * Time.deltaTime);
                //float rot = transform.rotation.y+90;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(new Vector3(0, rot, 0)), Time.deltaTime * 0.7f);
                //Vector3 currentRotation = gameObject.transform.rotation;
                //Vector3 wantedRotation = currentRotation * Quaternion.AngleAxis(-90, Vector3.up);
                //transform.rotation = Quaternion.Slerp(currentRotation, wantedRotation, Time.deltaTime * 2);
                //Vector3 relativePos = NearestEnemey.transform.position;
                //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(relativePos.normalized), 0.02f);
            }
            fireCannon = true;
        }
        else if (distance < 100)
        {
            fireCannon = false;
            //find next enemy
            agent.enabled = true;
            SetPathOfBot(NearestEnemey.transform.position);
        }
        else
        {
            //random
            SetPathOfBot(Enemies[randomEnemy].transform.position);
        }
    }

    int randomEnemy;
    void GetRandomEnemy()
    {
        randomEnemy = Random.Range(1, Enemies.Length);
        if (Enemies[randomEnemy] == gameObject && Enemies.Length > 3)
        {
            GetRandomEnemy();
        }
        else if (Enemies.Length < 3)
        {
            SetPathOfBot(NearestEnemey.transform.position);
        }
        else
        {
            SetPathOfBot(Enemies[randomEnemy].transform.position);
        }
        
    }

    
    #endregion

    #region fire

    bool CheckEnemyInfrontOfCannon()
    {
        bool enemy = false;
        foreach (GameObject g in ShipCannonsGo)
        {
            RaycastHit hit;
            GameObject t = g.transform.Find("RayCast").gameObject;
            Vector3 fwd = t.transform.TransformDirection(Vector3.forward);
            if (Physics.Raycast(t.transform.position, fwd, out hit, 30))
            {
                if (hit.collider.tag == "Ship"||hit.collider.tag=="MyShip")
                {
                    enemy = true;
                }
            }
        }
        return enemy;
    }

    void FireCannon()
    {
        foreach (GameObject g in ShipCannonsGo)
        {
            GameObject go = Instantiate(CannonBallGameObject, g.transform.position + new Vector3(0, 0.45f, 0), g.transform.rotation);
            go.GetComponent<CannonBall>().cannonDamage = 10;
            go.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            go.transform.GetComponent<Rigidbody>().AddForce(-go.transform.forward * 15, ForceMode.Impulse);

        }
    }

    #endregion

    #region functions called by  others

    public void TakeDamage(float damage)
    {
        if (armor > damage)
            armor -= damage;
        else
        {
            armor = 0;
            health -= damage;
        }
        if (health < 0)
        {
            //sink TODO
            Destroy(gameObject);
        }
        else
        {
            ShowHealthUI();
        }
    }

    public void RechargeFuel(float no)
    {
        if (fuel < fuelTank)
        {
            fuel += no;
        }
        else
        {
            fuel = fuelTank;
        }
        ShowFuelUI();
    }


    public void GetChestThings(int health1, int armor1, int fuel1, int money1)
    {
        health += health1;
        armor += armor1;
        fuel += fuel1;
        saveload.money += money1;

        if (health > maxHealth)
            health = maxHealth;
        if (armor > maxArmor)
            armor = maxArmor;
        if (fuel > fuelTank)
            fuel = fuelTank;

        UpdateUI();
    }

    #endregion


    #region UI System

    [Header("Ship UI")]
    public GameObject Canvas;
    public Transform ViewPoint;
    public GameObject FuelPannel;
    public GameObject HealthPannel;
    public Image FuelImageBar;
    public Image HealthImageBar;

    

    void ShowHealthUI()
    {
        UpdateUI();
        HealthPannel.SetActive(true);
        StartCoroutine(UpdateEffect());
    }

    void ShowFuelUI()
    {
        UpdateUI();
        FuelPannel.SetActive(true);
        StartCoroutine(UpdateEffect());
    }

    void UpdateUI()
    {
        HealthPannel.transform.LookAt(Camera.main.transform);
        FuelPannel.transform.LookAt(Camera.main.transform);
        FuelImageBar.rectTransform.localScale = new Vector3(fuel / fuelTank, 1, 1);
        HealthImageBar.rectTransform.localScale = new Vector3(health / maxHealth, 1, 1);
    }

    IEnumerator UpdateEffect()
    {
        yield return new WaitForSeconds(2);
        HealthPannel.SetActive(false);
        FuelPannel.SetActive(false);
    }

    #endregion

    void SetPathOfBot(Vector3 Pos)
    {
        agent.SetDestination(Pos);
    }
}
//look at each other when close
//when low health then run
//when low fuel then run
//if enemy is powerfull have more hp then run