using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoveCircle : MonoBehaviour
{
    public float moveSpeed = 10f;
    public GameObject circlePrefab1;
    public GameObject circlePrefab2;
    private List<GameObject> circles = new List<GameObject>();
    public float explosionRadius = 3.0f;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI resultText;
    public float gameTime = 20f;
    private bool gameActive = true;

    // Reference to the "Next" sprite
    public GameObject nextSprite;

    void Start()
    {
        // Initialize the game
        SpawnCircles(5);
        StartCoroutine(Timer());
        PositionTimerText();
        
        //DontDestroyOnLoad(nextSprite);

        // Hide the result text and "Next" sprite at the start
        resultText.gameObject.SetActive(false);
        nextSprite.SetActive(false);
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
            // Random spawn position within custom polygon boundaries
            Vector2 spawnPosition1 = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f));
            GameObject circle1 = Instantiate(circlePrefab1, spawnPosition1, Quaternion.identity);
            circles.Add(circle1);

            // Ensure circlePrefab2 is spawned at a distinct position
            Vector2 spawnPosition2 = new Vector2(Random.Range(-8f, 8f), Random.Range(-4f, 4f)); 
            GameObject circle2 = Instantiate(circlePrefab2, spawnPosition2, Quaternion.identity);
            circles.Add(circle2);

            // Ensure both circles have the BouncingCircle script attached
            circle1.AddComponent<BouncingCircle>();  // Add BouncingCircle for movement
            circle2.AddComponent<BouncingCircle>();  // Add BouncingCircle for movement
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
            gameActive = false;
            resultText.text = "You Did It!"; // Set win message
            resultText.gameObject.SetActive(true); // Show the result text
            Debug.Log("All circles destroyed! You win!");
            ShowNextButton(); // Show the "Next" button
        }
    }

    IEnumerator Timer()
    {
        while (gameTime > 0 && gameActive)
        {
            timerText.text = "Time Left: " + Mathf.Ceil(gameTime).ToString();
            yield return new WaitForSeconds(1f);
            gameTime--;
        }

        // If time runs out and the game is still active, display lose message
        if (gameActive)
        {
            timerText.text = "Time's Up!";
            resultText.text = "You Failed :("; // Set lose message
            resultText.gameObject.SetActive(true); // Show the result text
            Debug.Log("Game Over! Time's up.");
            gameActive = false;
            ShowNextButton(); // Show the "Next" button
        }
    }

    void ShowNextButton()
    {
        if (nextSprite != null)
        {
        nextSprite.SetActive(true); // Make the "Next" button visible
        }
    }

    void PositionTimerText()
    {
        if (timerText != null)
        {
            RectTransform rectTransform = timerText.GetComponent<RectTransform>();
            rectTransform.anchorMin = new Vector2(1f, 1f);   // Top-right corner
            rectTransform.anchorMax = new Vector2(1f, 1f);   // Top-right corner
            rectTransform.pivot = new Vector2(1f, 1f);       // Top-right corner
            rectTransform.anchoredPosition = new Vector2(-10, -10);  // Offset from the corner
        }
    }
}
