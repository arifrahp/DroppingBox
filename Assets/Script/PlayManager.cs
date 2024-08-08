using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    public int score;
    public float scoreInterval = 1f;
    public TMP_Text scoreText;

    void Start()
    {
        score = 0; // Initialize score
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

    public void GameOver()
    {
        
    }

    public int lifeCount;
    public TMP_Text lifeText;

    public void RedObjectCollide()
    {
        lifeCount -= 1;
    }

    public void GreenObjectCollied()
    {
        score += 5;
    }

    public void CoinObjectCollide()
    {
        lifeCount += 1;
    }
}
