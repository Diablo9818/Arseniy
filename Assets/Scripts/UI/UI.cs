using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    [SerializeField] private bool isPauseActive = false;
    [SerializeField] private GameObject pauseObject;

    private CurrencyManager _currencyManager;

    private void Start()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();

    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);

        AppMetrica.Instance.ReportEvent("exit_to_menu_button_click");
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void QuitGame()
    {
        Application.Quit();

        AppMetrica.Instance.ReportEvent("application_exit_button_click");
        AppMetrica.Instance.SendEventsBuffer();
    }
    public void StartGame()
    {
        if (_currencyManager.UseEnergy())
        {
            SceneManager.LoadScene(1);

            AppMetrica.Instance.ReportEvent("start_game_button_click");
            AppMetrica.Instance.SendEventsBuffer();
        }
    }
    public void RestartGame()
    {
        if (_currencyManager.UseEnergy())
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            AppMetrica.Instance.ReportEvent("restart_game_button_click");
            AppMetrica.Instance.SendEventsBuffer();
        }
    }


    public void OpenPauseMenu()
    {
        if (!isPauseActive)
        {
            pauseObject.SetActive(true);
            isPauseActive = true;
            Time.timeScale = 0;

            AppMetrica.Instance.PauseSession();
        }
        else
        {
            pauseObject.SetActive(false);
            isPauseActive = false;
            Time.timeScale = 1;

            AppMetrica.Instance.ResumeSession();
        }
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void SettingsMenuMetric()
    {
        AppMetrica.Instance.ReportEvent("open_settings_button_click");
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void ShopMetric()
    {
        AppMetrica.Instance.ReportEvent("open_shop_button_click");
        AppMetrica.Instance.SendEventsBuffer();
    }
}
