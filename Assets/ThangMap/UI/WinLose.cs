using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinLose : MonoBehaviour
{
    public TMP_Text resultText;
    public TMP_Text scoreText;
    public Button retryButton;
    public Button menuButton;

    void Start()
    {
        retryButton.onClick.AddListener(OnRetry);
        menuButton.onClick.AddListener(OnMenu);
        gameObject.SetActive(false);
    }

    public void ShowWin(int score)
    {
        gameObject.SetActive(true);
        resultText.text = "THẮNG";
        resultText.color = new Color32(0, 229, 255, 255); // cyan neon
        scoreText.text = "Điểm: " + score;
    }

    public void ShowLose(int score)
    {
        gameObject.SetActive(true);
        resultText.text = "THUA";
        resultText.color = new Color32(255, 77, 77, 255); // đỏ neon
        scoreText.text = "Điểm: " + score;
    }

    void OnRetry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // load lại màn hiện tại
    }

    void OnMenu()
    {
        SceneManager.LoadScene("MainMenu"); // load menu
    }
}
