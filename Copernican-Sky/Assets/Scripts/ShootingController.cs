using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShootingController : MonoBehaviour
{

    public Transform gunTip;
    public GameObject bullet;
    public GameObject GM;
    public GameObject grenade;

    private GameObject text1;

    //records keypresses for directions
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    private bool moveDown = false;

    //speed variables
    private float moveSpeed = 75.0f;
    private float bulletSpeed = 5000.0f;
    private float grenadeSpeed = 125.0f;
    private float grenadeMaxDistance = 50.0f;

    //shooting variables
    private float timeBetweenShots;
    private float timeBetweenBullets;
    private float spreadAngle;
    private float nextShot;
    private float inaccuracy;
    private int numShots;
    private float nextBullet;

    //other stats
    private float health = 10.0f;
    private int currentGun;
    private int[] ammoClip;
    private int[] ammoStorage;
    private bool[] doesHave;

    //a bool to make it so the player is only notified they are out of ammo on the first hold down click
    private bool firstOut = true;

    //a bool to record how many shots are left
    private int multiShot = 0;

    //some 'final' variables set in Start()
    private int numOfGuns = 4;
    private int[] ammoMaxClip;
    private int[] ammoMaxStorage;

    private Animator anim;


    //components
    private Rigidbody2D rb;
    //private AudioController audioController;
    //private AnimationController animController;

    // Use this for initialization
    void Start()
    {
        //grabbing all the components and objects
        rb = GetComponent<Rigidbody2D>();

        anim = GetComponent<Animator>();

        //audioController = GetComponent<AudioController>();
        //animController = GetComponent<AnimationController>();
        //text1 = GameObject.FindGameObjectWithTag("UIText1");
        //GM = GameObject.FindGameObjectWithTag("GameManager");

        GunInit();

        GivePlayer();
    }

    //sets all the 'final' variables for the guns, subject to upgrades
    void GunInit()
    {
        //set gun variables
        ammoClip = new int[numOfGuns];
        ammoStorage = new int[numOfGuns];
        ammoMaxClip = new int[numOfGuns];
        ammoMaxStorage = new int[numOfGuns];
        doesHave = new bool[numOfGuns];

        //Shotgun
        ammoMaxClip[0] = 2;
        ammoMaxStorage[0] = 24;
        doesHave[0] = false;

        //SMG
        ammoMaxClip[1] = 20;
        ammoMaxStorage[1] = 100;
        doesHave[1] = false;

        //AR
        ammoMaxClip[2] = 30;
        ammoMaxStorage[2] = 120;
        doesHave[2] = false;

        //Pistol
        ammoMaxClip[3] = 12;
        ammoMaxStorage[3] = 48;
        doesHave[3] = false;
    }

    //a method that controls what the player gets when they start
    public void GivePlayer()
    {
        //Give the player a shotgun with full ammo to start
        doesHave[0] = true;
        ammoClip[0] = ammoMaxClip[0];
        ammoStorage[0] = ammoMaxStorage[0];

        //Give the player an SMG with full ammo to start
        doesHave[1] = true;
        ammoClip[1] = ammoMaxClip[1];
        ammoStorage[1] = ammoMaxStorage[1];

        //Give the player an AR with full ammo to start
        doesHave[2] = true;
        ammoClip[2] = ammoMaxClip[2];
        ammoStorage[2] = ammoMaxStorage[2];

        //Give the player an AR with full ammo to start
        doesHave[3] = true;
        ammoClip[3] = ammoMaxClip[3];
        ammoStorage[3] = ammoMaxStorage[3];


        SetGun(0);
    }

    void Update()
    {

        Controls();

    }

    void FixedUpdate()
    {
        //Rotation();

        //small bit of code for handling multiple shots in succession (burst fire), I think this is causing problems but idek
        if (multiShot >= 1 && Time.time > nextBullet)
        {
            Shoot();
            multiShot -= 1;
            ammoClip[currentGun] -= 1;
            SetUI();
        }

        SetUI();
    }


    //all the complex shooting stuff, can handle multiple bullet at ONCE (ie. shotty), but not multiple successive bullets (ie. burst fire)
    void Shoot()
    {
        //audioController.Shoot(currentGun);
        //animController.Shoot();

        nextShot = Time.time + timeBetweenShots;
        nextBullet = Time.time + timeBetweenBullets;


        float variance = -numShots / 2.0f * spreadAngle;

        float shootdir = Facing();
        Quaternion angle = Quaternion.AngleAxis(shootdir, Vector3.forward);


        Quaternion qAngle = Quaternion.AngleAxis(variance + (Random.Range(-inaccuracy, inaccuracy)), Vector3.forward) * angle;
        Quaternion qDelta = Quaternion.AngleAxis(spreadAngle + (Random.Range(-inaccuracy, inaccuracy)), Vector3.forward);
        for (int i = 0; i < numShots; i++)
        {
            GameObject go = Instantiate(bullet, gunTip.position, qAngle);
            go.GetComponent<Rigidbody2D>().AddForce(-go.GetComponent<Transform>().up * bulletSpeed);
            go.GetComponent<BulletController>().SetBulletType(currentGun);
            qDelta = Quaternion.AngleAxis(spreadAngle + (Random.Range(-inaccuracy, inaccuracy)), transform.forward);
            qAngle = qDelta * qAngle;
        }

    }

    //handles rotation toward mouse
    void Rotation()
    {
        Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        rb.rotation = (Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg + 90);

        //old rotation, worked better with some stuff, but with Dynamic RB it moves X and Y
        //rb.MoveRotation((Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg + 90));
    }

    //player controls
    void Controls()
    {
        moveUp = Input.GetKey("w");
        moveLeft = Input.GetKey("a");
        moveRight = Input.GetKey("d");
        moveDown = Input.GetKey("s");

        //if clicked fire on a click fire weapon
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShot && ammoClip[currentGun] > 0 && (currentGun == 0 || currentGun == 3))
        {
            Shoot();
            //animController.CancelReload();
            ammoClip[currentGun] -= 1;
            SetUI();
        }

        //if clicked and held fire on a automatic fire weapon
        if (Input.GetKey(KeyCode.Space) && Time.time > nextShot && ammoClip[currentGun] > 0 && currentGun == 1)
        {
            Shoot();
            //animController.CancelReload();
            ammoClip[currentGun] -= 1;
            SetUI();
        }

        //if clicked fire on a multi fire weapon
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShot && ammoClip[currentGun] > 0 && currentGun == 2)
        {

            multiShot = 2;
            Shoot();
            ammoClip[currentGun] -= 1;
            //animController.CancelReload();
            SetUI();
        }


        //if held and out of ammo
        if (Input.GetKey(KeyCode.Space) && Time.time > nextShot && ammoClip[currentGun] <= 0 && firstOut && currentGun == 1)
        {
            firstOut = false;
            //audioController.Empty();
            StartReload();


        }

        //if clicked and out of ammo
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShot && ammoClip[currentGun] <= 0)
        {
            StartReload();
        }

        //if clicked and out of ammo and stored ammo
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShot && ammoClip[currentGun] <= 0 && ammoStorage[currentGun] <= 0)
        {
            //audioController.Empty();
            StartReload();
        }
        /*
        //throw grenade
        if (Input.GetKeyDown(KeyCode.G))
        {

            GameObject go = Instantiate(grenade, transform.position, transform.rotation);
            Vector2 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float distance = (mouse - new Vector2(transform.position.x, transform.position.y)).magnitude;
            if (distance > grenadeMaxDistance)
                distance = grenadeMaxDistance;
            Debug.Log(distance);
            //rb.rotation = (Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg + 90);
            go.GetComponent<Rigidbody2D>().AddForce(-go.GetComponent<Transform>().up * grenadeSpeed * distance);
            go.GetComponent<Rigidbody2D>().AddTorque(500);
        }
        */

        //Next Weapon
        if (Input.GetKeyDown(KeyCode.Q) || (Input.GetAxis("Mouse ScrollWheel") > 0))
        {
            if (currentGun == 0)
                SetGun(3);
            else if (currentGun == 1)
                SetGun(0);
            else if (currentGun == 2)
                SetGun(1);
            else if (currentGun == 3)
                SetGun(2);
        }

        //Prev weapon
        if (Input.GetKeyDown(KeyCode.E) || (Input.GetAxis("Mouse ScrollWheel") < 0))
        {
            if (currentGun == 0)
                SetGun(1);
            else if (currentGun == 1)
                SetGun(2);
            else if (currentGun == 2)
                SetGun(3);
            else if (currentGun == 3)
                SetGun(0);
        }

        //Weapon Selection Numbers
        if (Input.GetKeyDown(KeyCode.Alpha1) && doesHave[0])
        {
            SetGun(0);
            SetUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && doesHave[1])
        {
            SetGun(1);
            SetUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && doesHave[2])
        {
            SetGun(2);
            SetUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && doesHave[3])
        {
            SetGun(3);
            SetUI();
        }

        
        //Reload 
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartReload();
        }
    }


    float Facing()
    {
        float direction = 0;



        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Down") || anim.GetCurrentAnimatorStateInfo(0).IsName("Down_Idle"))
        {
            direction = 0;
            Debug.Log("down");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Up") || anim.GetCurrentAnimatorStateInfo(0).IsName("Up_Idle"))
        {
            direction = 180;
            Debug.Log("up");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Right") || anim.GetCurrentAnimatorStateInfo(0).IsName("Right_Idle"))
        {
            direction = 90;
            Debug.Log("right");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Left") || anim.GetCurrentAnimatorStateInfo(0).IsName("Left_Idle"))
        {
            direction = 270;
            Debug.Log("left");
        }

        return direction;
    }


    /*
    Vector3 Facing()
    {
        Vector3 direction = Vector3.down;



        if(anim.GetCurrentAnimatorStateInfo(0).IsName("Down") || anim.GetCurrentAnimatorStateInfo(0).IsName("Down_Idle"))
        {
            direction = Vector3.down;
            Debug.Log("down");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Up") || anim.GetCurrentAnimatorStateInfo(0).IsName("Up_Idle"))
        {
            direction = Vector3.up;
            Debug.Log("up");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Right") || anim.GetCurrentAnimatorStateInfo(0).IsName("Right_Idle"))
        {
            direction = Vector3.right;
            Debug.Log("right");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Left") || anim.GetCurrentAnimatorStateInfo(0).IsName("Left_Idle"))
        {
            direction = Vector3.left;
            Debug.Log("left");
        }

        return direction;
    }
    */


    //tells the animator to start reloading
    void StartReload()
    {
        if (ammoStorage[currentGun] > 0)
        {
            FinishReload();
            //animController.Reload();

        }
    }

    //called by the animator once the players reload is finished, so that ammo is only added at the end of the animation (to prevent cancelling abuse)
    public void FinishReload()
    {
        firstOut = true;


        if (ammoStorage[currentGun] >= ammoMaxClip[currentGun] - ammoClip[currentGun])
        {
            ammoStorage[currentGun] -= ammoMaxClip[currentGun] - ammoClip[currentGun];

            ammoClip[currentGun] = ammoMaxClip[currentGun];
        }
        else
        {

            ammoClip[currentGun] += ammoStorage[currentGun];

            ammoStorage[currentGun] = 0;

        }

        //audioController.ReloadNoise();
    }

    //does the position movement stuff
    void Move()
    {
        Vector2 moveVector = new Vector2(0, 0);
        if (moveLeft)
            moveVector += (Vector2.left);
        if (moveRight)
            moveVector += (Vector2.right);
        if (moveUp)
            moveVector += (Vector2.up);
        if (moveDown)
            moveVector += (Vector2.down);

        rb.MovePosition(new Vector2(rb.position.x + moveVector.x * moveSpeed * Time.deltaTime, rb.position.y + moveVector.y * moveSpeed * Time.deltaTime));

    }

    //updates the UI with relevant player info
    void SetUI()
    {
        //text1.GetComponent<Text>().text = " Clip: " + ammoClip[currentGun] + "/" + ammoMaxClip[currentGun] + " Bag: " + ammoStorage[currentGun] + " HP: " + (health * 10.0f);

    }

    // a method to set various shooting related stats when switching guns, it also switches the animator (which switches sprites)
    void SetGun(int gunType)
    {
        if (gunType == 0)
        {
            currentGun = gunType;
            timeBetweenShots = 1.1f;
            spreadAngle = 7.0f;
            numShots = 5;
            inaccuracy = 3.0f;
            //gunTip.localPosition = new Vector2(-0.5f, -7);

        }

        if (gunType == 1)
        {
            currentGun = gunType;
            timeBetweenShots = 0.1f;
            spreadAngle = 3.0f;
            inaccuracy = 6.0f;
            numShots = 1;
            //gunTip.localPosition = new Vector2(-0.5f, -7);

        }

        if (gunType == 2)
        {
            currentGun = gunType;
            timeBetweenShots = 0.15f;
            timeBetweenBullets = 0.05f;
            spreadAngle = 2.0f;
            inaccuracy = 3.0f;
            numShots = 1;
            //gunTip.localPosition = new Vector2(-0.5f, -7);

        }

        if (gunType == 3)
        {
            currentGun = gunType;
            timeBetweenShots = 0.1f;
            spreadAngle = 3.0f;
            inaccuracy = 5.0f;
            numShots = 1;
            //gunTip.localPosition = new Vector2(-2.5f, -5);

        }

        //animController.GunSwitch(gunType);

    }


}
