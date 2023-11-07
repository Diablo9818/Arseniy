using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FiregunAbilityButton : MonoBehaviour
{
    [SerializeField] private Button abilityButton;
    [SerializeField] private FireGun fireGun;
    private float abilityCooldown;
    private bool isCooldown;
    [SerializeField] TextMeshProUGUI cooldownText;

    private void Start()
    {
        FireGun.OnAbilityAction += FireGun_OnAbilityAction;
        abilityCooldown = fireGun.GetAbilityCooldown();
        HideCooldownText();
    }

    private void FireGun_OnAbilityAction(object sender, System.EventArgs e)
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
                abilityCooldown = fireGun.GetAbilityCooldown();
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
