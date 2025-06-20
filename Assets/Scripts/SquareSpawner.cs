using UnityEngine;
using System.Collections.Generic;

public class SquareSpawner : MonoBehaviour
{
    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private Transform circle;
    [SerializeField] private float safeDistanceFromCircle = 1.5f;
    [SerializeField] private float squareSpacing = 0.5f;
    [SerializeField] private int minSquare = 5;
    [SerializeField] private int maxSquare = 20;

    private BoxCollider2D area;
    private List<Vector2> spawnedPositions = new List<Vector2>();
    private Transform squareParent;

    void Start()
    {
        area = GetComponent<BoxCollider2D>();

        if (area == null )
        {
            Debug.LogWarning("Please put the spawn area");
            return;
        }

        GameObject parentObj = new GameObject("Squares");
        squareParent = parentObj.transform;

        int squareCount = Random.Range(minSquare, maxSquare + 1);

        for (int i = 0; i < squareCount; i++)
        {
            Vector2 pos = GetValidPosition();

            if (pos != Vector2.positiveInfinity)
            {
                spawnedPositions.Add(pos);
                Instantiate(squarePrefab, pos, Quaternion.identity, squareParent);
            }
            else
            {
                Debug.LogWarning($"Failed to find valid position for square{i}");
            }
        }
    }

    private Vector2 GetValidPosition()
    {
        Bounds bounds = area.bounds;
        int attempts = 0;
        const int maxAttempts = 100;

        while (attempts < maxAttempts)
        {
            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);
            Vector2 candidatePos = new Vector2(x, y);

            if (Vector2.Distance(candidatePos, circle.position) < safeDistanceFromCircle)
            {
                attempts++;
                continue;
            }

            bool overlaps = false;
            foreach (var pos in spawnedPositions)
            {
                if (Vector2.Distance(candidatePos, pos) < squareSpacing)
                {
                    overlaps = true;
                    break;
                }
            }

            if (!overlaps)
            {
                return candidatePos;    
            }

            attempts++;
        }

        return Vector2.positiveInfinity;
    }
}
