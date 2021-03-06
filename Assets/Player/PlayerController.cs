using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] CharacterController controller; 
    [SerializeField]private float speed = 12f;

    public GameObject ObjectivePanel;

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
    private int glockShootCounter = 0;
    private int ak47ShootCounter = 0;

 //   public GameObject bullet;
    public Transform firePoint;

    public GunManager activeGun;
    public UIController ui;

    public List<GunManager> allGuns = new List<GunManager>();
    public List<GunManager> unlockableGuns = new List<GunManager>();
    public int currentGun;


    public bool toggle =false;
    //collider
    private void Awake()
    {
        instance = this;
        currentGun = 0;
        
    }


    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
     

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);
        ui = GameObject.Find("Canvas").GetComponent<UIController>();
        ui.maxAmmoText.text = "" + activeGun.maxAmmo.ToString();
        ui.currentAmmoText.text = "" + activeGun.currentAmmo.ToString();
        ObjectivePanel = GameObject.Find("ObjectivePanel");
        ObjectivePanel.SetActive(false);
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
        if (!UIController.instance.pausePanel.activeSelf)
        {

            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90, 90f);

            camTrans.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            transform.Rotate(Vector3.up * mouseX);

        }//handle shooting mouse 1 and 2 

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            
            if (!toggle)
            {
                toggle = !toggle;
                UIController.instance.PauseLevel();
            }
            else
            {
                toggle = !toggle;

                UIController.instance.resumeLevel();
            }
        }
        //single shot or tap fire
        if (Input.GetMouseButtonDown(0) && activeGun.fireCounter <=0 && !activeGun.manchete && !activeGun.crowbar)
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
        }else if (Input.GetMouseButtonDown(0) && activeGun.manchete && !anim.GetCurrentAnimatorStateInfo(0).IsTag("machete_attack"))
        {
           
            MeleeAttackMachete();

        }else if (Input.GetMouseButtonDown(0) && activeGun.crowbar && !anim.GetCurrentAnimatorStateInfo(0).IsTag("crowbarAttack"))
        {
            MeleeAttack();
        }


        //rapid fire hold mouse
        if (Input.GetMouseButton(0) && activeGun.canAutoFire)
        {
            if (activeGun.fireCounter <=0 && !anim.GetCurrentAnimatorStateInfo(0).IsTag("akreload"))
            {
                 FireShot();

            }
        }


        if (Input.GetKeyDown(KeyCode.R))
        {
            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("GlockReloading") && !anim.GetCurrentAnimatorStateInfo(0).IsTag("akreload"))
            {
                //reloading
                if (activeGun.glock)
                {
                    StartCoroutine(ReloadGlock(1.2f));

                }
                else if (activeGun.ak47)
                {
                    StartCoroutine(ReloadAK47(1.2f)); 
                }

              
            } 
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += 15; 
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= 15;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!UIController.instance.pausePanel.activeSelf)
            {

                ObjectivePanel.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            ObjectivePanel.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            
            SwitchGun(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (allGuns.Count >=2)
            {
              
                SwitchGun(1);
            } 
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (allGuns.Count >=3)
            {
           
                SwitchGun(2);
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (allGuns.Count >= 4)
            {

                SwitchGun(3);
            }
        }
        anim.SetFloat("moveSpeed", move.magnitude);

    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        
    }

    public void 
        FireShot()
    {
        // when bullets available
        if (activeGun.currentAmmo > 0)
        {
            activeGun.currentAmmo--;
            
            Instantiate(activeGun.bullet, firePoint.position, firePoint.rotation);
            activeGun.fireCounter = activeGun.fireRate;

            if (activeGun.glock)
            {
                glockShootCounter++;
                Debug.Log(glockShootCounter);
                anim.SetTrigger("gShoot");
               
                
            }
            if (activeGun.ak47)
            {
                ak47ShootCounter++;
                anim.SetTrigger("ak47Shoot");
            }

            ui.currentAmmoText.text = "" + activeGun.currentAmmo.ToString();
           
            activeGun.bulletFire.PlayOneShot(activeGun.bulletFire.clip);


            // ouch no bullets
        }else if (activeGun.currentAmmo == 0)
        {
            //reloading

            if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("GlockReloading") && !anim.GetCurrentAnimatorStateInfo(0).IsTag("akreload"))
            {

                if (activeGun.glock)
                {
                    StartCoroutine(ReloadGlock(1.2f));

                }
                else if (activeGun.ak47)
                {
                    if (!anim.GetCurrentAnimatorStateInfo(0).IsTag("akreload"))
                    {

                        StartCoroutine(ReloadAK47(1.2f));
                    }
                }

            }
            // activeGun.reload.Play();
        }

    }


    public void MeleeAttack()
    {
        if (activeGun.crowbar )
        {

            if (activeGun.fireCounter <=0)
            {
                anim.SetTrigger("crowbarAttack");
                StartCoroutine(Waitt(0.5f));
                activeGun.fireCounter = activeGun.fireRate;
            }
           
        }



    }

    public void MeleeAttackMachete()
    {
        if (activeGun.manchete && !anim.GetCurrentAnimatorStateInfo(0).IsTag("machete_attack"))
        {

            if (activeGun.fireCounter <=0)
            {

                anim.SetTrigger("macheteAttack");
                StartCoroutine(Waitt2(0.5f));
                activeGun.fireCounter = activeGun.fireRate;
            }
        }
    }

    private IEnumerator Waitt(float waitTime)
    {
        BoxCollider crowbar = transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<BoxCollider>();
        if (!activeGun.bulletFire.isPlaying) 
        {
            activeGun.bulletFire.Play();
            crowbar.enabled = !crowbar.enabled;
        }
        yield return new WaitForSeconds(waitTime);
        crowbar.enabled = !crowbar.enabled;

    }
    private IEnumerator Waitt2(float waitTime)
    {
        BoxCollider man = transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<BoxCollider>();

        if (!activeGun.bulletFire.isPlaying)
        {
            activeGun.bulletFire.Play();
            man.enabled = !man.enabled;
        }
        yield return new WaitForSeconds(waitTime);
        man.enabled = !man.enabled;

    }
    private IEnumerator ReloadAK47(float waitTime)
    {
        if (activeGun.maxAmmo >= 0)
        {
            Debug.Log("ReloadAK47");

          
            if (activeGun.currentAmmo < 30)
            {


                anim.SetTrigger("ak47Reload");
                activeGun.reload.Play();
                yield return new WaitForSeconds(waitTime);
                if (activeGun.maxAmmo < 0)
                {
                    activeGun.currentAmmo = 0;
                    activeGun.maxAmmo = 0;

                }
                else
                {

                    if (ak47ShootCounter > activeGun.maxAmmo)
                    {

                        Debug.Log(activeGun.currentAmmo + "enter here ");
                        Debug.Log(activeGun.maxAmmo + "max here ");
                        activeGun.currentAmmo += activeGun.maxAmmo;
                        activeGun.maxAmmo = 0;
                    }

                    else if (activeGun.maxAmmo <= 0)
                    {
                        activeGun.currentAmmo = 0;
                        activeGun.maxAmmo = 0;
                    }
                    else if (activeGun.currentAmmo == 0 && activeGun.maxAmmo > 0)
                    {
                        activeGun.maxAmmo -= 30;
                        activeGun.currentAmmo += 30;
                    }
                    else
                    {
                        activeGun.currentAmmo += ak47ShootCounter;
                        activeGun.maxAmmo -= ak47ShootCounter;

                    }
                }

            }
        }
        ak47ShootCounter = 0;
        ui.currentAmmoText.text = "" + activeGun.currentAmmo.ToString();
        ui.maxAmmoText.text = "" + activeGun.maxAmmo.ToString();
    }
    private IEnumerator ReloadGlock(float waitTime)
    {
        

        if (activeGun.maxAmmo > 0) {
          
           
            if (activeGun.currentAmmo < 10)
            {

               


                if (activeGun.maxAmmo < 0)
                {
                    activeGun.currentAmmo =0;
                    activeGun.maxAmmo = 0;

                }
                else
                {



                    anim.SetTrigger("gReload");
                    activeGun.reload.Play();
                    yield return new WaitForSeconds(waitTime);

                    if (glockShootCounter > activeGun.maxAmmo)
                    {
                    
                        Debug.Log(activeGun.currentAmmo + "enter here ");
                        Debug.Log(activeGun.maxAmmo + "max here ");
                        activeGun.currentAmmo += activeGun.maxAmmo;
                        activeGun.maxAmmo = 0;
                    }

                    else if (activeGun.maxAmmo <=0 )
                    {
                        activeGun.currentAmmo = 0;
                        activeGun.maxAmmo = 0;
                    } else if (activeGun.currentAmmo == 0 && activeGun.maxAmmo > 0)
                    {
                        activeGun.maxAmmo -= 10;
                        activeGun.currentAmmo += 10;
                    }
                    else
                    {
                        activeGun.currentAmmo += glockShootCounter;
                        activeGun.maxAmmo -= glockShootCounter;

                    }
                }


            }
        }
     
       
        glockShootCounter = 0;
        ui.currentAmmoText.text = "" + activeGun.currentAmmo.ToString();
        ui.maxAmmoText.text = "" + activeGun.maxAmmo.ToString();
    }
    public void SwitchGun(int number)
    {

        for (int i = 0; i < allGuns.Count; i++)
        {
            allGuns[i].gameObject.SetActive(false);
        }

        currentGun = number;

        activeGun = allGuns[currentGun];
        activeGun.gameObject.SetActive(true);


        
        ui.maxAmmoText.text = "" + activeGun.maxAmmo.ToString();
        ui.currentAmmoText.text = "" + activeGun.currentAmmo.ToString();
    }


    public void AddGun(string gunToAdd)
    {
        bool gunUnlocked = false;

        if (unlockableGuns.Count >0)
        {
            for (int i =0; i < unlockableGuns.Count; i++)
            {
                if (unlockableGuns[i].gunName == gunToAdd)
                {
                    gunUnlocked = true;
                    allGuns.Add(unlockableGuns[i]);
                    unlockableGuns.RemoveAt(i);

                    i = unlockableGuns.Count;
                }
            }

        }

        if (gunUnlocked)
        {
            currentGun = allGuns.Count -1 ;
            SwitchGun(currentGun);
        }
    }

}
