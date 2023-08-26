using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private Button openShopButton;
    [SerializeField] private ShopUI shopUI;

    [SerializeField] private Button crossBowAbility;
    [SerializeField] private Button firegunAbility;
    [SerializeField] private Button mortarAbility;

    private void Start()
    {
        Hide(shopUI.gameObject);
        Hide(crossBowAbility.gameObject);
        Hide(firegunAbility.gameObject);
        Hide(mortarAbility.gameObject);

        openShopButton.onClick.AddListener(() => {
            if (shopUI.gameObject.activeSelf) {
                Hide(shopUI.gameObject);
            } else {
                Show(shopUI.gameObject);
            }
        });


        shopUI.OnCrossbowAbilityBought += ShopUI_OnCrossbowAbilityBought;
        shopUI.OnFiregunAbilityBought += ShopUI_OnFiregunAbilityBought;
        shopUI.OnMortarAbilityBought += ShopUI_OnMortarAbilityBought;
    }

    private void ShopUI_OnMortarAbilityBought(object sender, System.EventArgs e)
    {
        Show(mortarAbility.gameObject);
    }

    private void ShopUI_OnFiregunAbilityBought(object sender, System.EventArgs e)
    {
        Show(firegunAbility.gameObject);
    }

    private void ShopUI_OnCrossbowAbilityBought(object sender, System.EventArgs e)
    {
        Show(crossBowAbility.gameObject);
    }

    private void Show(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    private void Hide(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }
}
