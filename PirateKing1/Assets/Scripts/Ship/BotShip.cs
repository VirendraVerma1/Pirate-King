using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotShip : MonoBehaviour
{
    private float health;
    private float armor;
    private float speed;
    private float money;

    private GameObject[] Enemies;
    private float[] distanceFromAll;
    private GameObject NearestEnemey;
    
    void Start()
    {
        health = 100;
        GetAllEnemy();
        CheckEnemyDistance();
    }

    
    void Update()
    {
        
    }

    void CheckEnemyDistance()
    {
        distanceFromAll=new float[Enemies.Length];
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
         
    }

    void GetAllEnemy()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Ship");
        
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health < 0)
        {
            //sink TODO
            Destroy(gameObject);
        }
    }
}
