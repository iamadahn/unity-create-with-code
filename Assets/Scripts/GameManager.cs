using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private bool isGameActive;
    public TextMeshProUGUI playerLivesText;
    public TextMeshProUGUI waveNumberText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI victoryText;
    public Button restartButton;
    public Button startButton;
    private SpawnManager spawnManager;
    private PlayerController playerController;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectsByType<PlayerController>().Length == 0)
        {
            GameOver();
        }

        waveNumberText.text = "Wave: " + spawnManager.WaveNumber();
        playerLivesText.text = "Lives: " + playerController.Lives();
    }

    public void StartGame()
    {
        startButton.gameObject.SetActive(false);
        titleText.gameObject.SetActive(false);
        playerLivesText.gameObject.SetActive(true);
        waveNumberText.gameObject.SetActive(true);
        isGameActive = true;
        spawnManager.StartSpawning();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }

    public bool IsGameActive()
    {
        return isGameActive;
    }

    public void Victory()
    {
        isGameActive = false;
        victoryText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
    }
}
