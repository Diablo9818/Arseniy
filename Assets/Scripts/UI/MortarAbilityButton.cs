using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MortarAbilityButton : MonoBehaviour
{
    [SerializeField] private Button abilityButton;
    [SerializeField] private Mortar mortar;
    private float abilityCooldown;
    private bool isCooldown;
    [SerializeField] TextMeshProUGUI cooldownText;

    private void Start()
    {
        mortar.OnAbilityAction += Mortar_OnAbilityAction;
        abilityCooldown = mortar.GetAbilityCooldown();
        HideCooldownText();
    }

    private void Mortar_OnAbilityAction(object sender, System.EventArgs e)
    {
        isCooldown = true;
    }

    private void Update()
    {
        if (isCooldown) {
            abilityCooldown -= Time.deltaTime;
            if (abilityCooldown <= 0) {
                HideCooldownText();
                abilityButton.interactable = true;
                abilityCooldown = mortar.GetAbilityCooldown();
                isCooldown = false;
            } else {
                abilityButton.interactable = false;
                ShowCooldownText();
                cooldownText.text = Mathf.Ceil(abilityCooldown).ToString();
            }
        }
        
    }

    public void ShowCooldownText()
    {
        cooldownText.gameObject.SetActive(true);
    }

    public void HideCooldownText()
    {
        cooldownText.gameObject.SetActive(false);
    }

    public bool IsCooldown()
    {
        return isCooldown;
    }
}
