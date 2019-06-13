using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    public Text score1;    public Text score2;    public Text score3;    public Text score4;    public Text score5;
    public Text HName1; public Text HName2; public Text HName3; public Text HName4; public Text HName5;
    public Text ttime1; public Text ttime2; public Text ttime3; public Text ttime4; public Text ttime5;
    public static int count = 1;
    public static int number = 0;
    public static string name = "Default Name";
    public static int rank = 1;
    public GameObject NewGameBtn;
    public Dropdown dropDown;

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
        PlayerPrefs.DeleteKey("RecentScore4");
        PlayerPrefs.DeleteKey("RecentScore5");
        PlayerPrefs.DeleteKey("RName1");
        PlayerPrefs.DeleteKey("RName2");
        PlayerPrefs.DeleteKey("RName3");
        PlayerPrefs.DeleteKey("RName4");
        PlayerPrefs.DeleteKey("RName5");
        PlayerPrefs.DeleteKey("RTime1");
        PlayerPrefs.DeleteKey("RTime2");
        PlayerPrefs.DeleteKey("RTime3");
        PlayerPrefs.DeleteKey("RTime4");
        PlayerPrefs.DeleteKey("RTime5");
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
        PlayerPrefs.DeleteKey("HighScore4");
        PlayerPrefs.DeleteKey("HighScore5");
        PlayerPrefs.DeleteKey("HName1");
        PlayerPrefs.DeleteKey("HName2");
        PlayerPrefs.DeleteKey("HName3");
        PlayerPrefs.DeleteKey("HName4");
        PlayerPrefs.DeleteKey("HName5");
        PlayerPrefs.DeleteKey("HTime1");
        PlayerPrefs.DeleteKey("HTime2");
        PlayerPrefs.DeleteKey("HTime3");
        PlayerPrefs.DeleteKey("HTime4");
        PlayerPrefs.DeleteKey("HTime5");
    }
    void Update()
    {
        
        if (dropDown.value == 0)
        {
            int tempScore1 = PlayerPrefs.GetInt("HighScore1");
            score1.text = tempScore1.ToString();
            int tempScore2 = PlayerPrefs.GetInt("HighScore2");
            score2.text = tempScore2.ToString();
            int tempScore3 = PlayerPrefs.GetInt("HighScore3");
            score3.text = tempScore3.ToString();
            int tempScore4 = PlayerPrefs.GetInt("HighScore4");
            score4.text = tempScore4.ToString();
            int tempScore5 = PlayerPrefs.GetInt("HighScore5");
            score5.text = tempScore5.ToString();
            string hName1 = PlayerPrefs.GetString("HName1");
            HName1.text = hName1;
            string hName2 = PlayerPrefs.GetString("HName2");
            HName2.text = hName2;
            string hName3 = PlayerPrefs.GetString("HName3");
            HName3.text = hName3;
            string hName4 = PlayerPrefs.GetString("HName4");
            HName4.text = hName4;
            string hName5 = PlayerPrefs.GetString("HName5");
            HName5.text = hName5;
            string time1 = PlayerPrefs.GetString("HTime1");
            ttime1.text = time1;
            string time2 = PlayerPrefs.GetString("HTime2");
            ttime2.text = time2;
            string time3 = PlayerPrefs.GetString("HTime3");
            ttime3.text = time3;
            string time4 = PlayerPrefs.GetString("HTime4");
            ttime4.text = time4;
            string time5 = PlayerPrefs.GetString("HTime5");
            ttime5.text = time5;
        }
        else if (dropDown.value == 1)
        {
            int RtempScore1 = PlayerPrefs.GetInt("RecentScore1");
            score1.text = RtempScore1.ToString();
            int RtempScore2 = PlayerPrefs.GetInt("RecentScore2");
            score2.text = RtempScore2.ToString();
            int RtempScore3 = PlayerPrefs.GetInt("RecentScore3");
            score3.text = RtempScore3.ToString();
            int RtempScore4 = PlayerPrefs.GetInt("RecentScore4");
            score4.text = RtempScore4.ToString();
            int RtempScore5 = PlayerPrefs.GetInt("RecentScore5");
            score5.text = RtempScore5.ToString();

            string rName1 = PlayerPrefs.GetString("RName1");
            HName1.text = rName1;
            string rName2 = PlayerPrefs.GetString("RName2");
            HName2.text = rName2;
            string rName3 = PlayerPrefs.GetString("RName3");
            HName3.text = rName3;
            string rName4 = PlayerPrefs.GetString("RName4");
            HName4.text = rName4;
            string rName5 = PlayerPrefs.GetString("RName5");
            HName5.text = rName5;

            string rtime1 = PlayerPrefs.GetString("RTime1");
            ttime1.text = rtime1;
            string rtime2 = PlayerPrefs.GetString("RTime2");
            ttime2.text = rtime2;
            string rtime3 = PlayerPrefs.GetString("RTime3");
            ttime3.text = rtime3;
            string rtime4 = PlayerPrefs.GetString("RTime4");
            ttime4.text = rtime4;
            string rtime5 = PlayerPrefs.GetString("RTime5");
            ttime5.text = rtime5;

        }
        

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
