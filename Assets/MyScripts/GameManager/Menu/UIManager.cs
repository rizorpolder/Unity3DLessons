﻿using CollabProxy.UI;
using UnityEngine;


public class UIManager: Singleton<UIManager>
{
    [SerializeField] private MainMenu _mainMenu;

    [SerializeField]private PauseMenu _pauseMenu;

    [SerializeField] private Camera _dummyCamera;

    public Events.EventFadeComplete OnMainMenuFadeComplete;

    private void Start()
    {
        _mainMenu.OnMainMenuFadeComplete.AddListener(HandleMainMenuFadeComplete);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleGameStateChanged);
    }

    void HandleMainMenuFadeComplete(bool fadeOut)
    {
        OnMainMenuFadeComplete.Invoke(fadeOut);
    }

    public void HandleGameStateChanged(GameManager.GameState currentSate, GameManager.GameState previousState)
    {
        _pauseMenu.gameObject.SetActive(currentSate==GameManager.GameState.PAUSED);
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.PREGAME) return; 

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameManager.Instance.StartGame();
        }
    }

    public void SetDummyCameraActive(bool active)
    {
        _dummyCamera.gameObject.SetActive(active);
    }

}

