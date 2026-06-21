using System.Collections.Generic;
using UnityEngine;

public class AmidaController : MonoBehaviour
{
    public GameObject HorizonPrefab;
    public float gridSize = 4.0f;
    public GameObject parentObject;
    private List<Vector2> linePositions = new List<Vector2>();
    private Queue<GameObject> lineObjects = new Queue<GameObject>();
    private const int maxLines = 6;
    private float startTime;
    // private const float gameDuration = 60f;

    public float minX = -4.0f;
    public float maxX = 4.0f;
    public float minY = -6.0f;
    public float maxY = 7.0f;

    private void Start()
    {
        startTime = Time.time;
    }

    void Update()
    {
        // if(Time.time -startTime > gameDuration)
        // {
        //     return;
        // }

        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            float snappedX = Mathf.Round(mousePos.x / gridSize) * gridSize;
            float snappedY = Mathf.Round(mousePos.y);
            Vector2 snappedPosition = new Vector2(snappedX, snappedY);

            if (IsValidPosition(snappedPosition))
            {
                GameObject newLine = Instantiate(HorizonPrefab, snappedPosition, Quaternion.identity);

                newLine.transform.SetParent(parentObject.transform);

                linePositions.Add(snappedPosition);
                lineObjects.Enqueue(newLine);

                if (lineObjects.Count > maxLines)
                {
                    GameObject oldLine = lineObjects.Dequeue();
                    linePositions.Remove(oldLine.transform.position);
                    Destroy(oldLine);
                }

                if (lineObjects.Count == maxLines)
                {
                    GameObject firstLine = lineObjects.Peek();
                    SpriteRenderer spriteRenderer = firstLine.GetComponent<SpriteRenderer>();
                    if (spriteRenderer != null)
                    {
                        Color color = spriteRenderer.color;
                        color.a = 0.75f;
                        spriteRenderer.color = color;
                    }
                }
            }
        }
    }

    bool IsValidPosition(Vector2 position)
    {
        if (position.x < minX || position.x > maxX || position.y < minY || position.y > maxY)
        {
            return false;
        }
        return !linePositions.Contains(position);
    }
}
