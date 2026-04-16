using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private bool isGameActive;
    private float boundZ = 11.0f;
    private float boundX = 23.0f;
    private int waveNum = 0;
    public GameObject enemySmallPrefab;
    public GameObject enemyMediumPrefab;
    public GameObject enemyBigPrefab;
    public GameObject enemyBossPrefab;
    public GameObject powerUpPrefab;
    private GameManager gameManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemyWave()
    {
        if (FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length == 0 && gameManager.IsGameActive() && waveNum < 5)
        {
            waveNum++;
            switch (waveNum)
            {
                case 1:
                    SpawnWave1();
                    break;

                case 2:
                    SpawnWave2();
                    break;

                case 3:
                    SpawnWave3();
                    break;

                case 4:
                    SpawnWave4();
                    break;

                case 5:
                    SpawnWave5();
                    break;

                default:
                    break;
            }
        }
        else if (FindObjectsByType<Enemy>(FindObjectsSortMode.None).Length == 0 && waveNum == 5)
        {
            gameManager.Victory();
        }
    }

    public int WaveNumber()
    {
        return waveNum;
    }

    public void StartSpawning()
    {
        
        InvokeRepeating(nameof(SpawnEnemyWave), 0.0f, 0.1f);
    }

    void SpawnPowerUp()
    {
        Instantiate(powerUpPrefab, new Vector3(Random.Range(-boundX, boundX), 0, Random.Range(-boundZ, boundZ)), powerUpPrefab.transform.rotation);
    }

    void SpawnEnemy(GameObject enemyPrefab, Vector3 position)
    {
        Instantiate(enemyPrefab, position, enemyPrefab.transform.rotation);
    }

    void SpawnWave1()
    {
        SpawnEnemy(enemySmallPrefab, new Vector3(0, 0, 10));
        SpawnEnemy(enemySmallPrefab, new Vector3(-10, 0, 10));
        SpawnEnemy(enemySmallPrefab, new Vector3(10, 0, 10));
        SpawnEnemy(enemySmallPrefab, new Vector3(-5, 0, 7.5f));
        SpawnEnemy(enemySmallPrefab, new Vector3(5, 0, 7.5f));
    }

    void SpawnWave2()
    {
        SpawnEnemy(enemyMediumPrefab, new Vector3(0, 0, 10));
        SpawnEnemy(enemyMediumPrefab, new Vector3(-10, 0, 10));
        SpawnEnemy(enemyMediumPrefab, new Vector3(10, 0, 10));

        SpawnEnemy(enemySmallPrefab, new Vector3(-7.5f, 0, 10));
        SpawnEnemy(enemySmallPrefab, new Vector3(7.5f, 0, 10));
        SpawnEnemy(enemySmallPrefab, new Vector3(-5, 0, 7.5f));
        SpawnEnemy(enemySmallPrefab, new Vector3(5, 0, 7.5f));
    }

    void SpawnWave3()
    {
        SpawnPowerUp();

        SpawnEnemy(enemyBigPrefab, new Vector3(0, 0, 10));

        SpawnEnemy(enemyMediumPrefab, new Vector3(-10, 0, 10));
        SpawnEnemy(enemyMediumPrefab, new Vector3(10, 0, 10));
        SpawnEnemy(enemyMediumPrefab, new Vector3(-2.5f, 0, 10));
        SpawnEnemy(enemyMediumPrefab, new Vector3(2.5f, 0, 10));

        SpawnEnemy(enemySmallPrefab, new Vector3(-7.5f, 0, 10));
        SpawnEnemy(enemySmallPrefab, new Vector3(7.5f, 0, 10));
        SpawnEnemy(enemySmallPrefab, new Vector3(-5, 0, 7.5f));
        SpawnEnemy(enemySmallPrefab, new Vector3(5, 0, 7.5f));
        
    }

    void SpawnWave4()
    {
        SpawnPowerUp();
        
        SpawnEnemy(enemyBigPrefab, new Vector3(0, 0, 10));
        SpawnEnemy(enemyBigPrefab, new Vector3(-10, 0, 10));
        SpawnEnemy(enemyBigPrefab, new Vector3(10, 0, 10));
        SpawnEnemy(enemyBigPrefab, new Vector3(-5, 0, 7.5f));
        SpawnEnemy(enemyBigPrefab, new Vector3(5, 0, 7.5f));

        SpawnEnemy(enemyMediumPrefab, new Vector3(-7.5f, 0, 10));
        SpawnEnemy(enemyMediumPrefab, new Vector3(7.5f, 0, 10));
        SpawnEnemy(enemyMediumPrefab, new Vector3(-2.5f, 0, 10));
        SpawnEnemy(enemyMediumPrefab, new Vector3(2.5f, 0, 10));
    }

    void SpawnWave5()
    {
        SpawnEnemy(enemyBossPrefab, new Vector3(0, 0, 10));
    }
}
