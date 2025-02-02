using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCircle : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject circlePrefab1;
    public GameObject circlePrefab2;
    private List<GameObject> circles = new List<GameObject>();
    public float explosionRadius = 3.0f;
    public Text timerText;
    public float gameTime = 20f;
    private bool gameActive = true;

    void Start()
    {
        SpawnCircles(5);
        StartCoroutine(Timer());
        PositionTimerText();
    }

    void Update()
    {
        if (!gameActive) return;

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ExplodeNearbyCircle();
        }

        CleanupAndCheckGameEnd();
    }

    void SpawnCircles(int count)
    {
        circles.Clear();
        for (int i = 0; i < count; i++)
        {
            Vector2 spawnPosition = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            GameObject circle1 = Instantiate(circlePrefab1, spawnPosition, Quaternion.identity);
            circles.Add(circle1);

            GameObject circle2 = Instantiate(circlePrefab2, spawnPosition, Quaternion.identity);
            circles.Add(circle2);
        }
    }

    void ExplodeNearbyCircle()
    {
        Vector2 cursorWorldPos = transform.position;
        GameObject nearestCircle = null;
        float nearestDistance = float.MaxValue;

        foreach (GameObject circle in new List<GameObject>(circles))
        {
            if (circle != null)
            {
                float distance = Vector2.Distance(cursorWorldPos, circle.transform.position);
                if (distance <= explosionRadius && distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestCircle = circle;
                }
            }
        }

        if (nearestCircle != null)
        {
            circles.Remove(nearestCircle);
            Destroy(nearestCircle);
        }
    }

    void CleanupAndCheckGameEnd()
    {
        circles.RemoveAll(circle => circle == null);

        if (circles.Count == 0)
        {
            Debug.Log("All circles destroyed! Game Complete!");
            gameActive = false;
        }
    }

    IEnumerator Timer()
    {
        while (gameTime > 0)
        {
            timerText.text = "Time Left: " + Mathf.Ceil(gameTime).ToString();
            yield return new WaitForSeconds(1f);
            gameTime--;
        }
        timerText.text = "Time's Up!";
        Debug.Log("Game Over!");
        gameActive = false;
    }

    void PositionTimerText()
    {
        if (timerText != null)
        {
            RectTransform rectTransform = timerText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(0.5f, 1f);
            rectTransform.anchorMax = new Vector2(0.5f, 1f);
            rectTransform.pivot = new Vector2(0.5f, 1f);
            rectTransform.anchoredPosition = new Vector2(0, -50);
        }
    }
}
