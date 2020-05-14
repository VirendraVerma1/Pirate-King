using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipController : MonoBehaviour
{
    //Ship Stats Variable
    private int health;
    private int armor;
    private int speed;

    [SerializeField]
    private LineRenderer line;
    
    [Header("Ship Cannons")]
    public GameObject[] ShipCannonsGo;
    public GameObject CannonBallGameObject;

    public float movementSpeed = 10;
    public float acceleration = 2;
    public float time = 0.05f;
    // Start is called before the first frame update
    void Start()
    {
        //line.positionCount = 0;

        //line = GetComponentInChildren<LineRenderer>();
    }

    #region ShipMovement

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        if (movement != Vector3.zero) transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement.normalized), time);


        transform.Translate(movement * movementSpeed * acceleration * Time.deltaTime, Space.World);

        
    }


    #endregion

    #region Ship Fire
    

    public void OnFireButtonDown()
    {
        //linerender direction on hold button
        
        
    }

    public void OnFireButtonUp()
    {
        //stop linerendrer and shoot
        foreach (GameObject g in ShipCannonsGo)
        {
            GameObject go = Instantiate(CannonBallGameObject, g.transform.position+new Vector3(0,0.45f,0), g.transform.rotation);
            go.GetComponent<CannonBall>().cannonDamage = 10;
            go.transform.localScale = new Vector3(0.4f,0.4f,0.4f);
            go.transform.GetComponent<Rigidbody>().AddForce(-go.transform.forward*15, ForceMode.Impulse);
            
            
        }
    }

    #endregion

}
