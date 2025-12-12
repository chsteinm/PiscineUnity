using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayersManager : MonoBehaviour
{
    // Parent object that contains all player GameObjects as children.
    public GameObject playersParent;

    // Main camera that will follow the active player.
    public Camera mainCamera;

    // Array of all PlayerController scripts found on the children.
    private PlayerController[] controllers;

    // Index of the currently active player (-1 means no active player yet).
    private int activePlayerIndex = -1;

    void Start()
    {
        // Cache all PlayerController components from the children of playersParent.
        int childCount = playersParent.transform.childCount;
        controllers = new PlayerController[childCount];

        for (int i = 0; i < childCount; i++)
        {
            var child = playersParent.transform.GetChild(i);
            controllers[i] = child.GetComponent<PlayerController>();
            controllers[i].InitAsChild(mainCamera); // Initialize each child controller.
        }

        // No active player at startup: activePlayerIndex stays at -1.
    }

    void Update()
    {
        CenterCamera();
    }

    // Called by the Input System when the Move action is triggered.
    void OnMove(InputValue value)
    {
        if (activePlayerIndex == -1) return; // No active player yet.

        // Forward the input to the currently active player controller.
        controllers[activePlayerIndex].OnMoveInput(value);
    }

    // Called by the Input System when the Jump action is triggered.
    void OnJump(InputValue value)
    {
        if (activePlayerIndex == -1) return;

        controllers[activePlayerIndex].OnJumpInput(value);
    }

    // Called by the Input System when switching to player 1.
    void OnSwitchToPlayer1() => SwitchToPlayer(0);

    // Called by the Input System when switching to player 2.
    void OnSwitchToPlayer2() => SwitchToPlayer(1);

    // Called by the Input System when switching to player 3.
    void OnSwitchToPlayer3() => SwitchToPlayer(2);

    // Called by the Input System to reset the current scene.
    void OnReset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Switch the active controlled player by index.
    void SwitchToPlayer(int index)
    {
        if (index < 0 || index >= controllers.Length) return;

        activePlayerIndex = index;

        // Activate only the selected player, deactivate all others.
        for (int i = 0; i < controllers.Length; i++)
        {
            controllers[i].SetActive(i == activePlayerIndex);
        }

        CenterCamera();
    }

    // Center the camera on the currently active player.
    void CenterCamera()
    {
        if (activePlayerIndex == -1) return;

        var rb = controllers[activePlayerIndex].GetRigidbody();
        if (rb == null) return;

        Vector3 pos = rb.transform.position;
        mainCamera.transform.position = new Vector3(pos.x, pos.y, mainCamera.transform.position.z);
    }

    // Called by a PlayerController when it enters its exit.
    public void NotifyPlayerAtExit()
    {
        // Check if all players reached their exits.
        for (int i = 0; i < controllers.Length; i++)
        {
            if (!controllers[i].IsAtExit())
                return;
        }

        // All players are in their exits.
        Debug.Log("Win!");

        LoadNextLevel();
    }

    // Load the next level or scene.
    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentSceneIndex + 1);
        else
            SceneManager.LoadScene(0); // Loop back to the first scene
    }
}
