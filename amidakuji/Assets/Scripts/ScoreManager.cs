using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text totalScoreText;
    public int totalScore = 0;
    public Text scoreText;
    private const int maxScores = 10;
    private List<int> scores = new List<int>();
    

    private void Start()
    {
        LoadScores();
        totalScoreText.enabled = false;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha3) && Input.GetKey(KeyCode.Alpha6))
        {
            ResetScores();
        }
    }

    public void AddScore(int newScore)
    {
        scores.Add(newScore);
        scores.Sort((a, b) => b.CompareTo(a));
        if(scores.Count > maxScores)
        {
            scores.RemoveAt(scores.Count - 1);
        }
        SaveScores();
    }

    private void SaveScores()
    {
        for(int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetInt("Score" + i, scores[i]);
        }
        PlayerPrefs.SetInt("ScoreCount", scores.Count);
        PlayerPrefs.Save();
    }

    private void LoadScores()
    {
        scores.Clear();
        int scoreCount = PlayerPrefs.GetInt("ScoreCount", 0);
        for(int i = 0; i < scoreCount; i++)
        {
            scores.Add(PlayerPrefs.GetInt("Score" + i, 0));
        }
    }

    public List<int> GetScores()
    {
        return new List<int>(scores);
    }

    public void ShowScore(int score)
    {
        totalScore += score;
        scoreText.text = "Score : \n"+totalScore.ToString();
    }

    public void EndGame()
    {
        AddScore(totalScore);

        totalScoreText.text = "Score: " + totalScore;
        scoreText.enabled = false;
        totalScoreText.enabled = true;


        Destroy(GameObject.Find("Horizon"));
        Destroy(GameObject.Find("Vertical"));
        Destroy(GameObject.Find("Goal"));
    }

    public void ResetScores()
    {
        scores.Clear();
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Debug.Log("Scores have been reset!");
    }
}