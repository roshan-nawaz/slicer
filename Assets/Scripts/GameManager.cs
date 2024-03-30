using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnTime = 2.0f;
    public float score;
    public float highScore = 0;
    public bool isGameOver = false;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public TextMeshProUGUI gameOverText;
    public GameObject titleScreen;
    public Button restartButton;


    private IEnumerator SpawnTarget()
    {
        while (isGameOver == false)
        {
            yield return new WaitForSeconds(spawnTime);
            spawnTime  =Random.Range(1.0f, spawnTime);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    //updatign score
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score :" + score;
        //highScore = Mathf.Max(score, highScore);
    }

    public void UpdateHighScore()
    {
        highScore = Mathf.Max(highScore, highScore);
        highScoreText.text = "High Score :" + highScore;
    }

    //making game over visible
    public void GameOver()
    {
        isGameOver = true;
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        //Destroy(gameObject);
    }

    public void GameContinues()
    {
        isGameOver = false;
        //restartButton.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
    }

    //restartign game
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //starting game
    public void StartGame(int difficulty)
    {
        isGameOver = false;
        score = 0;
        spawnTime /= difficulty;
        if(score > 2)
        {
            spawnTime = (spawnTime / difficulty) / difficulty;
        }

        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        titleScreen.gameObject.SetActive(false);

    }

}
