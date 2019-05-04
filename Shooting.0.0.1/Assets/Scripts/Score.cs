using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Text score1;
    public Text score2;
    public Text score3;
    public Text Rscore1;
    public Text Rscore2;
    public Text Rscore3;
    public static int count = 1;
    public static int number = 0;
    public static string name = "Default Name";
    public static int rank = 1;
    public GameObject NewGameBtn;

    public void GameStart()
    {
        SceneManager.LoadScene("Level1");
    }

    void Start()
    {
        NewGameBtn.SetActive(true);
    }

    public void DeleteRecentScore()
    {
        PlayerPrefs.DeleteKey("RecentScore1");
        PlayerPrefs.DeleteKey("RecentScore2");
        PlayerPrefs.DeleteKey("RecentScore3");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void DeleteHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore1");
        PlayerPrefs.DeleteKey("HighScore2");
        PlayerPrefs.DeleteKey("HighScore3");
    }
    void Update()
    {
        int tempScore1 = PlayerPrefs.GetInt("HighScore1");
        score1.text = tempScore1.ToString();
        int tempScore2 = PlayerPrefs.GetInt("HighScore2");
        score2.text = tempScore2.ToString();
        int tempScore3 = PlayerPrefs.GetInt("HighScore3");
        score3.text = tempScore3.ToString();
        int RtempScore1 = PlayerPrefs.GetInt("RecentScore1");
        Rscore1.text = RtempScore1.ToString();
        int RtempScore2 = PlayerPrefs.GetInt("RecentScore2");
        Rscore2.text = RtempScore2.ToString();
        int RtempScore3 = PlayerPrefs.GetInt("RecentScore3");
        Rscore3.text = RtempScore3.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
