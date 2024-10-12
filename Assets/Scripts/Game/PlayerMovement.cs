using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Data")]
    private Rigidbody2D myRB;
    [SerializeField] private Transform startPosition;
    [SerializeField] private float speed;
    private float limitSuperior;
    private float limitInferior;
    public int player_lives = 4;
    public bool isInvulnerable = false;
    [SerializeField] private Collider2D myCol;
    private Vector2 movement;
    [SerializeField] public PlayerSOS playerScore;

    [Header("UI Data")]
    [SerializeField] private TMP_Text scoreText;

    void Start()
    {
        SetMinMax();
        myRB = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        AplyPhysics();
    }
    private void Update()
    {
        UpdateScore();
    }
    private void AplyPhysics()
    {
        if (movement.y > 0 && transform.position.y < limitSuperior)
        {
            myRB.velocity = new Vector2(0f, speed);
        }
        else if (movement.y < 0 && transform.position.y > limitInferior)
        {
            myRB.velocity = new Vector2(0f, -speed);
        }
        else
        {
            myRB.velocity = Vector2.zero;
        }
    }
    public void OnMovement(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
    }

    void SetMinMax()
    {
        Vector3 bounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        limitInferior = -bounds.y;
        limitSuperior = bounds.y;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Candy"))
        {
            CandyGenerator.instance.ManageCandy(collision.gameObject.GetComponent<CandyController>(),this);
        }
        else if (collision.CompareTag("Especial"))
        {

        }
        else if (collision.CompareTag("Obstacle"))
        {
            if(!isInvulnerable)
            {
                GenerateObstacles.instance.ManagerObstacle(collision.gameObject.GetComponent<ControllerObstacle>(),this);
                transform.position = startPosition.position;
                StartCoroutine(Invelnerability());
            }
        }
    }
    IEnumerator Invelnerability()
    {
        myCol.enabled = false;
        isInvulnerable = true;
        yield return new WaitForSeconds(1f);
        myCol.enabled = true;
        isInvulnerable = false;
    }
    private void UpdateScore()
    {
        scoreText.text = "SCORE: " + playerScore.score.ToString();
    }
}
