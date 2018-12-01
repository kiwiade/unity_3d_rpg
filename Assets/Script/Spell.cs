using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spell : MonoBehaviour {

    [HideInInspector]
    public float DTime = 0;
    public float FTime = 0;
    public int D_ID = 10;
    public int F_ID = 10;
    public int skillpoint = 2;

    public GameObject D_Image;
    public GameObject F_Image;

    [Space]
    public GameObject D_CooldownImage;
    public GameObject D_CooldownText;

    [Space]
    public GameObject F_CooldownImage;
    public GameObject F_CooldownText;

    [Space]
    public Text SkillPointText;

    [Header("Resource")]
    [SerializeField]
    private GameObject Snow;
    [SerializeField]
    private Texture2D DefaultCursor;
    [SerializeField]
    private Texture2D TeleportCursor;
    [SerializeField]
    private GameObject TeleportWindow;

    [Space]
    [SerializeField]
    private GameObject FlashEffect;

    [Header("Skill List")]
    public GameObject[] Skill;

    [Space]
    [Header("Sound")]
    [SerializeField]
    private AudioClip FlashSound;
    [SerializeField]
    private AudioClip GhostSound;
    [SerializeField]
    private AudioClip HealSound;

    //private bool teleport = false;

    private GameObject player = null;
    private Animator ani = null;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        ani = player.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if (DTime > 0)
        {
            DTime -= Time.deltaTime;
            D_CooldownText.GetComponent<Text>().text = (Mathf.FloorToInt(DTime) + 1).ToString();
            if (DTime <= 0)
            {
                DTime = 0;
                D_CooldownText.GetComponent<Text>().text = "";

                D_CooldownImage.SetActive(false);
                D_CooldownText.SetActive(false);
            }
            PlayerData.D_SpellTime = DTime;
        }
        if (FTime > 0)
        {
            FTime -= Time.deltaTime;
            F_CooldownText.GetComponent<Text>().text = (Mathf.FloorToInt(FTime) + 1).ToString();
            if (FTime <= 0)
            {
                FTime = 0;
                F_CooldownText.GetComponent<Text>().text = "";

                F_CooldownImage.SetActive(false);
                F_CooldownText.SetActive(false);
            }
            PlayerData.F_SpellTime = FTime;
        }


        if (Input.GetKeyDown(KeyCode.D))
        {
            if(D_ID == 1)
            {
                PlayerData.MPplus(30);
            }

            if (PlayerData.getMP() >= 30)
            {
                if (DTime <= 0)
                {
                    if (D_ID != 10)
                    {
                        D_Skill();
                        PlayerData.MPminus(30);
                        D_CooldownImage.SetActive(true);
                        D_CooldownText.SetActive(true);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (F_ID == 1)
            {
                PlayerData.MPplus(30);
            }

            if (PlayerData.getMP() >= 30)
            {
                if (FTime <= 0)
                {
                    if (F_ID != 10)
                    {
                        F_Skill();
                        PlayerData.MPminus(30);
                        F_CooldownImage.SetActive(true);
                        F_CooldownText.SetActive(true);
                    }
                }
            }
        }
    }


    public void skillup(int id)
    {
        // SP가 없으면 실행x
        if (skillpoint == 0)
            return;

        // D나 F 스킬중에 같은게 있으면 실행x
        if (D_ID == id || F_ID == id)
            return;

        // D가 비어있으면 D에, F가 비어있으면 F에 id를 넣음
        if (D_ID == 10)
        {
            D_ID = id;
            PlayerData.D_Spell = id;

            if (D_Image.GetComponent<Image>().sprite == null)
            {
                // 0번 = 아이콘
                D_Image.GetComponent<Image>().sprite = Skill[id].transform.GetChild(0).GetComponent<Image>().sprite;
                D_Image.GetComponent<Image>().color = Color.white;
            }
        }
        else if (F_ID == 10)
        {
            F_ID = id;
            PlayerData.F_Spell = id;

            if (F_Image.GetComponent<Image>().sprite == null)
            {
                F_Image.GetComponent<Image>().sprite = Skill[id].transform.GetChild(0).GetComponent<Image>().sprite;
                F_Image.GetComponent<Image>().color = Color.white;
            }
        }

        Skill[id].GetComponent<Outline>().effectColor = Color.red;
        Skill[id].GetComponent<Outline>().effectDistance = new Vector2(2, 2);
        // 2 = plus버튼, 3 = minus
        Skill[id].transform.GetChild(2).gameObject.SetActive(false);
        Skill[id].transform.GetChild(3).gameObject.SetActive(true);

        skillpoint--;
        SkillPointText.text = skillpoint.ToString();
    }

    public void skilldown(int id)
    {
        if (id == 4)
        {
            Cursor.SetCursor(DefaultCursor, new Vector2(30f, 0), CursorMode.Auto);
        }

        if (D_ID == id)
        {
            D_ID = 10;
            PlayerData.D_Spell = 10;

            D_Image.GetComponent<Image>().sprite = null;
            Color whitealpha = Color.white;
            whitealpha.a = 0;
            D_Image.GetComponent<Image>().color = whitealpha;
        }
        else if (F_ID == id)
        {
            F_ID = 10;
            PlayerData.F_Spell = 10;

            F_Image.GetComponent<Image>().sprite = null;
            Color whitealpha = Color.white;
            whitealpha.a = 0;
            F_Image.GetComponent<Image>().color = whitealpha;
        }

        Skill[id].GetComponent<Outline>().effectColor = Color.black;
        Skill[id].GetComponent<Outline>().effectDistance = new Vector2(1, 1);

        Skill[id].transform.GetChild(2).gameObject.SetActive(true);
        Skill[id].transform.GetChild(3).gameObject.SetActive(false);

        skillpoint++;
        SkillPointText.text = skillpoint.ToString();
    }



    private void D_Skill()
    {
        switch (D_ID)
        {
            case 0:
                StartCoroutine(Heal('D'));
                break;
            case 1:
                StartCoroutine(Clearity('D'));
                break;
            case 2:
                Ghost('D');
                break;
            case 3:
                Flash('D');
                break;
            case 4:
                Teleport('D');
                break;
            case 5:
                Snowball('D');
                break;
            default:
                throw new System.NotSupportedException();
        }
    }

    private void F_Skill()
    {
        switch (F_ID)
        {
            case 0:
                StartCoroutine(Heal('F'));
                break;
            case 1:
                StartCoroutine(Clearity('F'));
                break;
            case 2:
                Ghost('F');
                break;
            case 3:
                Flash('F');
                break;
            case 4:
                Teleport('F');
                break;
            case 5:
                Snowball('F');
                break;
            default:
                throw new System.NotSupportedException();
        }
    }


    // 스펠
    public IEnumerator Heal(char spell)
    {
        GetComponent<AudioSource>().PlayOneShot(HealSound);
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 3; i++)
        {
            PlayerData.HPplus(10);
            yield return new WaitForSeconds(1.0f);
        }
        spellTime(spell, 10);
        yield return null;
    }

    public IEnumerator Clearity(char spell)
    {
        GetComponent<AudioSource>().PlayOneShot(HealSound);
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < 3; i++)
        {
            PlayerData.MPplus(10);
            yield return new WaitForSeconds(1.0f);
        }
        spellTime(spell, 10);
        yield return null;
    }

    public void Ghost(char spell)
    {
        ani.SetFloat("RunSpeed", 2.0f);
        Invoke("resetSpeed", 5.0f);

        GetComponent<AudioSource>().PlayOneShot(GhostSound);

        spellTime(spell, 10);
    }

    private void resetSpeed()
    {
        ani.SetFloat("RunSpeed", 1.0f);
    }

    public void Flash(char spell)
    {
        Vector3 rot = player.transform.rotation.eulerAngles;
        float rad = rot.y * Mathf.Deg2Rad;

        Instantiate(FlashEffect, player.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        Vector3 flashVec = new Vector3(Mathf.Sin(rad) * 7.0f, 0, Mathf.Cos(rad) * 7.0f);
        player.transform.position += flashVec;

        Instantiate(FlashEffect, player.transform.position + new Vector3(0, 1, 0), Quaternion.identity);

        GetComponent<AudioSource>().PlayOneShot(FlashSound);

        spellTime(spell, 20);
    }

    public void Teleport(char spell)
    {
        Cursor.SetCursor(TeleportCursor, Vector2.zero, CursorMode.Auto);
        if (TeleportWindow.activeSelf == false)
            TeleportWindow.SetActive(true);

        spellTime(spell, 30);
    }

    public void Snowball(char spell)
    {
        var snowball = Instantiate(Snow);

        Vector3 rot = player.transform.rotation.eulerAngles;
        float rad = rot.y * Mathf.Deg2Rad;
        Vector3 moveVec = new Vector3(Mathf.Sin(rad), 0, Mathf.Cos(rad));

        snowball.transform.position = player.transform.position + new Vector3(0, 1.5f);
        snowball.GetComponent<Snowball>().MoveDir = moveVec;
        Destroy(snowball, 2.0f);

        spellTime(spell, 10);
    }



    public void spellTime(char spell, int time)
    {
        if (spell == 'D')
            DTime = time;
        else if (spell == 'F')
            FTime = time;
    }
}
