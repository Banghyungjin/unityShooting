using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

	public GameObject allUI;
    public GameObject joyStick;
    public GameObject joyStick2;
    public Image fadePlane;
	public GameObject gameOverUI;

	public RectTransform newWaveBanner;
	public Text newWaveTitle;
	public Text newWaveEnemyCount;
	public Text scoreUI;
	public Text gameOverScoreUI;
	public RectTransform healthBar;

	Spawner spawner;
	Player player;

	void Start () {
		allUI.SetActive (true);
		player = FindObjectOfType<Player> ();
		player.OnDeath += OnGameOver;
	}

	void Awake() {
		spawner = FindObjectOfType<Spawner> ();
		spawner.OnNewWave += OnNewWave;
	}

	void Update() {
		scoreUI.text = ScoreKeeper.score.ToString("D6");
		float healthPercent = 0;
		if (player != null) {
			healthPercent = player.health / player.startingHealth;
		}
		healthBar.localScale = new Vector3 (healthPercent, 1, 1);
	}

	void OnNewWave(int waveNumber) {
		string[] numbers = { "One", "Two", "Three", "Four", "Five" };
		newWaveTitle.text = "- Wave " + numbers [waveNumber - 1] + " -";
		string enemyCountString = ((spawner.waves [waveNumber - 1].infinite) ? "Infinite" : spawner.waves [waveNumber - 1].enemyCount + "");
		newWaveEnemyCount.text = "Enemies: " + enemyCountString;

		StopCoroutine ("AnimateNewWaveBanner");
		StartCoroutine ("AnimateNewWaveBanner");
	}
		
	void OnGameOver() {
        int nowScore = ScoreKeeper.GetScore();
        int highScore1 = PlayerPrefs.GetInt("HighScore1");
        int highScore2 = PlayerPrefs.GetInt("HighScore2");
        int highScore3 = PlayerPrefs.GetInt("HighScore3");
        int recentScore1 = PlayerPrefs.GetInt("RecentScore1");
        int recentScore2 = PlayerPrefs.GetInt("RecentScore2");
        makeHighScore(nowScore, highScore1, highScore2, highScore3);
        makeRecentScore(nowScore, recentScore1, recentScore2);
        joyStick.SetActive(false);
        joyStick2.SetActive(false);
        Cursor.visible = true;
		StartCoroutine(Fade (Color.clear, new Color(0,0,0,.95f),1));
		gameOverScoreUI.text = scoreUI.text;
		scoreUI.gameObject.SetActive (false);
		healthBar.transform.parent.gameObject.SetActive (false);
        
		gameOverUI.SetActive (true);
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
    void makeHighScore(int nowScore, int highscore1, int highscore2, int highscore3)
    {
        if (nowScore >highscore1)
        {
            PlayerPrefs.SetInt("HighScore3", highscore2);
            PlayerPrefs.SetInt("HighScore2", highscore1);
            PlayerPrefs.SetInt("HighScore1", nowScore);
        }
        else if (nowScore > highscore2)
        {
            PlayerPrefs.SetInt("HighScore3", highscore2);
            PlayerPrefs.SetInt("HighScore2", nowScore);
        }
        else if (nowScore > highscore3)
        {
            PlayerPrefs.SetInt("HighScore3", nowScore);
        }
    }
    void makeRecentScore(int nowScore, int recentscore1, int recentscore2)
    {
        PlayerPrefs.SetInt("RecentScore3", recentscore2);
        PlayerPrefs.SetInt("RecentScore2", recentscore1);
        PlayerPrefs.SetInt("RecentScore1", nowScore);
    }

	// UI Input
	public void StartNewGame() {
        
		SceneManager.LoadScene ("Level1");
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
