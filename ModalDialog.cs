using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ModalDialog : MonoBehaviour {

    [SerializeField] private GameObject pausePanel;

    void Start()
    {
        pausePanel.SetActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!pausePanel.activeSelf)
            {
               PauseGame();
            }
            else
            {
                ContinueGame();
            }
        }
    }
    private void PauseGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        //enable the scripts again
    }

    public void retry()
    {
        SceneManager.LoadScene("Mainscene");
        ContinueGame();
    }

}
    