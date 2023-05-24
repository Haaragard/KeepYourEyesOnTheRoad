using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController controller;
    private int LayerToHit;

    public float speed;
    public float jumpHeight;
    private float jumpVelocity;
    public float gravity;

    public float rayRadius;

    public float horizontalSpeed;
    private bool isMovingLeft;
    private bool isMovingRight;

    public Animator anim;
    public bool isDead;

    private GameController gc;

    public GameObject camera;

    public int coins;
    
    // Start is called before the first frame update
    void Start()
    {
        this.controller = GetComponent<CharacterController>();
        this.gc = FindObjectOfType<GameController>();
        this.LayerToHit = this.GetObstacle();

        this.StartRunning();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.forward * speed;

        if(controller.isGrounded)
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                jumpVelocity = jumpHeight;
            }

            this.MoveSides();
        }
        else
        {
            jumpVelocity -= gravity;
        }

        direction.y = jumpVelocity;

        Vector3 moveDirection = direction * Time.deltaTime;

        controller.Move(moveDirection);
        this.updateCameraPosition();
    }
    
    private void updateCameraPosition()
    {
        Vector3 newCameraPosition = new Vector3(
            this.transform.position.x,
            this.transform.position.y + 5f,
            this.transform.position.z - 5f
        );
        this.camera.gameObject.transform.position = newCameraPosition;
    }

    private void MoveSides()
    {
        if (
            (
                Input.GetKeyDown(KeyCode.LeftArrow)
                || Input.GetKeyDown(KeyCode.A)
            )
            && transform.position.x > -8f && !isMovingLeft)
        {
            isMovingLeft = true;
            StartCoroutine(LeftMove());
        }
        else if (
            (
                Input.GetKeyDown(KeyCode.RightArrow)
                || Input.GetKeyDown(KeyCode.D)
            )
            && transform.position.x < 8f && !isMovingRight)
        {
            isMovingRight = true;
            StartCoroutine(RightMove());
        }
    }

    IEnumerator LeftMove()
    {
        for(float i = 0; i < 10; i++)
        {
            controller.Move(Vector3.left * Time.deltaTime * horizontalSpeed);
            yield return null;
        }

        isMovingLeft = false;
    }

    IEnumerator RightMove()
    {
        for(float i = 0; i < 10; i++)
        {
            controller.Move(Vector3.right * Time.deltaTime * horizontalSpeed);
            yield return null;
        }

        isMovingRight = false;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        LayerMask maskObstacle = this.GetObstacle();
        int layerHit = 1 << hit.gameObject.layer;
        bool hitLayerObstacle = this.HitObstacle();

        if ((maskObstacle & layerHit) != 0 && hitLayerObstacle)
        {
            this.Die();
        }
    }

    private LayerMask GetObstacle()
    {
        return LayerMask.GetMask("obstacle");
    }

    private bool HitObstacle()
    {
        return Physics.Raycast(this.transform.position, this.transform.TransformDirection(Vector3.forward), this.rayRadius, this.LayerToHit);
    }

    void GameOver()
    {
        this.StopRunning();
        gc.ShowGameOver();
    }

    private void Die()
    {
        anim.SetTrigger("die");

        speed = 0;
        jumpHeight = 0;
        horizontalSpeed = 0;
        isDead = true;

        this.StopRunning();

        Invoke("GameOver", 0.3f);
    }

    private void StartRunning()
    {
        this.anim.SetBool("running", true);
    }

    private void StopRunning()
    {
        this.anim.SetBool("running", false);
    }

    public IEnumerator AddACoin()
    {
        this.AddCoin();

        yield return null;
    }

    private void AddCoin()
    {
        this.coins++;
    }
}
