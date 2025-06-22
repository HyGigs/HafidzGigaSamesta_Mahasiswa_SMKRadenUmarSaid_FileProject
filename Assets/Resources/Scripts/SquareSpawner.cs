using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class SquareSpawner : MonoBehaviour
{
    public enum SquareType
    {
        Destroyable,
        NonDestroyable
    }

    public enum RespawnOption
    {
        NoRespawn,
        CanRespawn
    }

    [Header("Square Settings")]
    [SerializeField] private SquareType squareType = SquareType.Destroyable;
    [SerializeField] private RespawnOption respawnOption = RespawnOption.CanRespawn;
    [SerializeField] private float respawnDelay = 3f;

    [Header("Spawn Prefab")]
    [SerializeField] private GameObject squarePrefab;

    [Header("Scene References")]
    [SerializeField] private Transform circle;
    [SerializeField] private LayerMask wallLayer;

    [Header("Spawn Area Settings")]
    [SerializeField] private float spawnRadius = 0.3f;
    [SerializeField] private float safeDistanceFromCircle = 1.5f;
    [SerializeField] private float squareSpacing = 0.6f;

    [Header("Spawn Amount")]
    [SerializeField] private int minSquare = 5;
    [SerializeField] private int maxSquare = 15;

    private BoxCollider2D area;
    private List<Vector2> positions = new List<Vector2>();
    private Transform parentGroup;

    void Start()
    {
        ScoreManager.EnsureExists();
        ScoreManager.Instance.ResetScore();

        area = GetComponent<BoxCollider2D>();
        if (area == null) return;

        parentGroup = new GameObject("Squares").transform;
        int count = Random.Range(minSquare, maxSquare + 1);

        for (int i = 0; i < count; i++)
        {
            SpawnNewSquare();
        }
    }

    public void SpawnNewSquare()
    {
        Vector2 pos;
        if (TryGetSpawnPosition(out pos))
        {
            GameObject square = Instantiate(squarePrefab, pos, Quaternion.identity, parentGroup);

            positions.Add(pos);

            SquareBehavior behavior = square.GetComponent<SquareBehavior>();
            behavior.SetType(squareType);
            behavior.SetRespawnData(respawnOption, respawnDelay, this);
        }
        else
        {
            Debug.LogWarning("Gagal mencari posisi untuk square.");
        }
    }

    public void RemovePosition(Vector2 pos)
    {
        positions.Remove(pos);
    }



    private bool TryGetSpawnPosition(out Vector2 result)
    {
        const int maxTry = 100;
        for (int i = 0; i < maxTry; i++)
        {
            float x = Random.Range(area.bounds.min.x, area.bounds.max.x);
            float y = Random.Range(area.bounds.min.y, area.bounds.max.y);
            Vector2 candidate = new Vector2(x, y);

            if (Vector2.Distance(candidate, circle.position) < safeDistanceFromCircle) continue;

            bool tooClose = false;
            foreach (var p in positions)
            {
                if ((candidate - p).sqrMagnitude < squareSpacing * squareSpacing)
                {
                    tooClose = true;
                    break;
                }
            }
            if (tooClose) continue;

            Collider2D hit = Physics2D.OverlapCircle(candidate, spawnRadius, wallLayer);
            if (hit != null) continue;

            result = candidate;
            return true;
        }

        result = default;
        return false;
    }


    public IEnumerator RespawnNewSquareWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SpawnNewSquare();
    }

}