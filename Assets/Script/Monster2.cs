using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using UnityEngine.UI;

public class Monster2 : MonoBehaviour {

    private NavMeshAgent agent = null;
    public Transform[] Patrol = new Transform[4];
    private int destPoint = 0;

    private Animator ani = null;
    private int monsterHP = 5;
    private int monsterMaxHP = 5;
    private bool monsterDeath = false;
    private string state = "idle";
    private float restTime = 2.0f;

    public GameObject monsterHealthBar;
    public GameObject FloatingTextPrefab;

    // Use this for initialization
    void Start () {
        if (gameObject.tag == "Monster2")
        {
            for (int i = 0; i < Patrol.Length; i++)
            {
                string name = "Patrol" + (i + 1).ToString();
                Patrol[i] = GameObject.Find(name).transform;
            }
        }
        else if(gameObject.tag == "Monster3")
        {
            for (int i = 0; i < Patrol.Length; i++)
            {
                string name = "Patrol" + (i + 5).ToString();
                Patrol[i] = GameObject.Find(name).transform;
            }
        }

        ani = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        destPoint = Random.Range(0, 4);

        GotoNext();
	}
	
	// Update is called once per frame
	void Update () {
        monsterHealthBar.GetComponent<ProgressBar2>().currentPercent = ((float)monsterHP / monsterMaxHP) * 100;

        if (!monsterDeath)
        {
            if(state == "idle")
            {
                restTime -= Time.deltaTime;
                if (restTime < 0)
                {
                    GotoNext();
                    restTime = Random.Range(0, 2.0f);
                    state = "move";
                }
            }
            else if(state == "move")
            {
                if (!agent.pathPending && agent.remainingDistance < 0.2f)
                {
                    aniReset();
                    ani.SetBool("Idle", true);
                    state = "idle";
                }
            }
            else if(state == "detect")
            {
                if (agent.remainingDistance < 0.5f)
                {
                    agent.ResetPath();
                    aniReset();
                    ani.SetBool("Attack", true);
                }
                else
                {
                    var player = GameObject.FindGameObjectWithTag("Player");
                    agent.SetDestination(player.transform.position);

                    aniReset();
                    ani.SetBool("Move", true);
                }
            }
        }
    }

    private void GotoNext()
    {
        if (Patrol.Length <= 0)
            return;

        // 이동하는 애니메이션으로 변경
        aniReset();
        ani.SetBool("Move", true);

        // 다음좌표에서 x,z값 ±2로 목적지를 잡음
        float rx = Random.Range(-2.0f, 2.0f);
        float rz = Random.Range(-2.0f, 2.0f);
        agent.SetDestination(Patrol[destPoint].position + new Vector3(rx, 0, rz));

        // 다음좌표를 0~3중 랜덤으로
        destPoint = (destPoint + Random.Range(0, 4)) % Patrol.Length;
    }

    // 적을 감지했을때
    public void PlayerDetect()
    {
        state = "detect";
    }

    // 적이 범위 밖을 나갔을때 기본 패턴으로
    public void BasicPattern()
    {
        if (monsterDeath)
            return;

        if (destPoint >= 1)
            destPoint--;
        else if (destPoint == 0)
            destPoint = Patrol.Length - 1;

        agent.ResetPath();
        GotoNext();
        state = "move";
    }

    public bool getDeath()
    {
        return monsterDeath;
    }

    public void aniReset()
    {
        ani.SetBool("Idle", false);
        ani.SetBool("Move", false);
        //ani.SetBool("Damage", false);
        ani.SetBool("Attack", false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Weapon")
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player.GetComponent<MyCharacter>().isAttack() == true)
            {
                if (monsterHP > 0)
                {
                    monsterHP -= (int)PlayerData.getATK();

                    if (FloatingTextPrefab)
                    {
                        ShowFloatingText();
                    }

                    // 몬스터 밀려나게
                    Vector3 pushVec = transform.position - player.transform.position;
                    pushVec.y = 0;
                    pushVec.Normalize();

                    transform.LookAt(player.transform.position);
                    Vector3 vec = transform.position;
                    vec.y = 0;
                    transform.DOMove(vec + pushVec * 5.0f, 0.2f);
                }
                
                if (monsterHP <= 0 && monsterDeath == false)
                {
                    monsterDeath = true;

                    // 랜덤아이템생성 및 경험치업
                    RandomItemCreate();
                    PlayerData.MoneyPlus(1);
                    PlayerData.ExpPlus(300);

                    // 골드 이펙트
                    monsterHealthBar.transform.parent.GetChild(1).gameObject.SetActive(true);
                    monsterHealthBar.transform.parent.GetComponentInChildren<Text>().text = "+1";

                    //죽는 애니메이션
                    aniReset();
                    ani.SetBool("Die", true);

                    // patrol 중지
                    agent.ResetPath();

                    // 2초뒤 죽음
                    Destroy(gameObject, 2.0f);
                }
            }
        }
    }

    public void ShowFloatingText()
    {
        var go = Instantiate(FloatingTextPrefab, transform.position, transform.rotation, transform);
        go.transform.position += new Vector3(0, 2.0f, 0);
        go.transform.position += new Vector3(Random.Range(-0.5f, 0.5f), 0, 0);
        go.GetComponentInChildren<Text>().text = PlayerData.getATK().ToString();
    }

    public void RandomItemCreate()
    {
        int k = Random.Range(0, 10);
        if (k < 5)
            return;
        else if(k < 8)
        {
            // 포션
            GameObject item = (GameObject)Instantiate(Resources.Load("ItemOnTheGround") as GameObject);
            item.AddComponent<PickUpItem>();

            var itemDatabase = (ItemDataBaseList)Resources.Load("ItemDatabase");
            int id = Random.Range(125, 127);
            item.GetComponent<PickUpItem>().item = itemDatabase.getItemByID(id);
            item.transform.position = transform.position;
        }
        else if(k < 10)
        {
            // 장비
            GameObject item = (GameObject)Instantiate(Resources.Load("ItemOnTheGround") as GameObject);
            item.AddComponent<PickUpItem>();

            var itemDatabase = (ItemDataBaseList)Resources.Load("ItemDatabase");
            int id = Random.Range(121, 125);
            item.GetComponent<PickUpItem>().item = itemDatabase.getItemByID(id);
            item.transform.position = transform.position;
        }        
    }
}
