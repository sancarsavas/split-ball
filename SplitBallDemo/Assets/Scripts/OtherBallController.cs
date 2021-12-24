using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OtherBallController : MonoBehaviour
{
    public static OtherBallController instance;

    private float jumpSpeed = 12f;
    public bool isJumped;

    

    
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
        isJumped = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        // Check touch obstacle
        if(collision.gameObject.tag == "obstacle")
        {
            // stop game
            Time.timeScale = 0;

            // First, check current score is big or not than high score
            if(PlayerController.instance.score > PlayerPrefs.GetInt("highScore"))
            {
                // And we add score to high score
                PlayerPrefs.SetInt("highScore", PlayerController.instance.score);
                PlayerController.instance.highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
            }
            else
            {
                PlayerController.instance.highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
            }

            PlayerController.instance.currentScoreText.text = PlayerController.instance.score.ToString();

            PlayerController.instance.gameOverPanel.SetActive(true);
        }

        if (collision.gameObject.tag == "ground")
        {
            // if touch the ground after jump, reset cam Y pos
            CameraController.instance.camHeight = 5.8f;

            // set isJumped = false
            isJumped = false;
        }

        // if this game object is LeftBall, do some things
        if (this.gameObject.name == "LeftBall")
        {
            

            if (collision.gameObject.tag == "rightBall")
            {
                PlayerController.instance.mainBall.SetActive(true);
                PlayerController.instance.leftBall.SetActive(false);
                PlayerController.instance.rightBall.SetActive(false);

                // set active false other balls and active main ball
                PlayerController.instance.mainBall.transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }

        // if this game object is RightBall, do some things
        if (this.gameObject.name == "RightBall")
        {
            if (collision.gameObject.tag == "leftBall")
            {
                PlayerController.instance.mainBall.SetActive(true);
                PlayerController.instance.leftBall.SetActive(false);
                PlayerController.instance.rightBall.SetActive(false);

                // set active false other balls and active main ball
                PlayerController.instance.mainBall.transform.position = new Vector3(0, transform.position.y, transform.position.z);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check which game object controlling the script
        if(this.gameObject.name == "LeftBall" || this.gameObject.name == "RightBall")
        {
            
            // if touch jump box
            if (other.gameObject.tag == "jump" && !isJumped)
            {
                Rigidbody rigi = GetComponent<Rigidbody>();
                rigi.velocity = Vector3.up * jumpSpeed;

                // Set isJumped = true
                isJumped = true;
                // if touch jump, set camera Y pos
                CameraController.instance.camHeight = 10.0f;
                print("touch to jump");
            }
        }

        if(other.gameObject.tag == "gold")
        {
            Destroy(other.gameObject);
            PlayerController.instance.score += 1;
        }

        if(other.gameObject.tag == "finish")
        {
            Time.timeScale = 0;
            PlayerController.instance.levelCompletePanel.SetActive(true);

            // First, check current score is big or not than high score
            if (PlayerController.instance.score > PlayerPrefs.GetInt("highScore"))
            {
                // And we add score to high score
                PlayerPrefs.SetInt("highScore", PlayerController.instance.score);
                PlayerController.instance.highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
            }
            else
            {
                PlayerController.instance.highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
            }

            PlayerController.instance.currentScoreText.text = PlayerController.instance.score.ToString();
        }

        // Check balls are down
        if(other.gameObject.tag == "downarea")
        {
            Time.timeScale = 0;
            PlayerController.instance.gameOverPanel.SetActive(true);

            // First, check current score is big or not than high score
            if (PlayerController.instance.score > PlayerPrefs.GetInt("highScore"))
            {
                // And we add score to high score
                PlayerPrefs.SetInt("highScore", PlayerController.instance.score);
                PlayerController.instance.highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
            }
            else
            {
                PlayerController.instance.highScoreText.text = PlayerPrefs.GetInt("highScore").ToString();
            }

            PlayerController.instance.currentScoreText.text = PlayerController.instance.score.ToString();
        }
        
    }

    
}
