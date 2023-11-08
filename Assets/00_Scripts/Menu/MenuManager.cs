using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private PlayerInputHandler inputHandler;
    [SerializeField] private GameObject canvas;

    private void Update()
    {
        OnUIActive();
    }

    public void OnUIActive()
    {
        if (inputHandler.menuOpened)
        {
            canvas.SetActive(true);
        }
        else
            canvas.SetActive(false);
    }
    public void OnVolumeChange(UnityEngine.UI.Slider sliderInstance)
    {
        //GameManager.SetVolume(float slider value);
        print(sliderInstance.value);
    }

    public void OnSensitivityChange()
    {
        //GameManager.SetSensetivity(float slider value);
    }

    public void OnExitPressed()
    {
        Application.Quit();
    }
}
