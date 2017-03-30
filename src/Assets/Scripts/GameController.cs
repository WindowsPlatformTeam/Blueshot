using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text ScoreText;
    public GameObject gameOverPanel;

    private int _score;
    private bool _isGameOver;

    public static GameController GetInstance()
    {
        var gameControllerGameObject = GameObject.FindWithTag("GameController");
        if (gameControllerGameObject != null)
        {
            return gameControllerGameObject.GetComponent<GameController>();
        }
        return null;
    }

    public void Update()
    {
        if (_isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void AddScore(int score)
    {
        _score += score;
        ScoreText.text = string.Format("Score: {0}", _score);
    }

    public void GameOver()
    {
        _isGameOver = true;
        gameOverPanel.SetActive(true);
    }

    public void Restart()
    {
        _isGameOver = false;
        gameOverPanel.SetActive(false);
        SceneManager.LoadScene("MainScene");
    }
}
