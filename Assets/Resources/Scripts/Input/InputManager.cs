using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerControls controls;
    private Vector2 movement;
    public Vector2 Movement => movement;

    [SerializeField] private string mainMenuSceneName = "MainMenu";

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        controls = new PlayerControls();
    }

    private void OnEnable()
    {
        controls.Player.Enable();

        controls.Player.Move.performed += ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement = Vector2.zero;

        controls.Player.BackToMenu.performed += ctx => ReturnToMenu();
    }

    private void OnDisable()
    {
        controls.Player.Move.performed -= ctx => movement = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled -= ctx => movement = Vector2.zero;
        controls.Player.BackToMenu.performed -= ctx => ReturnToMenu();

        controls.Player.Disable();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
