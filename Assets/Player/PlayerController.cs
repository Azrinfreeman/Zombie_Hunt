using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] CharacterController controller; 
    [SerializeField]private float speed = 12f;

    public float gravity = -9.81f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public Animator anim;

    [SerializeField]Vector3 velocity;
    [SerializeField]bool isGrounded;

    //
    Vector3 mouseInput;
    float xRotation = 0f;
    public Transform camTrans;
    public float mouseSensitivity = 7f;

    //bullets

 //   public GameObject bullet;
    public Transform firePoint;

    public GunManager activeGun;
    public UIController ui;

    public List<GunManager> allGuns = new List<GunManager>();
    public int currentGun;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
     

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        ui.ammoText.text = "" + activeGun.currentAmmo.ToString();
    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y <0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime); // for movement 

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime); // for gravity 


        //contropl camera rotation
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity ;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity ;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90f);

        camTrans.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
        //handle shooting mouse 1 and 2 


        //single shot or tap fire
        if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <=0)
        {
            RaycastHit hit;
            if (Physics.Raycast(camTrans.position, camTrans.transform.forward, out hit, 50f))
            {
                firePoint.LookAt(hit.point);
            }
            else
            {
                firePoint.LookAt(camTrans.position + (camTrans.forward * 30f));
            }
            // Instantiate(bullet, firePoint.position + (transform.forward * (-speed * Time.deltaTime)), firePoint.rotation);
            FireShot();
        }


        //rapid fire hold mouse
        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <=0)
            {
                FireShot();
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchGun(1); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchGun(2);
        }

        anim.SetFloat("moveSpeed", move.magnitude);

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }

    public void FireShot()
    {
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;

            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;
            ui.ammoText.text = "" + activeGun.currentAmmo.ToString();
        }

    }

    public void SwitchGun(int number)
    {
        
        activeGun.gameObject.SetActive(false);

        currentGun = number;

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        ui.ammoText.text = "" + activeGun.currentAmmo.ToString();
    }



}
