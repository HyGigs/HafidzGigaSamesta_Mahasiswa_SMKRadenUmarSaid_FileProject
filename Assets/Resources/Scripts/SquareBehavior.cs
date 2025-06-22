using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider2D))]
public class SquareBehavior : MonoBehaviour
{
    private SquareSpawner.SquareType type;
    private SquareSpawner.RespawnOption respawnOption;
    private float respawnDelay;
    private SquareSpawner spawner;

    [SerializeField] private AudioClip destroySFX;

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
        if (type != SquareSpawner.SquareType.Destroyable) return;
        if (!collision.CompareTag("Circle")) return;

        ScoreManager.Instance?.AddScore(1);
        spawner?.RemovePosition(transform.position);

        if (destroySFX != null)
        {
            AudioSource.PlayClipAtPoint(destroySFX, transform.position);
        }

        if (respawnOption == SquareSpawner.RespawnOption.CanRespawn)
        {
            spawner.StartCoroutine(spawner.RespawnNewSquareWithDelay(respawnDelay));
        }

        Destroy(gameObject);
    }
}