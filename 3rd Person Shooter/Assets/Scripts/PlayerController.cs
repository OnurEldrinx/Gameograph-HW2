using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{


    private CharacterController characterController;
    private PlayerControls playerControlInputs;

    private Vector2 inputValue;
    private Vector3 movement;

    private float speed = 10f;

    private int health = 50;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootingPoint;

    private GameObject bullet;

    private Animator animator;

    private void Awake()
    {

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
    void Update()
    {

        characterController.SimpleMove(movement * speed);

        
   
    }

    

    public void Shoot(InputAction.CallbackContext context)
    {
       

        if (context.ReadValueAsButton())
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
                bullet.GetComponent<Rigidbody>().AddForce(Vector3.forward * 100, ForceMode.Impulse);

            }





        }
        else
        {

            animator.SetBool("isShooting",false);

        }

        

        

    }

    


    public void movementVector(InputAction.CallbackContext context)
    {


        inputValue = context.ReadValue<Vector2>();


        // Player will be able to move on only X axis, not Z axis
        movement = new Vector3(inputValue.x,0,0);

        if (inputValue.y != 0)
        {

            animator.SetBool("isRunning",true);

        }
        else
        {

            animator.SetBool("isRunning", false);


        }


        if (inputValue.x < 0 )
        {

            animator.SetBool("isLeft",true);

        }else if (inputValue.x > 0)
        {

            animator.SetBool("isRight",true);

        }
        else
        {
            animator.SetBool("isLeft", false);
            animator.SetBool("isRight", false);


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
