using UnityEngine;

public class Player : MonoBehaviour
{
    public enum Weapon { Mortar = 1, Crossbow = 0, FireGun = -1 };

    [SerializeField] public Weapon activeGun;

    Weapon topGun;
    Weapon midGun;
    Weapon botGun;

    public GameObject topGunObj;
    public GameObject midGunObj;
    public GameObject botGunObj;

    private bool hasFiregunAbility;
    private bool hasCrossbowAbility;
    private bool hasMortarAbility;

    private void Awake() //singleton
    {
        topGunObj = FindObjectOfType<Mortar>().gameObject;
        midGunObj = FindObjectOfType<Crossbow>().gameObject;
        botGunObj = FindObjectOfType<FireGun>().gameObject;

        topGun = Weapon.Mortar;
        midGun = Weapon.Crossbow;
        botGun = Weapon.FireGun;

        activeGun = midGun;
    }

    private void Start()
    {
        SwipeDetection.OnSwipeEvent += SwapWeapon;

        //Debug.Log(toMortar);
        //Debug.Log(toCrossbow);
        //Debug.Log(toThirdWeapon);
    }

    private void Update()
    {
        activeGun = midGun;
    }

    public void CheckAndStopCoroutine(Coroutine coroutine)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private void SwapWeapon(int i)
    {
        if (i == 1) //SwipeUP
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

        }
        else //SwipeDOWN
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
        return weapon switch
        {
            Weapon.FireGun => hasFiregunAbility,
            Weapon.Crossbow => hasCrossbowAbility,
            Weapon.Mortar => hasMortarAbility,
            _ => false,
        };
    }

    public void ActivateAbility(Weapon weapon)
    {
        switch (weapon)
        {
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
}
