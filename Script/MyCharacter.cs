using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using ProgressBar;

public class MyCharacter : MonoBehaviour {

    private Vector3 NavPos = Vector3.zero;
    private bool PathFinding = false;

    public GameObject arm = null;
    public GameObject leftarm = null;
    private int WeaponType = 0;
    private int MeleeType = 1;
    private GameObject myWeapon = null;
    private GameObject myWeapon2 = null;
    private bool AttackAni = false;

    private Vector3 bowPos = Vector3.zero;
    private Vector3 bowPos2 = Vector3.zero;
    private bool Attack = false;

    public GameObject inventory = null;
    public GameObject inventorytooltip = null;

    private ProgressBarBehaviour playerHPbar = null;
    private ProgressBarBehaviour playerMPbar = null;
    private ProgressBarBehaviour playerExpbar = null;

    private float passTime = 0;
    // Use this for initialization
    void Start () {
        playerHPbar = GameObject.FindGameObjectWithTag("PlayerHP").GetComponent<ProgressBarBehaviour>();
        playerHPbar.ProgressSpeed = 500;
        playerMPbar = GameObject.FindGameObjectWithTag("PlayerMP").GetComponent<ProgressBarBehaviour>();
        playerMPbar.ProgressSpeed = 500;
        playerExpbar = GameObject.FindGameObjectWithTag("PlayerExp").GetComponent<ProgressBarBehaviour>();
        playerExpbar.ProgressSpeed = 500;

        //LevelUpData.levels = LevelUpData.ReadCsv();
        ////IEnumerable <LevelUpData.LevelUp> levels = LevelUpData.ReadCsv();
        //foreach(LevelUpData.LevelUp lu in LevelUpData.levels)
        //{
        //    int a = lu.atk;
        //    int e = lu.exp;
        //    print(a + "," + e);
        //}

        //ItemDatabase.items = ItemDatabase.ReadCsv();
        //Dictionary<int, ItemDatabase.Item> items = ItemDatabase.items.ToDictionary(x => x.Key, x => x.Value);
        //ItemDatabase.Item it = items[2];
        //print(items[1].name);
        //print(items[101].name);
        //print(items[201].name);
    }

	
	// Update is called once per frame
	void Update () {
        if(passTime < 1.0f)
            passTime += Time.deltaTime;

        if (SceneManager.GetActiveScene().name == "MoveIsland")
            return;

        playerHPbar.SetFillerSizeAsPercentage(PlayerData.getHPpercent());
        playerMPbar.SetFillerSizeAsPercentage(PlayerData.getMPpercent());
        playerExpbar.SetFillerSizeAsPercentage(PlayerData.getExpPercent());

        bool bMove = false;
        bool bRun = true; // 원래는 false로 걷기, 뛰기 만들랬는데 너무 답답해서 일단 true로 뛰기 고정함
        // 방향 이동
        if (AttackAni == false)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //transform.DORotate(new Vector3(0, 180, 0), 0.5f);
                //transform.rotation = Quaternion.Slerp(transform.rotation, Camera.main.transform.rotation, 
                //transform.DORotate(Camera.main.transform.rotation, 0.5f);
                //transform.rotation = Camera.main.transform.rotation;
                bMove = true;
                //Vector3 nowRot = transform.rotation.eulerAngles;
                //nowRot.y -= 90.0f;
                //transform.DORotate(nowRot, 0.5f);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                //transform.DORotate(new Vector3(0, 0, 0), 0.5f);
                //transform.rotation = Camera.main.transform.rotation + Quaternion.Euler(0, 180, 0);
                bMove = true;
                GetComponent<Animator>().SetBool("Static_b", true);
                transform.Translate(Vector3.back * Time.deltaTime * 3.0f);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                //transform.DORotate(new Vector3(0, 90, 0), 0.5f);
                if (Camera.main.GetComponent<CompleteCameraController>() != null)
                {
                    Camera.main.GetComponent<CompleteCameraController>().CameraRight();

                    Vector3 rVec = transform.rotation.eulerAngles;
                    rVec.y = Camera.main.transform.rotation.eulerAngles.y;
                    transform.rotation = Quaternion.Euler(rVec);
                }
                //bMove = true;
                //Vector3 nowRot = transform.rotation.eulerAngles;
                //nowRot.x -= 90.0f;
                //transform.DORotate(nowRot, 0.5f);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                //transform.DORotate(new Vector3(0, -90, 0), 0.5f);
                if (Camera.main.GetComponent<CompleteCameraController>() != null)
                {
                    Camera.main.GetComponent<CompleteCameraController>().CameraLeft();

                    Vector3 rVec = transform.rotation.eulerAngles;
                    rVec.y = Camera.main.transform.rotation.eulerAngles.y;
                    transform.rotation = Quaternion.Euler(rVec);
                }
                //Vector3 nowRot = transform.rotation.eulerAngles;
                //nowRot.x += 90.0f;
                //transform.DORotate(nowRot, 0.5f);
            }

            if(Input.GetKeyUp(KeyCode.DownArrow))
            {
                GetComponent<Animator>().SetBool("Static_b", false);
            }


            // 스페이스 누르면 점프
            if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Animator>().SetBool("Jump_b", true);
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                GetComponent<Animator>().SetBool("Jump_b", false);
            }

            // 왼쪽쉬프트 누르면 달리기
            if (Input.GetKey(KeyCode.LeftShift))
            {
                bRun = true;
            }

            //if(Input.GetKeyDown(KeyCode.I))
            //{
            //    if (inventory.activeSelf == false)
            //    {
            //        inventory.GetComponent<Inventory>().openInventory();
            //    }
            //    else
            //    {
            //        inventory.GetComponent<Inventory>().closeInventory();
            //        inventorytooltip.GetComponent<Tooltip>().deactivateTooltip();
            //    }
            //}

            if (Input.GetKeyDown(KeyCode.X))
            {
                getItem();
            }
        }

        // 길찾기 - 마우스로 클릭하면 길인 경우에 그곳으로 감 - 지금 안씀
        /*
        if (Input.GetMouseButtonUp(0))
        {
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hit = Physics.RaycastAll(r);
            foreach(RaycastHit h in hit)
            {
                if(h.transform.tag == "way")
                {
                    NavPos = h.point;
                    NavMeshAgent agent = GetComponent<NavMeshAgent>();
                    agent.destination = NavPos;
                    PathFinding = true;
                }
            }
        }
        */

        // 목적지에 가고있으면 달리는 모션
        if (PathFinding == true)
        {
            bMove = true;
            bRun = true;
        }

        // 목적지에 도달하면 경로초기화
        if(NavPos != Vector3.zero)
        {
            if(Vector3.Distance(NavPos, transform.position) < 1.0f)
            {
                NavPos = Vector3.zero;
                NavMeshAgent agent = GetComponent<NavMeshAgent>();
                agent.ResetPath();
                PathFinding = false;
            }
        }

        // 이동키가 활성화 되어있으면 속도를 줌.
        if (bMove == true)
        {
            if(bRun == true)
                GetComponent<Animator>().SetFloat("Speed_f", 1.0f);
            else
                GetComponent<Animator>().SetFloat("Speed_f", 0.3f);
        }
        else
        {
            GetComponent<Animator>().SetFloat("Speed_f", 0.0f);
        }

        // 무기교체
        // 1롱소드 2그레이트소드(양손검) 3창(한손검) 4활
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            equipOneHand();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            equipTwoHand();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            equipSpear();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            equipBow();
        }

        // 공격
        if (Input.GetKey(KeyCode.LeftControl))
        {
            // 활이면
            if(WeaponType == 11)
            {
                Attack = true;
                GetComponent<Animator>().SetInteger("WeaponType_int", 11);

                myWeapon.transform.localRotation = Quaternion.Euler(0, -2.7f, 82.74f);
                myWeapon.transform.localPosition = bowPos;

                myWeapon2.transform.localRotation = Quaternion.Euler(-28.58f, -1.1f, -12f);
                myWeapon2.transform.localPosition = bowPos2;
            }
            // 근접무기면
            else if (WeaponType == 12)
            {
                Attack = true;
                GetComponent<Animator>().SetInteger("WeaponType_int", 12);
                GetComponent<Animator>().SetInteger("MeleeType_int", MeleeType);

                myWeapon.transform.localRotation = Quaternion.AngleAxis(135f, Vector3.right);
            }
            AttackAni = true;
        }
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            GetComponent<Animator>().SetBool("Shoot_b", true);
            GetComponent<Animator>().SetBool("Reload_b", false);
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            AttackAni = false;
        }

        // 애니메이션이 끝나면 무기를 초기회전값으로 되돌림
        if (AttackAni == false)
        {
            if ((GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Melee_OneHanded")
                || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Melee_TwoHanded")
                || GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Melee_Stab"))
                && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                myWeapon.transform.localRotation = Quaternion.identity;
                Attack = false;
                GetComponent<Animator>().SetInteger("WeaponType_int", 0);
            }

            // 활은 애니메이션 끝날때 위치와 bool값 초기화
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BowShoot")
                && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                Attack = false;
                GetComponent<Animator>().SetBool("Reload_b", true);
                GetComponent<Animator>().SetBool("Shoot_b", false);
                //myWeapon.transform.localPosition = Vector3.zero;
                //myWeapon2.transform.localPosition = Vector3.zero;
            }

            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Bow_Load")
                && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                GetComponent<Animator>().SetBool("Shoot_b", false);
                GetComponent<Animator>().SetBool("Reload_b", false);
                GetComponent<Animator>().SetInteger("WeaponType_int", 0);
            }
        }
        else
        {
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("BowShoot")
                && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                GetComponent<Animator>().SetBool("Reload_b", true);
            }
            if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("Bow_Load")
                && GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.99f)
            {
                GetComponent<Animator>().SetBool("Shoot_b", false);
                GetComponent<Animator>().SetBool("Reload_b", false);
            }
        }
    }

    public void getItem()
    {
        var inven = inventory.GetComponent<Inventory>();
        bool foundItem = false;
        int getItemId = UnityEngine.Random.Range(100, 109);

        List<Item> items = inven.ItemsInInventory;
        foreach (Item item in items)
        {
            if (item.itemID == getItemId)
            {
                item.itemValue++;
                foundItem = true;
                break;
            }
        }
        if (foundItem == false)
        {
            inven.addItemToInventoryStorage(getItemId, 1);
        }
    }

    private GameObject makeWeapon(int type)
    {
        // 기존무기 삭제
        foreach (Transform child in arm.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        foreach (Transform child in leftarm.transform)
        {
            GameObject.Destroy(child.gameObject);
        }

        // 무기생성
        GameObject WeaponManager = GameObject.FindGameObjectWithTag("WeaponManager");
        var Weapon = WeaponManager.GetComponent<WeaponManager>().makeWeapon(type);

        // 부모(팔)밑에 붙임
        Weapon.transform.parent = arm.transform;
        GameObject Weapon2 = null;
        if (type == 3)
        {
            Weapon2 = WeaponManager.GetComponent<WeaponManager>().makeWeapon(4);
            Weapon2.transform.parent = leftarm.transform;
        }

        // 기본 회전은 0도
        Weapon.transform.localRotation = Quaternion.identity;

        // 포지션 잡아줌
        Weapon.transform.position = arm.transform.position;

        // 활일때만 다르게 
        if (Weapon2 != null)
        {
            myWeapon = Weapon;
            myWeapon2 = Weapon2;

            Weapon2.transform.localRotation = Quaternion.identity;
            Weapon2.transform.position = leftarm.transform.position;
            bowPos = Weapon.transform.localPosition + new Vector3(2.04f, -0.19f, 0.17f);
            bowPos2 = Weapon2.transform.localPosition + new Vector3(-0.17f, -1.2f, 0.7f);

            myWeapon.transform.localRotation = Quaternion.Euler(0, -2.7f, 82.74f);
            myWeapon.transform.localPosition = bowPos;
            myWeapon2.transform.localRotation = Quaternion.Euler(-28.58f, -1.1f, -12f);
            myWeapon2.transform.localPosition = bowPos2;
        }
        return Weapon;
    }

    public void equipOneHand()
    {
        myWeapon = makeWeapon(0);
        WeaponType = 12;
        MeleeType = 1;
    }

    public void equipTwoHand()
    {
        myWeapon = makeWeapon(1);
        WeaponType = 12;
        MeleeType = 2;
    }

    public void equipSpear()
    {
        myWeapon = makeWeapon(2);
        WeaponType = 12;
        MeleeType = 0;
    }

    public void equipBow()
    {
        myWeapon = makeWeapon(3);
        WeaponType = 11;
    }

    public bool isAttack()
    {
        return Attack;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 한번 맞으면 1.0초이내에는 안맞음
        if (passTime < 1.0f)
            return;

        if (other.tag == "Monster")
        {
            if(other.GetComponent<Monster>().getDeath() == false)
                PlayerData.HPminus(10);
        }

        if (other.tag == "MonsterWeapon")
        {
            if(other.transform.root.GetComponent<Monster>().getDeath() == false)
                PlayerData.HPminus(10);
        }

        passTime = 0;
        // 맞는 이펙트 넣기
    }
}
