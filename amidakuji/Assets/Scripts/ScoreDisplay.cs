using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;  // UIテキスト（ランキング表示用）
    public ScoreManager scoreManager;  // ScoreManagerへの参照

    void Start()
    {
        DisplayScores();  // 起動時にランキング表示
    }

    public void DisplayScores()
    {
        List<int> scores = scoreManager.GetScores();
        scoreText.text = "ランキング\n";
        for (int i = 0; i < scores.Count; i++)
        {
            scoreText.text += $"{i + 1}: {scores[i]}\n";  // ランキングをUIに反映
        }
    }
}
