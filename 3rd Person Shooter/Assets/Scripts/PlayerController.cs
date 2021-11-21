using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{

    public static PlayerController PlayerInstance;

    private CharacterController characterController;
    private PlayerControls playerControlInputs;

    private Vector2 inputValue;
    private Vector3 movement;

    public int score;

    private float speed = 5f;

    public float health = 100;

    public bool isDead;
    public bool levelDone;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootingPoint;

    private GameObject bullet;

    private Animator animator;

    [SerializeField] private Text healthText;
    [SerializeField] private Text scoreText;

    private Rigidbody bulletRb;


    private void Awake()
    {
        PlayerInstance = this;

        isDead = false;
        levelDone = false;

        bulletRb = bulletPrefab.GetComponent<Rigidbody>();
        playerControlInputs = new PlayerControls();
        characterController = GetComponent<CharacterController>();
        animator = gameObject.GetComponentInChildren<Animator>();

        playerControlInputs.Player.Move.started += movementVector;
        playerControlInputs.Player.Move.performed += movementVector;
        playerControlInputs.Player.Move.canceled += movementVector;

        playerControlInputs.Player.Shoot.started += Shoot;
        playerControlInputs.Player.Shoot.performed += Shoot;
        playerControlInputs.Player.Shoot.canceled += Shoot;


    }

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        healthText.text = "Health : " + (int)PlayerInstance.health;
        scoreText.text = "Score : " + (int)PlayerInstance.score;


        if (!isDead && !levelDone)
        {

            characterController.SimpleMove(movement * speed);
            Debug.Log(movement.x +" , "+movement.y +" , "+movement.z);

        }
        

        if (PlayerInstance.health<=0)
        {

            Debug.Log("Game Over");
            animator.SetBool("isDead",true);
            isDead = true;
            
            

        }
   
    }

    

    public void Shoot(InputAction.CallbackContext context)
    {
       

        if (context.ReadValueAsButton() && !isDead && !levelDone)
        {
            Debug.Log("Shoot");
            
            
            // Instantiate(bulletPrefab,shootingPoint.transform.position,bulletPrefab.transform.rotation).GetComponent<Rigidbody>().AddForce(Vector3.forward * 100,ForceMode.Impulse);

            bullet = ObjectPool.pool.getObjectFromPool();
            if(bullet != null)
            {

                bullet.transform.position = shootingPoint.transform.position;
                bullet.transform.rotation = bulletPrefab.transform.rotation;
                animator.SetBool("isShooting",true);
                bullet.SetActive(true);
                bullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * 50,ForceMode.Impulse);

            }





        }
        else
        {

            animator.SetBool("isShooting",false);

        }

        

        

    }

    


    public void movementVector(InputAction.CallbackContext context)
    {


        if (!isDead && !levelDone)
        {

            inputValue = context.ReadValue<Vector2>();


            // Player will be able to move on only X axis, not Z axis
            movement = new Vector3(inputValue.x, 0,inputValue.y);

            if (inputValue.y != 0)
            {

                animator.SetBool("isRunning", true);

            }
            else
            {

                animator.SetBool("isRunning", false);


            }

            /*
            if (inputValue.x < 0)
            {

                animator.SetBool("isLeft", true);

            }
            else if (inputValue.x > 0)
            {

                animator.SetBool("isRight", true);

            }
            else
            {
                animator.SetBool("isLeft", false);
                animator.SetBool("isRight", false);


            }
            */
        
        
        }


    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Finish")
        {

            Debug.Log("You finished this level");
            levelDone = true;
            animator.SetBool("isRunning",false);

        }


    }

    private void OnEnable()
    {

        playerControlInputs.Enable();

    }

    private void OnDisable()
    {

        playerControlInputs.Disable();

    }

    

}
