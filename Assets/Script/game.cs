using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class game : MonoBehaviour
{
    [Header("Restart")]
    [SerializeField] private bool allowRestartWithR = true;
    [SerializeField] private bool restartOnlyAfterGameEnds = true;

    public enum GameState
    {
        Playing,
        Win,
        Lose
    }

    public GameState CurrentState { get; private set; } = GameState.Playing;

    private void Update()
    {
        if (!allowRestartWithR || Keyboard.current == null)
        {
            return;
        }

        if (!Keyboard.current.rKey.wasPressedThisFrame)
        {
            return;
        }

        if (restartOnlyAfterGameEnds && CurrentState == GameState.Playing)
        {
            Debug.Log("Restart dengan R aktif saat state Win/Lose.");
            return;
        }

        RestartRun();
    }

    public void WinRun()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        CurrentState = GameState.Win;
        Debug.Log("WIN: objective selesai dan berhasil keluar.");
    }

    public void LoseRun()
    {
        if (CurrentState != GameState.Playing)
        {
            return;
        }

        CurrentState = GameState.Lose;
        Debug.Log("LOSE: player gagal bertahan.");
    }

    public void RestartRun()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
