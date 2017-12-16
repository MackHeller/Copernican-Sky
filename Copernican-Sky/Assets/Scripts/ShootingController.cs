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
    public SheetSwap sheetController;

    private GameObject text1;

    //records keypresses for directions
    private bool moveLeft = false;
    private bool moveRight = false;
    private bool moveUp = false;
    private bool moveDown = false;

    //speed variables
    private float moveSpeed = 75.0f;
    private float grenadeSpeed = 125.0f;
    private float grenadeMaxDistance = 50.0f;

    //other stats
    private float health = 10.0f;

    //a bool to make it so the player is only notified they are out of ammo on the first hold down click
    private bool firstOut = true;

    private int multiShot = 0;
    private float nextShot;
    private float nextBullet;

    //some 'final' variables set in Start()
    private int numOfGuns = 4;

    //An array of all the weapons in the game
    private Weapon[] weapons;

    //A list of all the weapons the player currently has equipped
    private List<Weapon> playersWeapons;

    //The currently held weapon
    private Weapon currentWeapon;

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
        text1 = GameObject.FindGameObjectWithTag("WeaponUI");
        //GM = GameObject.FindGameObjectWithTag("GameManager");

        GunInit();

        GivePlayer();
    }

    //sets all the 'final' variables for the guns, subject to upgrades
    void GunInit()
    {
        weapons = new Weapon[numOfGuns];

        //set gun variables


        //Shotgun
        weapons[0] = new Weapon();
        weapons[0].name = "Shotgun";
        weapons[0].weaponType = 0;
        weapons[0].ammoMaxClip = 2;
        weapons[0].ammoMaxStorage = 24;
        weapons[0].timeBetweenShots = 1.1f;
        weapons[0].spreadAngle = 7.0f;
        weapons[0].numShots = 5;
        weapons[0].inaccuracy = 3.0f;
        weapons[0].bulletSpeed = 5000.0f;
        weapons[0].posUp = new Vector3(2.5f, 7.5f);
        weapons[0].posDown = new Vector3(2.5f, 2.5f);
        weapons[0].posRight = new Vector3(6.5f, 6.5f);
        weapons[0].posLeft = new Vector3(-6.5f, 6.5f);
        weapons[0].spriteSheetName = "Sprites/AivaShotgun_Sheet";

        //SMG
        weapons[1] = new Weapon();
        weapons[1].name = "SMG";
        weapons[1].weaponType = 1;
        weapons[1].ammoMaxClip = 20;
        weapons[1].ammoMaxStorage = 100;
        weapons[1].timeBetweenShots = 0.1f;
        weapons[1].spreadAngle = 3.0f;
        weapons[1].inaccuracy = 6.0f;
        weapons[1].numShots = 1;
        weapons[1].bulletSpeed = 5000.0f;
        weapons[1].posUp = new Vector3(2.5f, 7.5f);
        weapons[1].posDown = new Vector3(2.5f, 2.5f);
        weapons[1].posRight = new Vector3(6.5f, 6.5f);
        weapons[1].posLeft = new Vector3(-6.5f, 6.5f);
        weapons[1].spriteSheetName = "Sprites/AivaGun_Sheet";

        //AR
        weapons[2] = new Weapon();
        weapons[2].name = "AR";
        weapons[2].weaponType = 2;
        weapons[2].ammoMaxClip = 30;
        weapons[2].ammoMaxStorage = 120;
        weapons[2].timeBetweenShots = 0.15f;
        weapons[2].timeBetweenBullets = 0.05f;
        weapons[2].spreadAngle = 2.0f;
        weapons[2].inaccuracy = 3.0f;
        weapons[2].numShots = 1;
        weapons[2].bulletSpeed = 5000.0f;
        weapons[2].posUp = new Vector3(2.5f, 7.5f);
        weapons[2].posDown = new Vector3(2.5f, 2.5f);
        weapons[2].posRight = new Vector3(6.5f, 6.5f);
        weapons[2].posLeft = new Vector3(-6.5f, 6.5f);
        weapons[2].spriteSheetName = "Sprites/AivaGun_Sheet";

        //Pistol
        weapons[3] = new Weapon();
        weapons[3].name = "Pistol";
        weapons[3].weaponType = 0;
        weapons[3].ammoMaxClip = 12;
        weapons[3].ammoMaxStorage = 48;
        weapons[3].timeBetweenShots = 0.1f;
        weapons[3].spreadAngle = 3.0f;
        weapons[3].inaccuracy = 5.0f;
        weapons[3].numShots = 1;
        weapons[3].bulletSpeed = 5000.0f;
        weapons[3].posUp = new Vector3(2.5f, 7.5f);
        weapons[3].posDown = new Vector3(2.5f, 2.5f);
        weapons[3].posRight = new Vector3(6.5f, 6.5f);
        weapons[3].posLeft = new Vector3(-6.5f, 6.5f);
        weapons[3].spriteSheetName = "Sprites/AivaGun_Sheet";

    }



    //a method that controls what the player gets when they start
    public void GivePlayer()
    {
        playersWeapons = new List<Weapon>();

        //Give the player all the guns with full ammo
        for (int x = 0; x < numOfGuns; x++)
        {
            playersWeapons.Add(weapons[x]);
            playersWeapons[x].ammoClip = playersWeapons[x].ammoMaxClip;
            playersWeapons[x].ammoStorage = playersWeapons[x].ammoMaxStorage;
        }

        currentWeapon = playersWeapons[0];
        sheetController.SpriteSheetName = currentWeapon.spriteSheetName;

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
            currentWeapon.ammoClip -= 1;
            SetUI();
        }

        SetUI();
    }


    //all the complex shooting stuff, can handle multiple bullet at ONCE (ie. shotty), but not multiple successive bullets (ie. burst fire)
    void Shoot()
    {
        //audioController.Shoot(currentGun);
        //animController.Shoot();

        nextShot = Time.time + currentWeapon.timeBetweenShots;
        nextBullet = Time.time + currentWeapon.timeBetweenBullets;


        float variance = -currentWeapon.numShots / 2.0f * currentWeapon.spreadAngle;

        float shootdir = Facing();
        Quaternion angle = Quaternion.AngleAxis(shootdir, Vector3.forward);


        Quaternion qAngle = Quaternion.AngleAxis(variance + (Random.Range(-currentWeapon.inaccuracy, currentWeapon.inaccuracy)), Vector3.forward) * angle;
        Quaternion qDelta = Quaternion.AngleAxis(currentWeapon.spreadAngle + (Random.Range(-currentWeapon.inaccuracy, currentWeapon.inaccuracy)), Vector3.forward);
        for (int i = 0; i < currentWeapon.numShots; i++)
        {
            GameObject go = Instantiate(bullet, gunTip.position, qAngle);
            go.GetComponent<Rigidbody2D>().AddForce(-go.GetComponent<Transform>().up * currentWeapon.bulletSpeed);

            //this part needs to be changed/fixed, mostly changed bullet -> shooter relationship
            go.GetComponent<BulletController>().SetBulletType(System.Array.IndexOf(weapons, currentWeapon));

            qDelta = Quaternion.AngleAxis(currentWeapon.spreadAngle + (Random.Range(-currentWeapon.inaccuracy, currentWeapon.inaccuracy)), transform.forward);
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
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShot && currentWeapon.ammoClip > 0 && currentWeapon.weaponType == 0)
        {
            Shoot();
            //animController.CancelReload();
            currentWeapon.ammoClip -= 1;
            SetUI();
        }

        //if clicked and held fire on a automatic fire weapon
        if (Input.GetKey(KeyCode.Space) && Time.time > nextShot && currentWeapon.ammoClip > 0 && currentWeapon.weaponType == 1)
        {
            Shoot();
            //animController.CancelReload();
            currentWeapon.ammoClip -= 1;
            SetUI();
        }

        //if clicked fire on a multi fire weapon
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShot && currentWeapon.ammoClip > 0 && currentWeapon.weaponType == 2)
        {

            multiShot = 2;
            Shoot();
            currentWeapon.ammoClip -= 1;
            //animController.CancelReload();
            SetUI();
        }


        //if held and out of ammo
        if (Input.GetKey(KeyCode.Space) && Time.time > nextShot && currentWeapon.ammoClip <= 0 && firstOut && currentWeapon.weaponType == 1)
        {
            firstOut = false;
            //audioController.Empty();
            StartReload();


        }

        //if clicked and out of ammo
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShot && currentWeapon.ammoClip <= 0)
        {
            StartReload();
        }

        //if clicked and out of ammo and stored ammo
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextShot && currentWeapon.ammoClip <= 0 && currentWeapon.ammoStorage <= 0)
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

        //Prev Weapon
        if (Input.GetKeyDown(KeyCode.Q) || (Input.GetAxis("Mouse ScrollWheel") > 0))
        {
            int current = playersWeapons.IndexOf(currentWeapon);
            if (current == 0)
                current = playersWeapons.Count -1;
            else
                current -= 1;

            currentWeapon = playersWeapons[current];
            sheetController.SpriteSheetName = currentWeapon.spriteSheetName;

        }

        //Next weapon
        if (Input.GetKeyDown(KeyCode.E) || (Input.GetAxis("Mouse ScrollWheel") < 0))
        {
            int current = playersWeapons.IndexOf(currentWeapon);
            if (current == playersWeapons.Count -1)
                current = 0;
            else
                current += 1;

            currentWeapon = playersWeapons[current];
            sheetController.SpriteSheetName = currentWeapon.spriteSheetName;

        }

        //Weapon Selection Numbers
        if (Input.GetKeyDown(KeyCode.Alpha1) && (playersWeapons.Count >= 1))
        {
            currentWeapon = playersWeapons[0];
            sheetController.SpriteSheetName = currentWeapon.spriteSheetName;
            SetUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && (playersWeapons.Count >= 2))
        {
            currentWeapon = playersWeapons[1];
            sheetController.SpriteSheetName = currentWeapon.spriteSheetName;
            SetUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && (playersWeapons.Count >= 3))
        {
            currentWeapon = playersWeapons[2];
            sheetController.SpriteSheetName = currentWeapon.spriteSheetName;
            SetUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && (playersWeapons.Count >= 4))
        {
            currentWeapon = playersWeapons[3];
            sheetController.SpriteSheetName = currentWeapon.spriteSheetName;
            SetUI();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4) && (playersWeapons.Count >= 5))
        {
            currentWeapon = playersWeapons[4];
            sheetController.SpriteSheetName = currentWeapon.spriteSheetName;
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
            gunTip.localPosition = currentWeapon.posDown;
            Debug.Log("down");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Up") || anim.GetCurrentAnimatorStateInfo(0).IsName("Up_Idle"))
        {
            direction = 180;
            gunTip.localPosition = currentWeapon.posUp;
            Debug.Log("up");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Right") || anim.GetCurrentAnimatorStateInfo(0).IsName("Right_Idle"))
        {
            direction = 90;
            gunTip.localPosition = currentWeapon.posRight;
            Debug.Log("right");
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Left") || anim.GetCurrentAnimatorStateInfo(0).IsName("Left_Idle"))
        {
            direction = 270;
            gunTip.localPosition = currentWeapon.posLeft;
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
        if (currentWeapon.ammoStorage > 0)
        {
            FinishReload();
            //animController.Reload();

        }
    }

    //called by the animator once the players reload is finished, so that ammo is only added at the end of the animation (to prevent cancelling abuse)
    public void FinishReload()
    {
        firstOut = true;


        if (currentWeapon.ammoStorage >= currentWeapon.ammoMaxClip - currentWeapon.ammoClip)
        {
            currentWeapon.ammoStorage -= currentWeapon.ammoMaxClip - currentWeapon.ammoClip;

            currentWeapon.ammoClip = currentWeapon.ammoMaxClip;
        }
        else
        {

            currentWeapon.ammoClip += currentWeapon.ammoStorage;

            currentWeapon.ammoStorage = 0;

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
        //text1.GetComponent<Text>().text = "Clip:" + currentWeapon.ammoClip + "/" + currentWeapon.ammoMaxClip + " Bag:" + currentWeapon.ammoStorage;
        text1.GetComponent<Text>().text = currentWeapon.name + ": " + currentWeapon.ammoClip + "/" + currentWeapon.ammoMaxClip + " (" + currentWeapon.ammoStorage+")";

    }



}


public class Weapon
{
    //name
    public string name;

    // gun stats
    public float timeBetweenShots;
    public float timeBetweenBullets;
    public float spreadAngle;
    public float inaccuracy;
    public float numShots;

    // ammo stats
    public int ammoMaxClip;
    public int ammoMaxStorage;
    public int ammoClip;
    public int ammoStorage;

    // bullet stats
    public float bulletSpeed;
    public float damage;

    // weapon type
    // 0 = semi automatic
    // 1 = automatic
    // 2 = burst fire
    public int weaponType;

    //projectile position
    public Vector3 posUp;
    public Vector3 posDown;
    public Vector3 posLeft;
    public Vector3 posRight;

    //sprite sheet
    public string spriteSheetName;


}
