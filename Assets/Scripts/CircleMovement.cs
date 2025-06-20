using UnityEngine;
using UnityEngine.SceneManagement;

public class CircleMovement : MonoBehaviour
{
    public enum MovementMode {Static, KeyboardMovement, MouseMovement}

    [SerializeField] private float speed = 5f;
    [SerializeField] private MovementMode movomentMode = MovementMode.Static;

    private Rigidbody2D rb;
    private Vector2 direction = Vector2.right;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (movomentMode == MovementMode.Static)
        {
            direction = new Vector2(1f, Random.Range(-1f, 1f)).normalized;
            rb.linearVelocity = direction * speed;
        }
    }

    void FixedUpdate()
    {
        switch (movomentMode)
        {
            case MovementMode.KeyboardMovement:
                if (InputManager.Instance == null) return;
                Vector2 input = InputManager.Instance.Movement;
                rb.MovePosition(rb.position + input * speed * Time.fixedDeltaTime);

                break;

            case MovementMode.MouseMovement:
                Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 targetPos = mouseWorld;

                Vector2 toTarget = targetPos - rb.position;
                float distance = toTarget.magnitude;

                if (distance < 0.1f)
                {
                    rb.linearVelocity = Vector2.zero;
                    return;
                }

                Vector2 nextPos = Vector2.Lerp(rb.position, targetPos, speed * Time.fixedDeltaTime);
                rb.MovePosition(nextPos);

                break;
        }
    }
}
