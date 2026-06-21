using UnityEngine;

public class BlockController : MonoBehaviour
{
    public float moveDistance = 1f;
    public Vector2 direction = Vector2.down;
    private float moveTimer = 0f;
    private int roadCount = 0;
    private float spawnTime;
    private float speed = 1f;
    private int count = 1;

    private void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        moveTimer += Time.deltaTime;

        if(moveTimer >= 1f / speed)
        {
            transform.Translate(direction * moveDistance);
            moveTimer = 0f;
        }
        if(moveTimer >= count * 10f)
        {
            speed *= 1.2f;
            count++;
        }
        if (Time.time - spawnTime >= 1f && roadCount == 0)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("TurnRight"))
        {
            direction = new Vector2(direction.y, -direction.x);
        }
        else if (collision.CompareTag("TurnLeft"))
        {
            direction = new Vector2(-direction.y, direction.x);
        }
        if (collision.CompareTag("Road"))
        {
            roadCount++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Road"))
        {
            roadCount--;
        }
    }
}
