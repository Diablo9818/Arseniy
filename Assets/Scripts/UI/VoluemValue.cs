using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoluemValue : MonoBehaviour
{
    private AudioSource audioScr;
    [SerializeField] private Slider SoundSlider;
    public void Start()
    {
        audioScr = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (SoundSlider.gameObject.activeSelf)
        {
            audioScr.volume = SoundSlider.value;
        }
    }
}
