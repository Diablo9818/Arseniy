using System.Collections;
using System.Threading;
using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] public float MinsForEnergy;
    [SerializeField] public int EnergyPerTick;
    [SerializeField] public int MaxEnergy;
    [SerializeField] public int EnergyForRun;
    public int CurrEnergy;
    [SerializeField] TMP_Text EnergyText;
    [SerializeField] TMP_Text timeText;

    public static EnergyManager Instance;

    private Coroutine myCoroutine;

    private void Awake()
    {
        EnergyText.text = CurrEnergy.ToString();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            StartMyCoroutine();
        }
        else
        {
            Destroy(gameObject);
        }

        string appExitTimeStr = PlayerPrefs.GetString("AppExitTime", "");
        CurrEnergy = PlayerPrefs.GetInt("AppExitEnergy");
        if (!string.IsNullOrEmpty(appExitTimeStr))
        {
            DateTime appExitTime = DateTime.Parse(appExitTimeStr);
            TimeSpan timeSpentOutside = DateTime.Now - appExitTime;
            CurrEnergy += (int)((int)timeSpentOutside.TotalMinutes / MinsForEnergy * EnergyPerTick);
            timeText.text = "тайм - " + timeSpentOutside.TotalSeconds.ToString() + " | энерджи - " + (int)(timeSpentOutside.TotalSeconds / (MinsForEnergy * 60)) * EnergyPerTick;
            if (CurrEnergy >= MaxEnergy)
            {
                CurrEnergy = MaxEnergy;
            }
            EnergyText.text = CurrEnergy.ToString();
        }
    }

    public void StartMyCoroutine()
    {
        myCoroutine = StartCoroutine(MyCoroutine());
    }

    public void StopMyCoroutine()
    {
        if (myCoroutine != null)
        {
            StopCoroutine(myCoroutine);
            myCoroutine = null;
        }
    }

    IEnumerator MyCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(MinsForEnergy * 60);
            if (CurrEnergy + EnergyPerTick <= MaxEnergy)
            {
                CurrEnergy += EnergyPerTick;
            }
            else
            {
                CurrEnergy = MaxEnergy;
            }
            EnergyText.text = CurrEnergy.ToString();
        }
    }

    public void UseEnergy()
    {
        if (CurrEnergy >= EnergyForRun)
        {
            CurrEnergy -= EnergyForRun;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetString("AppExitTime", DateTime.Now.ToString());
            PlayerPrefs.SetInt("AppExitEnergy", CurrEnergy);
        }
        else
        {
            string appExitTimeStr = PlayerPrefs.GetString("AppExitTime", "");
            CurrEnergy = PlayerPrefs.GetInt("AppExitEnergy");
            if (!string.IsNullOrEmpty(appExitTimeStr))
            {
                DateTime appExitTime = DateTime.Parse(appExitTimeStr);
                TimeSpan timeSpentOutside = DateTime.Now - appExitTime;
                CurrEnergy += (int)(timeSpentOutside.TotalSeconds / (MinsForEnergy * 60)) * EnergyPerTick;
                timeText.text = "тайм - " + timeSpentOutside.TotalSeconds.ToString() + " | энерджи - " + (int)(timeSpentOutside.TotalSeconds / (MinsForEnergy * 60)) * EnergyPerTick;
                if (CurrEnergy >= MaxEnergy)
                {
                    CurrEnergy = MaxEnergy;
                }
                EnergyText.text = CurrEnergy.ToString();
            }
        }
    }

    private void OnApplicationQuit()
    {
            PlayerPrefs.SetString("AppExitTime", DateTime.Now.ToString());
            PlayerPrefs.SetInt("AppExitEnergy", CurrEnergy);
    }
}