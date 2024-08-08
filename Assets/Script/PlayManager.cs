using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public int score;
    public float scoreInterval = 1f;
    public TMP_Text scoreText;
    public TMP_Text scoreGameOverText;
    public int lifeCount;
    public TMP_Text lifeText;
    public int greenBoxCount;
    public TMP_Text greenBoxCounText;

    public GameObject gameOverPanel;
    public GameObject pausePanel;
    public GameObject inGamePanel;

    private ObjectSpawner objectSpawner;
    private MainBox mainBox;
    void Start()
    {
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
        inGamePanel.SetActive(true);

        score = 0;
        lifeCount = 1;
        greenBoxCount = 0;

        objectSpawner = FindAnyObjectByType<ObjectSpawner>();
        mainBox = FindAnyObjectByType<MainBox>();

        StartCoroutine(IncrementScoreEverySecond());
    }

    private IEnumerator IncrementScoreEverySecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(scoreInterval);
            IncrementScore();
        }
    }

    private void IncrementScore()
    {
        score++;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        lifeText.text = lifeCount.ToString();
        if (scoreText != null)
        {
            scoreText.text = score.ToString();
        }

        if(lifeCount < 1)
        {
            GameOver();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        inGamePanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        inGamePanel.SetActive(true);
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        scoreGameOverText.text = score.ToString();
        greenBoxCounText.text = greenBoxCount.ToString();
    }

    public void RedObjectCollide()
    {
        lifeCount -= 1;
        /*foreach(GameObject gameObject in objectSpawner.activeFallingObjects)
        {
            Destroy(gameObject);
        }
        mainBox.OnImune();*/
    }

    public void GreenObjectCollied()
    {
        score += 5;
        greenBoxCount += 1;
    }

    public void CoinObjectCollide()
    {
        lifeCount += 1;
    }
}
