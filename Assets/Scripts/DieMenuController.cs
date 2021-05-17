using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DieMenuController : MonoBehaviour
{
    public GameObject dieMenu;
    public GameObject player;
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        dieMenu.SetActive(false);
    }

    void FixedUpdate()
    {
        if (player.transform.position.y <= -1)
        {
            Time.timeScale = 0f;
            dieMenu.SetActive(true);
            if(PlayerController.highScore < 1500)
            {
                highScoreText.text = "High score: \n" + PlayerController.highScore.ToString("0.00") + "(s)";
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }
}