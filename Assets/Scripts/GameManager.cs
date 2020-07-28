using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    public float spawnRate;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI timer;
    public Button restart;
    public int score;
    public int lives;
    public bool isGameActive; //controlling gamestate
    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame(int difficulty)
    {
        spawnRate /= difficulty;
        isGameActive = true;
        //timer.gameObject.SetActive(true);
        //StartCoroutine(Timer());
        StartCoroutine(SpawnTarget());
        titleScreen.SetActive(false);
    }

    IEnumerator SpawnTarget()
    {
        //yield return new WaitForSeconds(3);
        //timer.gameObject.SetActive(false);
        while (isGameActive)
        {
            yield return new WaitForSeconds(GenerateRandomTimeRate());
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    IEnumerator Timer()
    {
        for(int i = 3; i>0; i--)
        {
            timer.text = "Starting in "+i;
            yield return new WaitForSeconds(1);
        }
    }

    float GenerateRandomTimeRate()
    {
        return spawnRate = Random.Range(0, 3f);
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;

    }

    public void UpdateLives(int livesToDecrease)
    {
         lives -= livesToDecrease;
        Debug.Log("Not destroyed" + lives);

    }

    public void GameOver()
    {
        restart.gameObject.SetActive(true);
        isGameActive = false;
        gameOverText.gameObject.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
