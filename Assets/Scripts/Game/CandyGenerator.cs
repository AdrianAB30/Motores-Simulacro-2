using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandyGenerator : MonoBehaviour
{
    public static CandyGenerator instance;
    public List<GameObject> Candies = new List<GameObject>();
    private float time_to_create = 2f;
    private float actual_time = 0f;
    private float limitSuperior;
    private float limitInferior;
    public List<GameObject> actual_candies = new List<GameObject>();
    public AudioSource audioEat;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }
    void Start()
    {
        SetMinMax();
    }

    void Update()
    {
        actual_time += Time.deltaTime;
        if (time_to_create <= actual_time)
        {
            GameObject candy = Instantiate(Candies[Random.Range(0, Candies.Count)],
            new Vector3(transform.position.x, Random.Range(limitInferior, limitSuperior), 0f), Quaternion.identity);
            candy.GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0);
            actual_time = 0f;
            actual_candies.Add(candy);
        }
    }

    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -(bounds.y * 0.9f);
        limitSuperior = (bounds.y * 0.9f);
    }

    public void ManageCandy(CandyController candy_script, PlayerMovement player_script = null)
    {
        if (player_script == null)
        {
            Destroy(candy_script.gameObject);
            return;
        }
        int point = player_script.playerScore.score;
        int live_changer = candy_script.candiesPoints.pointsAdd;
        point += live_changer;
        player_script.playerScore.score += candy_script.candiesPoints.pointsAdd;
        audioEat.Play();
        Destroy(candy_script.gameObject);
    }
}
