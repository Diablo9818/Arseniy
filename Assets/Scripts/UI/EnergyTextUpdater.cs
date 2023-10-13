using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergyTextUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text EnergyText;
    public static EnergyTextUpdater Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }
    public void UpdateText()
    {
        EnergyText.text = CurrencyManager.Instance.GetEnergy().ToString();
    }
}
