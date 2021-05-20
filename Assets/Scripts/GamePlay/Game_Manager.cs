using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game_Manager : MonoBehaviour
{
    [Header("UI Ref's")]
    [SerializeField] private GameObject gameOverText;
    [SerializeField] private GameObject restartGameText;
    [SerializeField] private GameObject WinGameText;
    [SerializeField] private Text timer;

    private float _myTime;
    private bool isWin = false;

    [Header("Script Ref's")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private RIngScript ringScript;

    private void Awake()
    {
        gameOverText.SetActive(false);
        restartGameText.SetActive(false);
        WinGameText.SetActive(false);

    }

    private void Update()
    {
        timer.text = _myTime.ToString("f2");

        if(playerController.IsDead)
        {
            gameOverText.SetActive(true);
            restartGameText.SetActive(true);
            RestartGame();
        }
        else
        {
            _myTime += Time.deltaTime;
        }

        if (isWin)
            RestartGame();
    }

    #region Scene State
    private void RestartGame()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            Time.timeScale = 1;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    #endregion

    #region WinState

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if(ringScript.TookRing)
            {
                WinCondition();
            }
        }
    }

    private void WinCondition()
    {
        isWin = true;
        WinGameText.SetActive(true);
        restartGameText.SetActive(true);
        playerController.playerMovement._rb2d.gravityScale = 0;
        AudioManager_script.Instance.WinSound();

        Invoke("StopTime", 1);
    }

    private void StopTime() => Time.timeScale = 0;

    #endregion
}
