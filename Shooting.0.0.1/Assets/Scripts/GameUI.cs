using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameUI : MonoBehaviour { //INGAME UI//

	public GameObject allUI;
    public GameObject joyStick;
    public GameObject joyStick2;
    public Image fadePlane;
	public GameObject gameOverUI;
    public GameObject pauseUI;
    public GameObject nextLevelUI;
    public GameObject mapSelect;
	public RectTransform newWaveBanner;
	public Text newWaveTitle;
	public Text newWaveEnemyCount;
	public Text scoreUI;
	public Text gameOverScoreUI;
    //public Text rivalScoreUI; // 겜화면에서 상대방 점수 표시할  text //좌 상단
    public InputField inputField;
	public RectTransform healthBar;
    public int nowScore;
    public string playerName = "Default Name";
    public Text inputText;
    public string nowTime1 = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
    bool IsPause;

    public Spawner spawner;
	public Player player;

	void Start () {
		allUI.SetActive (true);
		//player = FindObjectOfType<Player> ();
		player.OnDeath += OnGameOver;
        StartCoroutine("AnimateNewWaveBanner");
    }

	void Awake() {
        //spawner = FindObjectOfType<Spawner> ();
        StartCoroutine("AnimateNewWaveBanner");
        spawner.OnNewWave += OnNewWave;
	}

	void Update() {
		scoreUI.text = ScoreKeeper.score.ToString("D6");
        float healthPercent = 0;
		if (player != null) {
			healthPercent = player.health / player.startingHealth;
		}
		healthBar.localScale = new Vector3 (healthPercent, 1, 1);

        if (Input.GetKey(KeyCode.Escape))
        {
            OnPause();
        }

    }

	void OnNewWave(int waveNumber) {
		string[] numbers = { "One", "Two", "Three", "Four", "Five" };
		//newWaveTitle.text = "- Wave " + numbers [waveNumber - 1] + " -";
		string enemyCountString = ((spawner.waves [waveNumber - 1].infinite) ? "Infinite" : spawner.waves [waveNumber - 1].enemyCount + "");
		//newWaveEnemyCount.text = "Enemies: " + enemyCountString;

		StopCoroutine ("AnimateNewWaveBanner");
		StartCoroutine ("AnimateNewWaveBanner");
	}


		
	void OnGameOver() {
        /*
        nowScore = ScoreKeeper.GetScore();
        string nowTime = nowTime1;
        int highScore1 = PlayerPrefs.GetInt("HighScore1");        int highScore2 = PlayerPrefs.GetInt("HighScore2");        int highScore3 = PlayerPrefs.GetInt("HighScore3");        int highScore4 = PlayerPrefs.GetInt("HighScore4");        int highScore5 = PlayerPrefs.GetInt("HighScore5");
        int recentScore1 = PlayerPrefs.GetInt("RecentScore1");        int recentScore2 = PlayerPrefs.GetInt("RecentScore2");        int recentScore3 = PlayerPrefs.GetInt("RecentScore3");        int recentScore4 = PlayerPrefs.GetInt("RecentScore4");        int recentScore5 = PlayerPrefs.GetInt("RecentScore5");
        string HTime1 = PlayerPrefs.GetString("HTime1"); string HTime2 = PlayerPrefs.GetString("HTime2"); string HTime3 = PlayerPrefs.GetString("HTime3"); string HTime4 = PlayerPrefs.GetString("HTime4"); string HTime5 = PlayerPrefs.GetString("HTime5");
        string STime1 = PlayerPrefs.GetString("STime1"); string STime2 = PlayerPrefs.GetString("STime2"); string STime3 = PlayerPrefs.GetString("STime3"); string STime4 = PlayerPrefs.GetString("STime4"); string STime5 = PlayerPrefs.GetString("STime5");
        makeHighScore(nowScore, highScore1, highScore2, highScore3, highScore4, highScore5,nowTime, HTime1,HTime2,HTime3,HTime4,HTime5);
        makeRecentScore(nowScore, recentScore1, recentScore2,recentScore4,recentScore5, nowTime,STime1,STime2,STime3,STime4);
        SetPlayerName();*/
        joyStick.SetActive(false);
        joyStick2.SetActive(false);
        //inputField.SetActive(true);
        Cursor.visible = true;
		StartCoroutine(Fade (Color.clear, new Color(0,0,0,.95f),1));
		gameOverScoreUI.text = scoreUI.text;
        //==
        //int receiveScore = ???; //함수 구현 int ReceiveScore(arguments);
        //==
        //if(atoi(gameOverScoreUI.text)>receiveScore)
        //  승리 표시
        //else
        //  패배 표시
        //변수 추출//
        //점수 상대에게 전달하는 함수 사용
        //gameOverScoreUI.text를 전달
        //
        //==
        scoreUI.gameObject.SetActive (false);
		healthBar.transform.parent.gameObject.SetActive (false);
        
		gameOverUI.SetActive (true);
        //여기서 변수를 추출해야 한다 by 정희석
	}

    void OnPause()
    {
        Time.timeScale = 0;
        joyStick.SetActive(false);
        joyStick2.SetActive(false);
        //inputField.SetActive(true);
        Cursor.visible = true;
        //StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, .95f), 1));
        gameOverScoreUI.text = scoreUI.text;
        scoreUI.gameObject.SetActive(false);
        healthBar.transform.parent.gameObject.SetActive(false);
        pauseUI.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        joyStick.SetActive(true);
        joyStick2.SetActive(true);
        //inputField.SetActive(true);
        Cursor.visible = true;
        gameOverScoreUI.text = scoreUI.text;
        scoreUI.gameObject.SetActive(true);
        healthBar.transform.parent.gameObject.SetActive(true);
        pauseUI.SetActive(false);
        nextLevelUI.SetActive(false);
    }

    
	IEnumerator AnimateNewWaveBanner() {

		float delayTime = 1.5f;
		float speed = 3f;
		float animatePercent = 0;
		int dir = 1;

		float endDelayTime = Time.time + 1 / speed + delayTime;

		while (animatePercent >= 0) {
			animatePercent += Time.deltaTime * speed * dir;

			if (animatePercent >= 1) {
				animatePercent = 1;
				if (Time.time > endDelayTime) {
					dir = -1;
				}
			}

			newWaveBanner.anchoredPosition = Vector2.up * Mathf.Lerp (-170, 45, animatePercent);
			yield return null;
		}

	}

	IEnumerator Fade(Color from, Color to, float time) {
		float speed = 1 / time;
		float percent = 0;

		while (percent < 1) {
			percent += Time.deltaTime * speed;
			fadePlane.color = Color.Lerp(from,to,percent);
			yield return null;
		}
	}
    void makeHighScore(int nowScore, int highscore1, int highscore2, int highscore3, int highscore4, int highscore5, string nowtime, string htime1, string htime2, string htime3, string htime4, string htime5, string hname1, string hname2,
                                           string hname3,string hname4, string playername )
    {
        if (nowScore >highscore1)
        {
            PlayerPrefs.SetInt("HighScore5", highscore4);
            PlayerPrefs.SetString("HName5", hname4);
            PlayerPrefs.SetString("HTime5", htime4);
            PlayerPrefs.SetInt("HighScore4", highscore3);
            PlayerPrefs.SetString("HTime4", htime3);
            PlayerPrefs.SetString("HName4", hname3);
            PlayerPrefs.SetInt("HighScore3", highscore2);
            PlayerPrefs.SetString("HTime3", htime2);
            PlayerPrefs.SetString("HName3", hname2);
            PlayerPrefs.SetInt("HighScore2", highscore1);
            PlayerPrefs.SetString("HTime2", htime1);
            PlayerPrefs.SetString("HName2", hname1);
            PlayerPrefs.SetInt("HighScore1", nowScore);
            PlayerPrefs.SetString("HTime1", nowtime);
            PlayerPrefs.SetString("HName1", playername);
        }
        else if (nowScore > highscore2)
        {
            PlayerPrefs.SetInt("HighScore5", highscore4);
            PlayerPrefs.SetString("HName5", hname4);
            PlayerPrefs.SetString("HTime5", htime4);
            PlayerPrefs.SetInt("HighScore4", highscore3);
            PlayerPrefs.SetString("HTime4", htime3);
            PlayerPrefs.SetString("HName4", hname3);
            PlayerPrefs.SetInt("HighScore3", highscore2);
            PlayerPrefs.SetString("HTime3", htime2);
            PlayerPrefs.SetString("HName3", hname2);
            PlayerPrefs.SetInt("HighScore2", nowScore);
            PlayerPrefs.SetString("HTime2", nowtime);
            PlayerPrefs.SetString("HName2", playername);
        }
        else if (nowScore > highscore3)
        {
            PlayerPrefs.SetInt("HighScore5", highscore4);
            PlayerPrefs.SetString("HName5", hname4);
            PlayerPrefs.SetString("HTime5", htime4);
            PlayerPrefs.SetInt("HighScore4", highscore3);
            PlayerPrefs.SetString("HTime4", htime3);
            PlayerPrefs.SetString("HName4", hname3);
            PlayerPrefs.SetInt("HighScore3", nowScore);
            PlayerPrefs.SetString("HTime3", nowtime);
            PlayerPrefs.SetString("HName3", playername);
        }
        else if (nowScore > highscore4)
        {
            PlayerPrefs.SetInt("HighScore5", highscore4);
            PlayerPrefs.SetString("HName5", hname4);
            PlayerPrefs.SetString("HTime5", htime4);
            PlayerPrefs.SetInt("HighScore4", nowScore);
            PlayerPrefs.SetString("HTime4", nowtime);
            PlayerPrefs.SetString("HName4", playername);
        }
        else if (nowScore > highscore5)
        {
            PlayerPrefs.SetInt("HighScore5", nowScore);
            PlayerPrefs.SetString("HTime5", nowtime);
            PlayerPrefs.SetString("HName5", playername);
        }
    }
    void makeRecentScore(int nowScore, int recentscore1, int recentscore2,int recentscore3,int recentscore4, string nowtime, string rtime1, string rtime2, string rtime3, string rtime4, string rname1, string rname2,
                                                string rname3, string rname4, string playername)
    {
        PlayerPrefs.SetInt("RecentScore5", recentscore4);
        PlayerPrefs.SetString("RTime5", rtime4);
        PlayerPrefs.SetString("RName5", rname4);
        PlayerPrefs.SetInt("RecentScore4", recentscore3);
        PlayerPrefs.SetString("RTime4", rtime3);
        PlayerPrefs.SetString("RName4", rname3);
        PlayerPrefs.SetInt("RecentScore3", recentscore2);
        PlayerPrefs.SetString("RTime3", rtime2);
        PlayerPrefs.SetString("RName3", rname2);
        PlayerPrefs.SetInt("RecentScore2", recentscore1);
        PlayerPrefs.SetString("RTime2", rtime1);
        PlayerPrefs.SetString("RName2", rname1);
        PlayerPrefs.SetInt("RecentScore1", nowScore);
        PlayerPrefs.SetString("RTime1", nowtime);
        PlayerPrefs.SetString("RName1", playername);
    }

    // UI Input

    public void SetPlayerName()
    {
        playerName = inputText.text;
        nowScore = ScoreKeeper.GetScore();
        string nowTime = nowTime1;
        int highScore1 = PlayerPrefs.GetInt("HighScore1"); int highScore2 = PlayerPrefs.GetInt("HighScore2"); int highScore3 = PlayerPrefs.GetInt("HighScore3"); int highScore4 = PlayerPrefs.GetInt("HighScore4"); int highScore5 = PlayerPrefs.GetInt("HighScore5");
        int recentScore1 = PlayerPrefs.GetInt("RecentScore1"); int recentScore2 = PlayerPrefs.GetInt("RecentScore2"); int recentScore3 = PlayerPrefs.GetInt("RecentScore3"); int recentScore4 = PlayerPrefs.GetInt("RecentScore4"); int recentScore5 = PlayerPrefs.GetInt("RecentScore5");
        string HTime1 = PlayerPrefs.GetString("HTime1"); string HTime2 = PlayerPrefs.GetString("HTime2"); string HTime3 = PlayerPrefs.GetString("HTime3"); string HTime4 = PlayerPrefs.GetString("HTime4"); string HTime5 = PlayerPrefs.GetString("HTime5");
        string RTime1 = PlayerPrefs.GetString("RTime1"); string RTime2 = PlayerPrefs.GetString("RTime2"); string RTime3 = PlayerPrefs.GetString("RTime3"); string RTime4 = PlayerPrefs.GetString("RTime4"); string RTime5 = PlayerPrefs.GetString("RTime5");
        string RName1 = PlayerPrefs.GetString("RName1");
        string RName2 = PlayerPrefs.GetString("RName2");
        string RName3 = PlayerPrefs.GetString("RName3");
        string RName4 = PlayerPrefs.GetString("RName4");
        string HName1 = PlayerPrefs.GetString("HName1");
        string HName2 = PlayerPrefs.GetString("HName2");
        string HName3 = PlayerPrefs.GetString("HName3");
        string HName4 = PlayerPrefs.GetString("HName4");

        makeHighScore(nowScore, highScore1, highScore2, highScore3, highScore4, highScore5, nowTime, HTime1, HTime2, HTime3, HTime4, HTime5,HName1,HName2,HName3,HName4, playerName);
        makeRecentScore(nowScore, recentScore1, recentScore2, recentScore4, recentScore5, nowTime, RTime1, RTime2, RTime3, RTime4, RName1,RName2,RName3,RName4,playerName);
        SceneManager.LoadScene("Menu");

    }
    public void StartNewGame() {

        mapSelect.SetActive(true);
	}

    public void StartNewTemple()
    {

        SceneManager.LoadScene("Level3");
    }
    public void StartNewCastle()
    {

        SceneManager.LoadScene("Level5");
    }

    public void StartNewCity()
    {

        SceneManager.LoadScene("Level4");
    }

    public void StartNewLAB()
    {

        SceneManager.LoadScene("Level6");
    }

    public void LeaderBoard()
    {
        SceneManager.LoadScene("LeaderBoard");
    }

	public void ReturnToMainMenu() {
        
        SceneManager.LoadScene ("Menu");
	}

    public void Quit()
    {
       
        SceneManager.LoadScene("Menu");
    }

}
