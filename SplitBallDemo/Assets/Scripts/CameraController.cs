using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public GameObject player;


    [Header("Camera Settings")]
    public float camSpeed;
    public float camHeight = 5.8f;
    public float offset = 6f;

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

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, camHeight, player.transform.position.z - offset), camSpeed * Time.deltaTime);
    }
}
