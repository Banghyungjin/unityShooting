using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class Spawner : MonoBehaviour {

	public bool devMode;

	public Wave[] waves;
	public Enemy[] enemyList;

	LivingEntity playerEntity;
	Transform playerT;

	public GameObject[] spawnList;
    public GameObject playerSpawnPoint;
    public GameObject nextSpawnPoint;
	public GameObject nextLevelUI;
	public spArray[] spArrays;
	private Text enemyRemain;
	public Text gameOverScoreUI;
	public Text scoreUI;
	public GameObject joyStick;
	public GameObject joyStick2;

	Wave currentWave;
	int currentWaveNumber = 0;
	int enemiesRemainingToSpawn;
	int enemiesRemainingAlive;
	float nextSpawnTime;

	public GameObject map;

	float timeBetweenCampingChecks = 2;
	float campThresholdDistance = 1.5f;
	float nextCampCheckTime;
	Vector3 campPositionOld;
	bool isCamping;

	bool isDisabled;

	public event System.Action<int> OnNewWave;

	void Start() {
		playerEntity = FindObjectOfType<Player> ();
		playerT = playerEntity.transform;

		nextCampCheckTime = timeBetweenCampingChecks + Time.time;
		campPositionOld = playerT.position;
		playerEntity.OnDeath += OnPlayerDeath;
		enemyRemain = GameObject.Find("EnemyRemain").GetComponent<Text>();
		NextWave ();
	}

	void Update() {
		if (!isDisabled) {
			if (Time.time > nextCampCheckTime) {
				nextCampCheckTime = Time.time + timeBetweenCampingChecks;

				isCamping = (Vector3.Distance (playerT.position, campPositionOld) < campThresholdDistance);
				campPositionOld = playerT.position;
			}

			if ((enemiesRemainingToSpawn > 0 || currentWave.infinite) && Time.time > nextSpawnTime) {
				enemiesRemainingToSpawn--;
				nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;
				StartCoroutine ("SpawnEnemy");
			}
		}

		if (devMode) {
			if (Input.GetKeyDown(KeyCode.Return)) {
				StopCoroutine("SpawnEnemy");
				foreach (Enemy enemy in FindObjectsOfType<Enemy>()) {
					GameObject.Destroy(enemy.gameObject);
				}
				NextWave();
			}
		}
		enemyRemain.text = "Enemy : " + enemiesRemainingAlive;
	}

	IEnumerator SpawnEnemy() {
		float spawnDelay = 1;
        float tileFlashSpeed = 4;

        //Transform spawnTile = map.GetRandomOpenTile ();
        /*
        if (isCamping) {
			spawnTile = map.GetTileFromPosition(playerT.position);
		}*/
        int spawnPointNumber = Random.Range(0, spawnList.Length);
		nextSpawnPoint = spawnList[spawnPointNumber];

		Material tileMat = nextSpawnPoint.GetComponent<Renderer> ().material;
		Color initialColour = Color.white;
		Color flashColour = Color.red;
		float spawnTimer = 0;

		while (spawnTimer < spawnDelay) {

			tileMat.color = Color.Lerp(initialColour,flashColour, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));

			spawnTimer += Time.deltaTime;
			yield return null;
		}
        Vector3 pos = nextSpawnPoint.transform.position;
		int enemyNumber = Random.Range(0, enemyList.Length);
		Enemy spawnedEnemy = Instantiate(enemyList[enemyNumber], pos + Vector3.up, Quaternion.identity) as Enemy;
		spawnedEnemy.OnDeath += OnEnemyDeath;
		spawnedEnemy.SetCharacteristics (currentWave.moveSpeed, currentWave.hitsToKillPlayer, currentWave.enemyHealth, currentWave.skinColour);
	}

	void OnPlayerDeath() {
		isDisabled = true;
	}

	void OnEnemyDeath() {
		enemiesRemainingAlive --;
		if (enemiesRemainingAlive == 0) {
			foreach (Enemy enemy in FindObjectsOfType<Enemy>())
			{
				GameObject.Destroy(enemy.gameObject);
			}
			joyStick.SetActive(false);
			joyStick2.SetActive(false);
			Cursor.visible = true;
			Time.timeScale = 0;
			//inputField.SetActive(true);
			//StartCoroutine(Fade(Color.clear, new Color(0, 0, 0, .95f), 1));
			nextLevelUI.SetActive(true);
			NextWave();	
		}
	}

	void ResetPlayerPosition() {
		playerT.position = playerSpawnPoint.transform.position;
	}

	public void NextWave() {
		foreach (Enemy enemy in FindObjectsOfType<Enemy>())
		{
			GameObject.Destroy(enemy.gameObject);
		}
		if (currentWaveNumber > 0) {
			AudioManager.instance.PlaySound2D("Level Complete");
			//ResetPlayerPosition();
		}
		currentWaveNumber++;
		if (currentWaveNumber - 1 < waves.Length)
		{
			currentWave = waves[currentWaveNumber - 1];

			enemiesRemainingToSpawn = currentWave.enemyCount;
			enemiesRemainingAlive = enemiesRemainingToSpawn;

			if (OnNewWave != null)
			{
				AudioManager.instance.PlaySound2D("Level Complete");
				OnNewWave(currentWaveNumber);

			}
			//ResetPlayerPosition();
		}
		else { currentWaveNumber = waves.Length;
			AudioManager.instance.PlaySound2D("Level Complete");
		}
	}

	[System.Serializable]
	public class Wave {
		public bool infinite;
		public int enemyCount;
		public float timeBetweenSpawns;
		public float moveSpeed;
		public int hitsToKillPlayer;
		public float enemyHealth;
		public Color skinColour;
	}

    public class spArray
    {
        public GameObject nextSpawnPoint;
    }

	public class SpawningPoint
    {
		public GameObject SpawnPoint;
	}



}
