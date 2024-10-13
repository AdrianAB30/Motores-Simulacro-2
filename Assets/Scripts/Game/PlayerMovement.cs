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
    [SerializeField] private float speedSpecial;
    [SerializeField] private float lessSpeed;
    private float limitSuperior;
    private float limitInferior;
    public int player_lives = 4;
    public bool isInvulnerable = false;
    [SerializeField] private Collider2D myCol;
    private Vector2 movement;
    [SerializeField] public PlayerSOS playerScore;

    [Header("UI Data")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text lifeText;

    [Header("Sounds Especial")]
    [SerializeField] private AudioSource mushroom;
    [SerializeField] private AudioSource coffe;
    void Start()
    {
        SetMinMax();
        myRB = GetComponent<Rigidbody2D>();
        ResetScore();
    }

    void FixedUpdate()
    {
        AplyPhysics();
    }
    private void Update()
    {
        UpdateScore();
        UpdateLife();

        if(player_lives <= 0)
        {
            GameManager.instance.GameOver();
        }
        ElectionGameplay();
    }
    private void MovePlayerWithMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        float targetY = Mathf.Clamp(mousePosition.y, limitInferior, limitSuperior);
        Vector2 targetPosition = new Vector2(transform.position.x, targetY);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
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
        else if (collision.CompareTag("EspecialCoffe"))
        {
            speed += speedSpecial;
            coffe.Play();
            Destroy(collision.gameObject);
            if (speed >= 15f)
            {
                speed = 15f;
            }
        }
        else if (collision.CompareTag("EspecialMushroom"))
        {
            speed -= lessSpeed;
            mushroom.Play();
            Destroy(collision.gameObject);
            if (speed <= 1f)
            {
                speed = 1f;
            }
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
    private void ResetScore()
    {
        playerScore.ResetScore();
        UpdateScore();
    }
    private void UpdateLife()
    {
        lifeText.text = "LIVES: " + player_lives.ToString();
    }
    private void ElectionGameplay()
    {
        string controlType = PlayerPrefs.GetString("ControlType", "Keyboard");
        if(controlType == "Mouse")
        {
            MovePlayerWithMouse();
        }
        else if(controlType == "Keyboard")
        {
            AplyPhysics();
        }
    }
}
