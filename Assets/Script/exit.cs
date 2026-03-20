using UnityEngine;

public class exit : MonoBehaviour, IInteractable
{
    [Header("Objective Gate")]
    [SerializeField] private bool requiresPuzzle1Complete = true;

    [Header("Exit Requirement")]
    [SerializeField] private bool requiresItem = false;
    [SerializeField] private string requiredItemId = "AccessCard";

    private Puzzle1Manager puzzle1Manager;

    private void Awake()
    {
        puzzle1Manager = FindFirstObjectByType<Puzzle1Manager>();
    }

    public void Interact(GameObject interactor)
    {
        playerCarryItem carryItem = interactor.GetComponent<playerCarryItem>();

        if (requiresPuzzle1Complete && puzzle1Manager != null && !puzzle1Manager.IsCompleted)
        {
            Debug.Log("Exit terkunci. Selesaikan objective Puzzle 1 dulu.");
            return;
        }

        if (requiresItem)
        {
            if (carryItem == null || !carryItem.HasRequiredItem(requiredItemId))
            {
                Debug.Log("Exit terkunci. Butuh item: " + requiredItemId);
                return;
            }

            carryItem.ClearItem();
        }

        Debug.Log("Exit terbuka. Player berhasil keluar ruangan.");

        game gameManager = FindFirstObjectByType<game>();
        if (gameManager != null)
        {
            gameManager.WinRun();
        }
    }
}
