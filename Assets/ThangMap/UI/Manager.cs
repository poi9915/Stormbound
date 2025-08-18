using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("UI Panels")]
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;
    public GameObject characterPanel;
    public GameObject winLosePanel;
    public GameObject loadingPanel;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        ShowMainMenu();
    }

    public void HideAll()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(false);
        characterPanel.SetActive(false);
        winLosePanel.SetActive(false);
        loadingPanel.SetActive(false);
    }

    // ================== STATES ===================
    public void ShowMainMenu()
    {
        HideAll();
        mainMenuPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        HideAll();
        settingsPanel.SetActive(true);
    }

    public void ShowCharacter()
    {
        HideAll();
        characterPanel.SetActive(true);
    }

    public void ShowWinLose()
    {
        HideAll();
        winLosePanel.SetActive(true);
    }

    public void ShowLoading()
    {
        HideAll();
        loadingPanel.SetActive(true);
    }
}
