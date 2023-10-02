using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    private const string POPUP_PARAMETR_NAME = "IsGameOver";
    [SerializeField] private GameObject _panel;
    [SerializeField] private Animator _panelAnimator;
    [SerializeField] private Button _restartButton;

    public void Start()
    {
        HideScreen();

        _restartButton.onClick.AddListener(HideScreen);
    }

    public void ShowScreen()
    {
        _panel.SetActive(true);
        _panelAnimator.SetBool(POPUP_PARAMETR_NAME, true);
    }

    public void HideScreen()
    {
        _panel.SetActive(false);
        _panelAnimator.SetBool(POPUP_PARAMETR_NAME, false);
    }
}
