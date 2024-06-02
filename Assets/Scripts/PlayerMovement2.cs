using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement2 : MonoBehaviour
{
    public static PlayerMovement2 instance;

    public Camera playerCamera;
    public float walkSpeed = 6f;
    public float runSpeed = 12f;
    public float jumpPower = 7f;
    public float gravity = 10f;
    public float lookSpeed = 2f;
    public float lookSpeedX = 600f; // Camera sensitivity for X-axis
    public float lookSpeedY = 600f; // Camera sensitivity for Y-axis
    public float lookXLimit = 45f;
    public float defaultHeight = 1f;
    public float crouchHeight = 1f;
    public float crouchSpeed = 3f;
    public float dashDistance = 5f; // Distance to dash
    public float dashDuration = 0.2f; // Duration of the dash
    public float dashCooldown = 2f; // Cooldown between dashes
    public SpriteRenderer theSR;
    public int playerDamage = 10; // Player's damage value
    public GameObject bulletsplash;
    public GameObject crosshair;
    public GameObject dashParticle; // Reference to the particle system GameObject for dash effects
    public int playerHealth = 10;

    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private float rotationX = 0;
    private bool isDashing = false;
    private float dashTimer = 0f;
    private float dashCooldownTimer = 0f;
    public GameObject UI;
    private bool canMove = true;
    private TextMeshProUGUI textMeshPro;
    public RoomGenerator currentRoom;
    Animator anim;

    RaycastHit hit;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        canMove = true;
        anim = GetComponent<Animator>();
        anim.SetBool("walk", false);
        anim.SetBool("attack", false);
    }

    void Update()
    {
        // Player movement
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if(Input.GetAxisRaw("Horizontal")!=0 || Input.GetAxisRaw("Vertical")!=0)
            anim.SetBool("walk", true);

        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
            anim.SetBool("walk", false);

        /* 
         if(moveDirection.y ==0 && moveDirection.x ==0 && moveDirection.z ==0)
             anim.SetBool("walk", false);
        */

        if (Input.GetKeyDown(KeyCode.P))
        {
            PauseUnpause();
        }

        if (Input.GetButton("Jump") && canMove && characterController.isGrounded)
        {
            moveDirection.y = jumpPower;
        }
        else
        {
            moveDirection.y = movementDirectionY;
        }

        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.R) && canMove)
        {
            characterController.height = crouchHeight;
            walkSpeed = crouchSpeed;
            runSpeed = crouchSpeed;
        }
        else
        {
            characterController.height = defaultHeight;
            walkSpeed = 6f;
            runSpeed = 12f;
        }

        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("attack", true);
            Attack();
            anim.SetBool("attack", false);
        }

        // Dash
        if (Input.GetKeyDown(KeyCode.Q) && !isDashing && dashCooldownTimer <= 0) // Changed to check for Q
        {
            isDashing = true;
            dashTimer = dashDuration;
            dashCooldownTimer = dashCooldown;
            Dash();
        }

        if (isDashing)
        {
            float dashDistanceThisFrame = dashDistance * Time.deltaTime / dashDuration;
            characterController.Move(transform.forward * dashDistanceThisFrame);
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                isDashing = false;
            }
        }

        // Cooldown timer
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }


        if (!theSR.flipX && moveDirection.x > 0)
        {
            theSR.flipX = true;
        }
        else if (theSR.flipX && moveDirection.x < 0)
        {
            theSR.flipX = false;
        }

        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;

            //print(-Input.GetAxis("Mouse Y"));

            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);

            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            //transform.rotation *= Quaternion.Euler(0, axisx * lookSpeed, 0);

            SFX.instance.PlaySFX(2); // footsteps

            //anim.SetBool("walk", true);

            //axisx = 0;
            // Camera rotation
            //      if (canMove)
            // {
            //     float mouseX = Input.GetAxis("Mouse X") * lookSpeedX * Time.deltaTime;
            //      transform.Rotate(Vector3.up * mouseX);

            //     rotationX += -Input.GetAxis("Mouse Y") * lookSpeedY * Time.deltaTime;
            //    rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            //     playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            // }

        }

        if (Physics.Raycast(theSR.transform.position, theSR.transform.forward, out hit, 20f))
        {
            Debug.DrawRay(theSR.transform.position, theSR.transform.forward * hit.distance, Color.red);
            //crosshair.transform.Translate(theSR.transform.forward * (hit.distance - 1f));
        }
        else
        {
            Debug.DrawRay(theSR.transform.position, theSR.transform.forward * 20f, Color.green);
            //crosshair.transform.Translate(theSR.transform.forward * 10f);
        }

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void Attack()
    {
        // Implement your attack logic here
        Debug.Log("Player attacks!");

        // Detect if the attack hits the dummy
        RaycastHit hit;
        if (Physics.Raycast(theSR.transform.position, theSR.transform.forward, out hit))
        {
            Debug.DrawRay(theSR.transform.position, theSR.transform.forward * hit.distance, Color.red);
            Dummy dummy = hit.collider.GetComponent<Dummy>();
            if (dummy != null)
            {
                dummy.TakeDamage(playerDamage); // Adjust the damage value as needed
            }
            GameObject impactDO = Instantiate(bulletsplash, hit.point, Quaternion.LookRotation(hit.normal));

            Destroy(impactDO, 2f);
        }

        SFX.instance.PlaySFX(0);
    }
    
    void PauseUnpause()
    {
        if(UI.activeSelf == false)
        {
            UI.SetActive(true);
            textMeshPro = UI.GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = "Pause!";
            Time.timeScale = 0f;
            Cursor.visible = true;
        }
        else
        {
            UI.SetActive(false);
            textMeshPro = UI.GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = "Pause!";
            Time.timeScale = 1f;
            Cursor.visible = false;
        }
    }

    IEnumerator Wait()
    {
        Debug.Log("Wait... start");
        yield return new WaitForSecondsRealtime(5);
        Debug.Log("Wait... end");
        Time.timeScale = 1f;
        UI.SetActive(false);
        SceneManager.LoadScene("MainMenu");
    }

    public void EndGame()
    {
            UI.SetActive(true);
            textMeshPro = UI.GetComponentInChildren<TextMeshProUGUI>();
            textMeshPro.text = "End Game!";
            Cursor.visible = true;
            anim.SetBool("die",true);
            Time.timeScale = 1f;
            canMove = false;
            StartCoroutine(Wait());

        //UI.SetActive(false);
        //Time.timeScale = 1f;
        //SceneManager.LoadScene(nextScene);
    }

    public void WonGame()
    {
        UI.SetActive(true);
        textMeshPro = UI.GetComponentInChildren<TextMeshProUGUI>();
        textMeshPro.text = "Game won!";
        Cursor.visible = true;
        Time.timeScale = 1f;
        canMove = false;
        StartCoroutine(Wait());

        //UI.SetActive(false);
        //Time.timeScale = 1f;
        //SceneManager.LoadScene(nextScene);
    }

    void Dash()
    {
        GameObject dashPart = Instantiate(dashParticle, theSR.transform.position, Quaternion.LookRotation(hit.normal));

        Destroy(dashPart, 2f);
        SFX.instance.PlaySFX(1); // dash
    }
}
