using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenerateObstacles : MonoBehaviour
{
    public static GenerateObstacles instance;
    public List<GameObject> Obstacles = new List<GameObject>();
    public GameObject Warning;
    private float spawnInterval = 4f;
    private float limitSuperior;
    private float limitInferior;
    public AudioSource hitSound;
    public AudioSource hurtSound;

    public GameObject player;
    private PlayerMovement playerScript;

    private void Awake()
    {
        playerScript = player.GetComponent<PlayerMovement>();

        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }
    private void Start()
    {
        SetMinMax();
        StartCoroutine(GenerateObstaclesAndWarningSignal());
    }
    IEnumerator GenerateObstaclesAndWarningSignal()
    {
        while (true)
        {
            GameObject _obstacle = Instantiate(Obstacles[Random.Range(0, Obstacles.Count)],
            new Vector3(transform.position.x, Random.Range(limitInferior, limitSuperior), 0f), Quaternion.identity);
            _obstacle.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0);

            GameObject _warningSign = Instantiate(Warning, new Vector3(transform.position.x - 3, _obstacle.transform.position.y, 0f), Quaternion.identity);

            yield return new WaitForSeconds(1f);
            Destroy(_warningSign);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -(bounds.y * 0.9f);
        limitSuperior = (bounds.y * 0.9f);
    }
    public void ManagerObstacle(ControllerObstacle obstacle_script, PlayerMovement player_script = null)
    {
        if (player_script == null)
        {
            Destroy(obstacle_script.gameObject);
            return;
        }

        int lives = player_script.player_lives;
        int live_changer = obstacle_script.obstacles.lifeChangePlayer;
        lives -= live_changer;
        print(lives);

        if (lives <= 0)
        {
            lives = 0;
        }
        player_script.player_lives = lives;
        hitSound.Play();
        hurtSound.Play();
        Destroy(obstacle_script.gameObject);
    }
}
