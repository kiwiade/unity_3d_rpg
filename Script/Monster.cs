using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public class Monster : MonoBehaviour {

    public Transform[] Patrol = new Transform[4];
    private int destPoint = 0;
    private NavMeshAgent agent = null;
    private bool patrol = true;
    private Animator ani = null;
    private bool detect = false;
    private int monsterHP = 3;
    private bool monsterDeath = false;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < Patrol.Length; i++)
        {
            string name = "Patrol" + (i + 1).ToString();
            Patrol[i] = GameObject.Find(name).transform;
        }

        ani = GetComponent<Animator>();
        ani.SetBool("Static_b", true);

        agent = GetComponent<NavMeshAgent>();
        GotoNext();
        patrol = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (!agent.pathPending && agent.remainingDistance < 0.5f && patrol == true)
            GotoNext();

        if (detect == true)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            agent.ResetPath();
            agent.destination = player.transform.position;

            if (agent.remainingDistance < 2.5f)
            {
                agent.ResetPath();
                ani.SetInteger("WeaponType_int", 12);
                ani.SetFloat("Speed_f", 0f);
            }
            else
            {
                ani.SetInteger("WeaponType_int", 0);
                ani.SetFloat("Speed_f", 0.5f);
            }
        }
        else
        {
            ani.SetInteger("WeaponType_int", 0);
            ani.SetFloat("Speed_f", 0.5f);
        }
    }

    private void GotoNext()
    {
        if (Patrol.Length <= 0)
            return;

        agent.destination = Patrol[destPoint].position;
        destPoint = (destPoint + 1) % Patrol.Length;

        ani.SetFloat("Speed_f", 0.5f);
    }

    // 적을 감지했을때
    public void PlayerDetect()
    {
        patrol = false;
        detect = true;
    }

    // 적이 범위 밖을 나갔을때 기본 패턴으로
    public void BasicPattern()
    {
        if (destPoint >= 1)
            destPoint--;
        else if (destPoint == 0)
            destPoint = 3;

        agent.ResetPath();
        patrol = true;
        detect = false;
    }

    public bool getDeath()
    {
        return monsterDeath;
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
                    monsterHP--;

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

                    // 유저 인벤토리에 아이템 들어가고 경험치업
                    player.GetComponent<MyCharacter>().getItem();
                    PlayerData.ExpPlus(300);

                    //죽는 애니메이션
                    GetComponent<Animator>().SetBool("Death_b", true);

                    // patrol 중지
                    agent.ResetPath();
                    patrol = false;

                    // 2초뒤 죽음
                    Destroy(gameObject, 2.0f);
                }
            }
        }
    }
}
