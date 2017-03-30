using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public Text ScoreText;

    private int _score;

    public static GameController GetInstance()
    {
        var gameControllerGameObject = GameObject.FindWithTag("GameController");
        if (gameControllerGameObject != null)
        {
            return gameControllerGameObject.GetComponent<GameController>();
        }
        return null;
    }

    public void AddScore(int score)
    {
        _score += score;
        ScoreText.text = string.Format("Score: {0}", _score);
    }
}
