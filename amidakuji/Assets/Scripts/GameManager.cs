using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Transform[] spawnPoints;
    public float spawnInterval = 2.0f;
    private bool isGameOver = false;

    private Coroutine spawnRoutine;
    private ScoreManager scoreManager;

    public Camera gameCamera;
    public Camera gameOverCamera;

    [System.Obsolete]
    void Start()
    {
        gameCamera.enabled = true;
        gameOverCamera.enabled = false;

        int randomPointIndex = Random.Range(0, spawnPoints.Length);
        int randomObjectIndex = Random.Range(0, objectsToSpawn.Length);

        Transform spawnPoint = spawnPoints[randomPointIndex];
        GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

        Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);

        // 出現を開始し、指定の時間が経過したら停止
        scoreManager = FindObjectOfType<ScoreManager>();
        spawnRoutine = StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(spawnInterval);

            int randomPointIndex = Random.Range(0, spawnPoints.Length);
            int randomObjectIndex = Random.Range(0, objectsToSpawn.Length);

            Transform spawnPoint = spawnPoints[randomPointIndex];
            GameObject objectToSpawn = objectsToSpawn[randomObjectIndex];

            Instantiate(objectToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }

    public void GameOver()
    {
        isGameOver = true;

        gameCamera.enabled = false;
        gameOverCamera.enabled = true;

        if(spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }

        if(scoreManager != null)
        {
            scoreManager.EndGame();
        }
    }
}
