using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private Button fireGunAbilityBuy;
    [SerializeField] private TextMeshProUGUI fireGunAbilityBuyText;
    [SerializeField] private Button crossbowAbilityBuy;
    [SerializeField] private TextMeshProUGUI crossbowAbilityBuyText;
    [SerializeField] private Button mortarAbilityBuy;
    [SerializeField] private TextMeshProUGUI mortarAbilityBuyText;

    [SerializeField] private GameObject firegunBuyAbilityUI;
    [SerializeField] private GameObject mortarBuyAbilityUI;
    [SerializeField] private GameObject crossbowBuyAbilityUI;

    [SerializeField] private GameObject firegunUpgradeAbilityUI;
    [SerializeField] private GameObject mortarUpgradeAbilityUI;
    [SerializeField] private GameObject crossbowUpgradeAbilityUI;

    public event EventHandler OnFiregunAbilityBought;
    public event EventHandler OnCrossbowAbilityBought;
    public event EventHandler OnMortarAbilityBought;

    [Header("----------PRICES----------")]
    [SerializeField] private int mortarAbilityPrice;
    [SerializeField] private int firegunAbilityPrice;
    [SerializeField] private int crossbowAbilityPrice;
    [Space(5)]
    [SerializeField] private int firegunDamagePrice;
    [Space(5)]
    [SerializeField] private int firegunDotDurationPrice;
    [Space(5)]
    [SerializeField] private int firegunDotDamagePrice;
    [Space(10)]
    [SerializeField] private int crossbowDamagePrice;
    [Space(10)]
    [SerializeField] private int mortarDamagePrice;


    [Header("----------FIREGUN----------")]
    [SerializeField] private FireGun firegun;
    [SerializeField] private Button upgradeFiregunDamageButton;
    [SerializeField] private TextMeshProUGUI upgradeFiregunDamageText;
    [SerializeField] private TextMeshProUGUI upgradeFiregunDamagePriceText;
    [SerializeField] private Button upgradeDotDamageButton;
    [SerializeField] private TextMeshProUGUI upgradeDotDamageText;
    [SerializeField] private TextMeshProUGUI upgradeDotDamagePriceText;
    [SerializeField] private Button upgradeDotDurationButton;
    [SerializeField] private TextMeshProUGUI upgradeDotDurationText;
    [SerializeField] private TextMeshProUGUI upgradeDotDurationPriceText;

    [Header("----------CROSSBOW----------")]
    [SerializeField] private Crossbow crossbow;
    [SerializeField] private Button upgradeCrossbowDamageButton;
    [SerializeField] private TextMeshProUGUI upgradeCrossbowDamageText;
    [SerializeField] private TextMeshProUGUI upgradeCrossbowDamagePriceText;

    [Header("----------MORTAR----------")]
    [SerializeField] private Mortar mortar;
    [SerializeField] private Button upgradeMortarDamageButton;
    [SerializeField] private TextMeshProUGUI upgradeMortarDamageText;
    [SerializeField] private TextMeshProUGUI upgradeMortarDamagePriceText;


<<<<<<< Updated upstream:Arseniy/Assets/Scripts/UI/ShopUI.cs
=======
    private void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();

        mortar.SetDamageLevel(upgradesMailman.MortarDamageLevel);
        crossbow.SetDamageLevel(upgradesMailman.CrossbowDamageLevel);
        firegun.SetUpgrades(upgradesMailman.FlamethrowerDamageLevel);

        UpdateAllButtons();
    }
>>>>>>> Stashed changes:Assets/Scripts/UI/ShopUI.cs

    private void OnEnable()
    {
        UpdateCoinsAmount();
    }

    private void Start()
    {
        fireGunAbilityBuyText.text = $"{firegunAbilityPrice}";
        crossbowAbilityBuyText.text = $"{crossbowAbilityPrice}";
        mortarAbilityBuyText.text = $"{mortarAbilityPrice}";

        fireGunAbilityBuy.onClick.AddListener(() => {
            TryToBuyAbility(Player.Weapon.FireGun);
        });
        mortarAbilityBuy.onClick.AddListener(() => {
            TryToBuyAbility(Player.Weapon.Mortar);
        });
        crossbowAbilityBuy.onClick.AddListener(() => {
            TryToBuyAbility(Player.Weapon.Crossbow);
        });

<<<<<<< Updated upstream:Arseniy/Assets/Scripts/UI/ShopUI.cs
        upgradeFiregunDamageButton.onClick.AddListener(() => {
            if(Player.Instance.GetCoins() >= firegunDamagePrice) {
                Player.Instance.SpendCoins(firegunDamagePrice);
                firegun.UpgradeDamageLevel();
            } else {
                Debug.Log("Not enough money");
            }
        });

        upgradeDotDurationButton.onClick.AddListener(() => {
            if (Player.Instance.GetCoins() >= firegunDotDurationPrice) {
                Player.Instance.SpendCoins(firegunDotDurationPrice);
                firegun.UpgradeDotDurationLevel();
            } else {
                Debug.Log("Not enough money");
            }
        });

        upgradeDotDamageButton.onClick.AddListener(() => {
            if (Player.Instance.GetCoins() >= firegunDotDamagePrice) {
                Player.Instance.SpendCoins(firegunDotDamagePrice);
                firegun.UpgradeDotDamageLevel();
            } else {
=======
        upgradeFiregunDamageButton.onClick.AddListener(() =>
        {
            if (_currencyManager.GetCoins() >= firegunDamagePrice)
            {
                _currencyManager.SpendCoins(firegunDamagePrice);
                firegunDamagePrice += firegun.GetCurrentDamageLevel() * 2;
                firegun.UpgradeDamage();
            }
            else
            {
>>>>>>> Stashed changes:Assets/Scripts/UI/ShopUI.cs
                Debug.Log("Not enough money");
            }
        });

<<<<<<< Updated upstream:Arseniy/Assets/Scripts/UI/ShopUI.cs
        upgradeCrossbowDamageButton.onClick.AddListener(() => {
            if (Player.Instance.GetCoins() >= crossbowDamagePrice) {
                Player.Instance.SpendCoins(crossbowDamagePrice);
=======
        upgradeCrossbowDamageButton.onClick.AddListener(() =>
        {
            if (_currencyManager.GetCoins() >= crossbowDamagePrice)
            {
                _currencyManager.SpendCoins(crossbowDamagePrice);
                crossbowDamagePrice += crossbow.GetCurrentDamageLevel() * 2;
>>>>>>> Stashed changes:Assets/Scripts/UI/ShopUI.cs
                crossbow.UpgradeDamageLevel();
            } else {
                Debug.Log("Not enough money");
            }
        });

<<<<<<< Updated upstream:Arseniy/Assets/Scripts/UI/ShopUI.cs
        upgradeMortarDamageButton.onClick.AddListener(() => {
            if (Player.Instance.GetCoins() >= mortarDamagePrice) {
                Player.Instance.SpendCoins(mortarDamagePrice);
=======
        upgradeMortarDamageButton.onClick.AddListener(() =>
        {
            if (_currencyManager.GetCoins() >= mortarDamagePrice)
            {
                _currencyManager.SpendCoins(mortarDamagePrice);
                mortarDamagePrice += mortar.GetCurrentDamageLevel() * 2;
>>>>>>> Stashed changes:Assets/Scripts/UI/ShopUI.cs
                mortar.UpgradeDamageLevel();
            } else {
                Debug.Log("Not enough money");
            }
        });

    }

    private void Update()
    {
        UpdateAllButtons();
        UpdateCoinsAmount();
    }

    private void UpdateCoinsAmount()
    {
        coinsText.text = $"Монет: {Player.Instance.GetCoins()}";
    }

    private void TryToBuyAbility(Player.Weapon weapon)
    {
        switch (weapon) {
            case Player.Weapon.FireGun:
                if(Player.Instance.GetCoins() >= firegunAbilityPrice) {
                    OnFiregunAbilityBought?.Invoke(this, EventArgs.Empty);
                    Player.Instance.SpendCoins(firegunAbilityPrice);

                    //Show(firegunUpgradeAbilityUI);
                    Hide(fireGunAbilityBuy.gameObject);
                } else {
                    Debug.Log("No enough money");
                }
                break;
            case Player.Weapon.Mortar:
                if (Player.Instance.GetCoins() >= mortarAbilityPrice) {
                    OnMortarAbilityBought?.Invoke(this, EventArgs.Empty);
                    Player.Instance.SpendCoins(mortarAbilityPrice);

                    //Show(mortarUpgradeAbilityUI);
                    Hide(mortarAbilityBuy.gameObject);
                } else {
                    Debug.Log("No enough money");
                }
                break;
            case Player.Weapon.Crossbow:
                if (Player.Instance.GetCoins() >= crossbowAbilityPrice) {
                    OnCrossbowAbilityBought?.Invoke(this, EventArgs.Empty);
                    Player.Instance.SpendCoins(crossbowAbilityPrice);

                    //Show(crossbowUpgradeAbilityUI);
                    Hide(crossbowAbilityBuy.gameObject);
                } else {
                    Debug.Log("No enough money");
                }
                break;
        }
    }


    private void Show(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    private void Hide(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private void UpdateAllButtons()
    {
        upgradeFiregunDamageText.text = $"Базовая атака: {firegun.GetCurrentDamage()}";
        upgradeFiregunDamagePriceText.text = $"{firegunDamagePrice}";

        upgradeDotDurationText.text = $"Длительность горения: {firegun.GetCurrentDotDuration()}";
        upgradeDotDurationPriceText.text = $"{firegunDotDurationPrice}";

        upgradeDotDamageText.text = $"Урон горения: {firegun.GetCurrentDotDamage()}";
        upgradeDotDamagePriceText.text = $"{firegunDotDamagePrice}";

        upgradeCrossbowDamageText.text = $"Базовая атака: {crossbow.GetCurrentDamage()}";
        upgradeCrossbowDamagePriceText.text = $"{crossbowDamagePrice}";

        upgradeMortarDamageText.text = $"Базовая атака: {mortar.GetCurrentDamage()}";
        upgradeMortarDamagePriceText.text = $"{mortarDamagePrice}";
<<<<<<< Updated upstream:Arseniy/Assets/Scripts/UI/ShopUI.cs


        switch (firegun.GetCurrentDamageLevel()) {
            case FireGun.DamageLevel.Level1:
                firegunDamagePrice = firegunDamageLevel2;
                break;
            case FireGun.DamageLevel.Level2:
                firegunDamagePrice = firegunDamageLevel3;
                break;
            case FireGun.DamageLevel.Level3:
                firegunDamagePrice = firegunDamageLevel4;
                break;
            case FireGun.DamageLevel.Level4:
                upgradeFiregunDamageButton.gameObject.SetActive(false);
                //firegunDamagePrice = firegunDamageLevel4;
                break;
        }

        switch (firegun.GetCurrentDotDamageLevel()) {
            case FireGun.DotDamageLevel.Level1:
                firegunDotDamagePrice = firegunDotDamageLevel2;
                break;
            case FireGun.DotDamageLevel.Level2:
                firegunDotDamagePrice = firegunDotDamageLevel3;
                break;
            case FireGun.DotDamageLevel.Level3:
                firegunDotDamagePrice = firegunDotDamageLevel4;
                break;
            case FireGun.DotDamageLevel.Level4:
                upgradeDotDamageButton.gameObject.SetActive(false);
                //firegunDotDamagePrice = firegunDotDamageLevel4;
                break;
        }
        switch (firegun.GetCurrentDotDurationLevel()) {
            case FireGun.DotDurationLevel.Level1:
                firegunDotDurationPrice = firegunDotDurationLevel2;
                break;
            case FireGun.DotDurationLevel.Level2:
                firegunDotDurationPrice = firegunDotDurationLevel3;
                break;
            case FireGun.DotDurationLevel.Level3:
                firegunDotDurationPrice = firegunDotDurationLevel4;
                break;
            case FireGun.DotDurationLevel.Level4:
                upgradeDotDurationButton.gameObject.SetActive(false);
                //firegunDotDurationPrice = firegunDotDurationLevel4;
                break;
        }

        switch (crossbow.GetCurrentDamageLevel()) {
            case Crossbow.DamageLevel.Level1:
                crossbowDamagePrice = crossbowDamageLevel2;
                break;
            case Crossbow.DamageLevel.Level2:
                crossbowDamagePrice = crossbowDamageLevel3;
                break;
            case Crossbow.DamageLevel.Level3:
                crossbowDamagePrice = crossbowDamageLevel4;
                break;
            case Crossbow.DamageLevel.Level4:
                upgradeCrossbowDamageButton.gameObject.SetActive(false);
                //crossbowDamagePrice = crossbowDamageLevel4;
                break;
        }
        switch (mortar.GetCurrentDamageLevel()) {
            case Mortar.DamageLevel.Level1:
                mortarDamagePrice = mortarDamageLevel2;
                break;
            case Mortar.DamageLevel.Level2:
                mortarDamagePrice = mortarDamageLevel3;
                break;
            case Mortar.DamageLevel.Level3:
                mortarDamagePrice = mortarDamageLevel4;
                break;
            case Mortar.DamageLevel.Level4:
                upgradeMortarDamageButton.gameObject.SetActive(false);
                //mortarDamagePrice = mortarDamageLevel4;
                break;
        }
=======
    }

    private void OnDisable()
    {
        upgradesMailman.CrossbowDamageLevel = crossbow.GetCurrentDamageLevel();
        upgradesMailman.MortarDamageLevel = mortar.GetCurrentDamageLevel();
        upgradesMailman.FlamethrowerDamageLevel = firegun.GetCurrentDamageLevel();
>>>>>>> Stashed changes:Assets/Scripts/UI/ShopUI.cs
    }
}
