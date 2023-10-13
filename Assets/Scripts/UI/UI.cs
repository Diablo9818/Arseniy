using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private Button crossBowAbility;
    [SerializeField] private Button firegunAbility;
    [SerializeField] private Button mortarAbility;

    [SerializeField] private bool isPauseActive = false;
    [SerializeField] private GameObject pauseObject;

    private CurrencyManager _currencyManager;

    private void Start()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();
        Hide(crossBowAbility.gameObject);
        Hide(firegunAbility.gameObject);
        Hide(mortarAbility.gameObject);
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

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void StartGame()
    {
        if (_currencyManager.UseEnergy())
        {
            SceneManager.LoadScene(1);
        }
    }
    public void RestartGame()
    {
        if (_currencyManager.UseEnergy())
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }


    public void OpenPauseMenu()
    {
        if (!isPauseActive)
        {
            pauseObject.SetActive(true);
            isPauseActive = true;
            Time.timeScale = 0;
        }
        else
        {
            pauseObject.SetActive(false);
            isPauseActive = false;
            Time.timeScale = 1;
        }
    }
}
