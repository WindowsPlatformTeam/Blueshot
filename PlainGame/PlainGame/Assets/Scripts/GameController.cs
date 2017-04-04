using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _gameController;

    public GameObject GameOverLayer;
    public Text Text;

    private int _score = 0;
    private bool _isDead = false;

    private void Start()
    {
        _gameController = GetComponent<GameController>();
        UpdateScore();
    }

    private void Update()
    {
        if(_isDead && Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public static GameController GetInstance()
    {
        return _gameController;
    }

    public void AddScore(int score)
    {
        _score += score;
        UpdateScore();
    }

    public void GameOver()
    {
        GameOverLayer.SetActive(true);
        Time.timeScale = 0.3f;
        _isDead = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1.0f;
    }

    private void UpdateScore()
    {
        Text.text = string.Format("Score: {0}", _score);
    }
}
