using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Player;

public class Player : MonoBehaviour
{
    public enum Weapon { Mortar = 1, Crossbow = 0, FireGun = -1 };

    [SerializeField] public Weapon activeGun;

    public static Player Instance;



    static Weapon topGun;
    static Weapon midGun;
    static Weapon botGun;

     public static GameObject topGunObj;
     public static GameObject midGunObj;
     public static GameObject botGunObj;

    [SerializeField] private int coins;

    private bool hasFiregunAbility;
    private bool hasCrossbowAbility;
    private bool hasMortarAbility;

    private void Awake() //singleton
    {
        topGunObj = GameObject.Find("Mortar");
        midGunObj = GameObject.Find("Crossbow");
        botGunObj = GameObject.Find("FireGun");

        topGun = Weapon.Mortar;
        midGun = Weapon.Crossbow;
        botGun = Weapon.FireGun;

        activeGun = midGun;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        

        SwipeDetection.OnSwipeEvent += SwapWeapon;
        

        //Debug.Log(toMortar);
        //Debug.Log(toCrossbow);
        //Debug.Log(toThirdWeapon);

        DontDestroyOnLoad(gameObject);

        


    }

    private void Update()
    {
        activeGun = midGun;
        
    }

    public void CheckAndStopCoroutine(Coroutine coroutine)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private void SwapWeapon(int i)
    {
        if(i == 1) //SwipeUP
        {
            //Changing WEAPON
            Weapon bridge;
            bridge = topGun;
            topGun = midGun;
            midGun = botGun;
            botGun = bridge;

            //Changing OBJECTS' POSITION
            Vector3 bridgeObjPos;
            bridgeObjPos = topGunObj.transform.position;
            topGunObj.transform.position = botGunObj.transform.position;
            botGunObj.transform.position = midGunObj.transform.position;
            midGunObj.transform.position = bridgeObjPos;
            

            //Changing OBJECT
            GameObject bridgeObj;
            bridgeObj = topGunObj;
            topGunObj = midGunObj;
            midGunObj = botGunObj;
            botGunObj = bridgeObj;

        } else //SwipeDOWN
        {
            //Changing WEAPON
            Weapon bridge;
            bridge = botGun;
            botGun = midGun;
            midGun = topGun;
            topGun = bridge;

            //Changing OBJECTS' POSITION
            Vector3 bridgeObjPos;
            bridgeObjPos = botGunObj.transform.position;
            botGunObj.transform.position = topGunObj.transform.position;
            topGunObj.transform.position = midGunObj.transform.position;
            midGunObj.transform.position = bridgeObjPos;

            //Changing OBJECT
            GameObject bridgeObj;
            bridgeObj = botGunObj;
            botGunObj = midGunObj;
            midGunObj = topGunObj;
            topGunObj = bridgeObj;

        }

        
    }

    public bool CheckAbility(Weapon weapon)
    {
        switch (weapon) {
            case Weapon.FireGun:
                return hasFiregunAbility;
            case Weapon.Crossbow:
                return hasCrossbowAbility;
            case Weapon.Mortar:
                return hasMortarAbility;
            default: 
                return false;
        }
    }

    public void ActivateAbility(Weapon weapon)
    {
        switch (weapon) {
            case Weapon.FireGun:
                hasFiregunAbility = true;
                break;
            case Weapon.Crossbow:
                hasCrossbowAbility = true;
                break;
            case Weapon.Mortar:
                hasMortarAbility = true;
                break;
        }
    }

    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
    } 

    public void SpendCoins(int spentCoins)
    {
        coins -= spentCoins;
    }

    public int GetCoins()
    {
        return coins;
    }

}
