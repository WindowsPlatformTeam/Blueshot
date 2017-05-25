using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    private static GameController _gameController;

    public int MaxEnemies = 30;
    public float TimeToSpawn = 2;
    public GameObject GameOverLayer;
    public Transform SpawnLeft;
    public Transform SpawnRight;
    public Text Text;

    private int _score = 0;
    private bool _isDead = false;
    private float _nextSpawn;
    private int _enemyCount;

    private void Start()
    {
        _gameController = GetComponent<GameController>();
        Physics2D.IgnoreLayerCollision(8, 8);
        UpdateScore();
    }

    private void Update()
    {
        Spawn();
        if (_isDead && Input.GetKeyDown(KeyCode.R))
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

    private void Spawn()
    {
        if (Time.time < _nextSpawn || _enemyCount >= MaxEnemies) return;

        var spawnPoint = Random.Range(0, 2) == 0 ? SpawnLeft : SpawnRight;
        var enemyName = Random.Range(0, 2) == 0 ? "EnemyFly" : "EnemySpider";


        var bullet = Instantiate(Resources.Load(enemyName), spawnPoint.position, Quaternion.identity) as GameObject;

        _enemyCount++;
        _nextSpawn = Time.time + TimeToSpawn;
    }


}
