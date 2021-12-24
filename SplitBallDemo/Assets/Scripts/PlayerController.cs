using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    public GameObject leftBall;
    public GameObject rightBall;

    public Rigidbody mainRigi;
    public Rigidbody leftRigi;
    public Rigidbody rightRigi;

    [Header("Ball Settings")]
    public float speed = 2f;
    [SerializeField]
    private float otherBallSpeed = 0.5f;
    [SerializeField]
    private float limitX = 1.5f;


    public GameObject mainBall;

    public bool moreBalls;

    public int score;
    public TMP_Text scoreText;
    public int currentScore;
    public TMP_Text currentScoreText;
    public int highScore;
    public TMP_Text highScoreText;

    public GameObject gameOverPanel;
    public GameObject levelCompletePanel;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // On start, set active false other balls
        leftBall.SetActive(false);
        rightBall.SetActive(false);

        moreBalls = false;

        scoreText.text = "Score: 0";

        // Set time scale 1
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

        Swipe();

        scoreText.text = "Score: " + score.ToString();

        // Move forward
        transform.Translate(0, 0, speed * Time.deltaTime);

        
    }

    public void Swipe()
    {
        if (Input.GetMouseButton(0))
        {
            float mousePos = Input.mousePosition.x;

            // Check mouse pos
            if(mousePos > Screen.width / 2)
            {
                mainBall.SetActive(false);
                leftBall.SetActive(true);
                rightBall.SetActive(true);

                moreBalls = true;

                leftBall.transform.position = Vector3.Lerp(leftBall.transform.position, new Vector3(-limitX, leftBall.transform.position.y, transform.position.z), otherBallSpeed * Time.deltaTime);
                rightBall.transform.position = Vector3.Lerp(rightBall.transform.position, new Vector3(limitX, rightBall.transform.position.y, transform.position.z), otherBallSpeed * Time.deltaTime);
            }

            // Check have one ball or more balls
            if (moreBalls)
            {
                if (mousePos < Screen.width / 2)
                {
                    leftBall.transform.position = Vector3.Lerp(leftBall.transform.position, new Vector3(0, leftBall.transform.position.y, transform.position.z), otherBallSpeed * Time.deltaTime);
                    rightBall.transform.position = Vector3.Lerp(rightBall.transform.position, new Vector3(0, rightBall.transform.position.y, transform.position.z), otherBallSpeed * Time.deltaTime);
                    
                    
                }
            }
        }
    }


    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

}
