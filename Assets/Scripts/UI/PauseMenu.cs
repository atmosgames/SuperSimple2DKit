using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Disables the cursor, freezes timeScale and contains functions that the pause menu button can use*/ 

public class PauseMenu : MonoBehaviour
{
    [SerializeField] AudioClip pressSound;
    [SerializeField] AudioClip openSound;
    [SerializeField] GameObject pauseMenu;

    private Controls controls;

    private void Awake() => controls = new Controls();

    void OnEnable()
    {
        controls.Enable();
        Cursor.visible = true;
        GameManager.Instance.audioSource.PlayOneShot(openSound);
        Time.timeScale = 0f;

        controls.UI.Escape.performed += _ => Unpause();
    }

    void OnDisable()
    {
        controls.Disable();

        controls.UI.Escape.performed -= _ => Unpause();
    }

    public void Unpause()
    {
        Cursor.visible = false;
        gameObject.SetActive(false);
        GameManager.Instance.audioSource.PlayOneShot(openSound);
        Time.timeScale = 1f;
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
