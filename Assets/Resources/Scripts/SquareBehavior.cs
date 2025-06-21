using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class SquareBehavior : MonoBehaviour
{
    private SquareSpawner.SquareType type;
    private SquareSpawner.RespawnOption respawnOption;
    private float respawnDelay;
    private SquareSpawner spawner;

    private bool hasCollided = false;

    public void SetType(SquareSpawner.SquareType t)
    {
        type = t;
        GetComponent<BoxCollider2D>().isTrigger = (type == SquareSpawner.SquareType.Destroyable);
    }

    public void SetRespawnData(SquareSpawner.RespawnOption option, float delay, SquareSpawner referenceSpawner)
    {
        respawnOption = option;
        respawnDelay = delay;
        spawner = referenceSpawner;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasCollided) return;
        hasCollided = true;

        if (type != SquareSpawner.SquareType.Destroyable) return;
        if (!collision.CompareTag("Circle")) return;

        Debug.Log($"Square {gameObject.name} tertabrak Circle.");

        ScoreManager.Instance?.AddScore(1);

        spawner?.RemovePosition(transform.position);

        if (respawnOption == SquareSpawner.RespawnOption.CanRespawn)
        {
            Debug.Log($"Square {gameObject.name} akan respawn dalam {respawnDelay} detik.");
            spawner.StartCoroutine(spawner.RespawnNewSquareWithDelay(respawnDelay));
        }
        else
        {
            Debug.Log($"Square {gameObject.name} tidak di-respawn (NoRespawn).");
        }

        Destroy(gameObject);
    }
}
