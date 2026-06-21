using UnityEngine;

public class AddScore : MonoBehaviour
{
    private ScoreManager scoreManager;
    private GameManager gameManager;
    public string targetTag = "targetTag";

    [System.Obsolete]
    private void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(targetTag))
        {
            scoreManager.ShowScore(10);
        }
        else
        {
            gameManager.GameOver();
        }
    }
}