using System.Collections;
using System.Threading;
using System;
using UnityEngine;
using TMPro;

public class EnergyManager : MonoBehaviour
{
    [SerializeField] public float MinsForEnergy;
    [SerializeField] public int EnergyPerTick;
    [SerializeField] public int MaxEnergy;
    [SerializeField] public int EnergyForRun;
    public int CurrEnergy;


    public static EnergyManager Instance;

    private Coroutine myCoroutine;

    private void Awake()
    {
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
            if (CurrEnergy >= MaxEnergy)
            {
                CurrEnergy = MaxEnergy;
            }
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
            EnergyTextUpdater.Instance.UpdateText();
        }
    }

    public int GetEnergy()
    {
        return CurrEnergy;
    }

    public bool UseEnergy()
    {
        if (CurrEnergy >= EnergyForRun)
        {
            CurrEnergy -= EnergyForRun;
            EnergyTextUpdater.Instance.UpdateText();
            return true;
        } else
        {
            return false;
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
                if (CurrEnergy >= MaxEnergy)
                {
                    CurrEnergy = MaxEnergy;
                }
                EnergyTextUpdater.Instance.UpdateText();
            }
        }
    }

    private void OnApplicationQuit()
    {
            PlayerPrefs.SetString("AppExitTime", DateTime.Now.ToString());
            PlayerPrefs.SetInt("AppExitEnergy", CurrEnergy);
    }
}