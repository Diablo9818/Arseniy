using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PauseButtonUI : MonoBehaviour
{
    [SerializeField] private bool isPauseActive = false;
    [SerializeField] private GameObject pauseObject;
    public void Pause()
    {
        if (!isPauseActive) {
            pauseObject.SetActive(true);
            isPauseActive = true;
            Time.timeScale = 0;
        } else {
            pauseObject.SetActive(false);
            isPauseActive = false;
            Time.timeScale = 1;
        }
    }
}
