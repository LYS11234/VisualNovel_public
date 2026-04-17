using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using System.Linq;
using System.IO;


public class InterfaceManager : MonoBehaviour
{
    #region Portrait & Panels
    [SerializeField]
    private Text charName;
    [SerializeField]
    private Text dialogue;
    [SerializeField]
    private Button btn0;
    [SerializeField]
    private Button btn1;
    [SerializeField]
    private Button btn2;
    [SerializeField]
    private RawImage background;
    [SerializeField]
    private SpriteRenderer leftPortrait;
    [SerializeField]
    private SpriteRenderer rightPortrait;
    [SerializeField]
    private AudioSource typeSound;
    List<Dictionary<string, object>> dic = new List<Dictionary<string, object>>();

    [SerializeField]
    private Animator autoBtnAnim;
    #endregion


    private bool dialogueEnd;
    private bool coroutineStop;
    public bool isAuto;
    [SerializeField]
    private bool buttonEnter;


    [SerializeField]
    private Animator anim;

    void Start()
    {
        StartCoroutine(ShowDialogue()); //ShowDialogue 라는 이름의 함수 실행
        btn0.onClick.AddListener(Btn0); //btn0라는 버튼에 Btn0라는 함수를 실행시킬 수 있는 명령어 삽입
        btn0.gameObject.SetActive(false); //btn0를 비활성화
        btn1.onClick.AddListener(Btn1);
        btn1.gameObject.SetActive(false);
        btn2.onClick.AddListener(Btn2);
        btn2.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//마우스 0번 버튼 클릭(좌클릭)을 했다면
        {
            if (Time.timeScale > 0 && anim.GetBool("Appear")) //현실 시간 1초당 흐르는 시간이 0초 이상이며 애니메이션의 Appear이라는 변수가 true라면
            {
                if (!btn0.gameObject.activeSelf && dialogueEnd)
                {
                    
                    
                    Database.instance.nowPlayer.dialogueNum++; //Database의 nowPlayer에 들어있는 dialogueNum에 1을 더한다.
                    coroutineStop = false;


                    StartCoroutine(ShowDialogue());
                }
                else if (!dialogueEnd && !buttonEnter)
                {
                    if (isAuto)
                    {
                        isAuto = false;
                    }
                    if (!buttonEnter && !btn0.gameObject.activeSelf)
                    {
                        coroutineStop = true; //Not Run
                        
                    }
                }
            }

        }

    }

    private IEnumerator ShowDialogue()
    {
        
        dialogueEnd = false;
        dic = CSVReader.Read(Database.instance.nowPlayer.dialogueFile);
        CheckBGM();
        PortraitCheck();
        BackgroundCheck();
        if (dic[Database.instance.nowPlayer.dialogueNum]["Character"].ToString() != "PlayerName")
            charName.text = dic[Database.instance.nowPlayer.dialogueNum]["Character"].ToString();
        else
            charName.text = Database.instance.nowPlayer.playerName;
        //Set Name
        

        dialogue.text = ""; //Reset Dialogue
        btn0.gameObject.SetActive(false);
        btn1.gameObject.SetActive(false);
        btn2.gameObject.SetActive(false);
        //Reset Button
        for (int i = 0; i < dic[Database.instance.nowPlayer.dialogueNum]["Script"].ToString().Length; i++)
        {
            if (dic[Database.instance.nowPlayer.dialogueNum]["Script"].ToString().Substring(i, 1) != "`")
                dialogue.text += dic[Database.instance.nowPlayer.dialogueNum]["Script"].ToString().Substring(i, 1);
            else if(dic[Database.instance.nowPlayer.dialogueNum]["Script"].ToString().Substring(i, 1) != "^")
            {
                dialogue.text += "\n";
            }
            else
            {
                for (int j = 0; j < Database.instance.nowPlayer.playerName.Length; j++)
                {
                    dialogue.text += Database.instance.nowPlayer.playerName.Substring(j, 1);
                    typeSound.Play();
                    yield return new WaitForSeconds(0.075f);
                }
            }
            typeSound.Play();
            if (dic[Database.instance.nowPlayer.dialogueNum]["Script"].ToString().Length < 300)
                yield return new WaitForSeconds(0.075f);
            else
            {
                yield return null;

            }

            if (coroutineStop)
            {
                dialogue.text = dic[Database.instance.nowPlayer.dialogueNum]["Script"].ToString();
                if (dialogue.text.Contains("`"))
                {
                    string[] st = new string[5];
                    st = dialogue.text.Split("`");
                    dialogue.text = st[0];
                    int j = 1;
                    while(j < st.Length)
                    {
                        dialogue.text += (Database.instance.nowPlayer.playerName + st[j]);
                        j++;
                    }


                }
                if(dialogue.text.Contains("^"))
                {
                    string[] st = new string[5];
                    st = dialogue.text.Split("^");
                    dialogue.text = st[0];
                    int j = 1;
                    while (j < st.Length)
                    {
                        dialogue.text += ("\n" + st[j]);
                        j++;
                    }
                }
                break;
            }
            if(dialogue.rectTransform.sizeDelta.y >= 225)
            {
                dialogue.rectTransform.transform.Translate(0, (((dialogue.rectTransform.sizeDelta.y - 180) / 45) * 20), 0);
            }
            
        } //Make typing Animation Animation
        dialogueEnd = true; //Check Dialogue Ended

        ButtonCheck();

        if (dic[Database.instance.nowPlayer.dialogueNum]["File"].ToString() != "" && !isAuto)
        {
            Database.instance.nowPlayer.dialogueFile = dic[Database.instance.nowPlayer.dialogueNum]["File"].ToString();
            Database.instance.nowPlayer.dialogueNum = -1;

        } //Check Dialogue File

        if (dic[Database.instance.nowPlayer.dialogueNum]["Script"].ToString().Length > 300)
        {
            coroutineStop = false;
            Database.instance.nowPlayer.dialogueNum++;
            StartCoroutine(ShowDialogue());
        } //Check Long Sentence or not

        else if(isAuto && !btn0.gameObject.activeSelf)
        {
            if (dialogueEnd)
            {
                yield return new WaitForSeconds(0.3f);
                if (dic[Database.instance.nowPlayer.dialogueNum]["File"].ToString() == "")
                    Database.instance.nowPlayer.dialogueNum++;
                else
                {
                    Database.instance.nowPlayer.dialogueFile = dic[Database.instance.nowPlayer.dialogueNum]["File"].ToString();
                    dic = CSVReader.Read(Database.instance.nowPlayer.dialogueFile);
                    Database.instance.nowPlayer.dialogueNum = 0;
                }

                StartCoroutine(ShowDialogue());
            }
        }
    }

    private void ButtonCheck()
    {
        if (dic[Database.instance.nowPlayer.dialogueNum]["Btn0Txt"].ToString() == "")
        {
            btn0.gameObject.SetActive(false);
            btn1.gameObject.SetActive(false);
            btn2.gameObject.SetActive(false);
            if (dic[Database.instance.nowPlayer.dialogueNum]["Btn0P"].ToString() != "")
            {
                Database.instance.nowPlayer.dialogueNum += (Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn0P"].ToString())) - 1;
            }
        }
        else
        {
            btn0.gameObject.SetActive(true);
            btn0.GetComponentInChildren<Text>().text = dic[Database.instance.nowPlayer.dialogueNum]["Btn0Txt"].ToString();
            if (Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn0SelfEsteem"].ToString()) > Database.instance.nowPlayer.selfEsteem)
                btn0.gameObject.SetActive(false);

            if (dic[Database.instance.nowPlayer.dialogueNum]["Btn1Txt"].ToString() == "")
            {
                btn1.gameObject.SetActive(false);
                btn2.gameObject.SetActive(false);
            }
            else
            {
                btn1.gameObject.SetActive(true);
                btn1.GetComponentInChildren<Text>().text = dic[Database.instance.nowPlayer.dialogueNum]["Btn1Txt"].ToString();
                if (Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn1SelfEsteem"].ToString()) > Database.instance.nowPlayer.selfEsteem)
                    btn1.gameObject.SetActive(false);

                if (dic[Database.instance.nowPlayer.dialogueNum]["Btn2Txt"].ToString() == "")
                {
                    btn2.gameObject.SetActive(false);
                }
                else
                {
                    btn2.gameObject.SetActive(true);
                    btn2.GetComponentInChildren<Text>().text = dic[Database.instance.nowPlayer.dialogueNum]["Btn2Txt"].ToString();
                    if (Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn2SelfEsteem"].ToString()) > Database.instance.nowPlayer.selfEsteem)
                        btn2.gameObject.SetActive(false);
                }
            }
        }
    }

    private void CheckBGM()
    {
        if (dic[Database.instance.nowPlayer.dialogueNum]["BGM"].ToString() != "")
        {
            SoundManager.instance.bgm_.clip = Resources.Load<AudioClip>(dic[Database.instance.nowPlayer.dialogueNum]["BGM"].ToString());
            SoundManager.instance.bgm_.Play();
        }
    }

    private void BackgroundCheck()
    {
        if (dic[Database.instance.nowPlayer.dialogueNum]["Background"].ToString() != "")
        {
            background.rectTransform.sizeDelta = new Vector2(1920, 1920);
            background.texture = Resources.Load<Texture2D>("Images/" + dic[Database.instance.nowPlayer.dialogueNum]["Background"].ToString());
        }
    }

    private void PortraitCheck()
    {
        if (dic[Database.instance.nowPlayer.dialogueNum]["LeftSprite"].ToString() != "")
        {
            leftPortrait.sprite = Resources.Load<Sprite>("Images/Sprites/" + dic[Database.instance.nowPlayer.dialogueNum]["LeftSprite"].ToString());
            leftPortrait.gameObject.SetActive(true);


            if (dic[Database.instance.nowPlayer.dialogueNum]["RightSprite"].ToString() != "")
            {
                rightPortrait.sprite = Resources.Load<Sprite>("Images/Sprites/" + dic[Database.instance.nowPlayer.dialogueNum]["RightSprite"].ToString());
                rightPortrait.gameObject.SetActive(true);
            }
            else
                rightPortrait.gameObject.SetActive(false);
        }
        else
        {
            leftPortrait.gameObject.SetActive(false);
            rightPortrait.gameObject.SetActive(false);
        }
        //Set Sprites
    }
    public void Btn0()
    {
        
        if (dic[Database.instance.nowPlayer.dialogueNum]["Btn0Affection"].ToString() != "")
        {
            switch(dic[Database.instance.nowPlayer.dialogueNum]["ID"].ToString())
            {
                case "1":
                    Database.instance.nowPlayer.affection_first += Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn0Affection"].ToString());
                    break;
                case "2":
                    Database.instance.nowPlayer.affection_second += Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn0Affection"].ToString());
                    break;
                case "3":
                    Database.instance.nowPlayer.affection_third += Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn0Affection"].ToString());
                    break;
                default:
                    break;
            }
        }
        Database.instance.nowPlayer.dialogueNum += Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn0P"].ToString());
        coroutineStop = false;
        StartCoroutine(ShowDialogue());

    }

    public void Btn1()
    {
        Database.instance.nowPlayer.dialogueNum += Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn1P"].ToString());
        coroutineStop = false;
        StartCoroutine(ShowDialogue());
    }

    public void Btn2()
    {
        Database.instance.nowPlayer.dialogueNum += Int32.Parse(dic[Database.instance.nowPlayer.dialogueNum]["Btn2P"].ToString());
        coroutineStop = false;
        StartCoroutine(ShowDialogue());
    }

    public void AutoButton()
    {

        isAuto = !isAuto;
        if (dialogueEnd && !btn0.gameObject.activeSelf)
        {
            Database.instance.nowPlayer.dialogueNum++;
            dialogueEnd = false;
            coroutineStop = false;
            StartCoroutine(ShowDialogue());
        }
        //autoBtnAnim.SetBool("Auto", isAuto);
    }

    public void ButtonEnter()
    {
        buttonEnter = true;
    }

    public void ButtonExit()
    {
        buttonEnter = false;
    }


    public void UnlockCG()
    {
        Database.instance.userData.cg[0] = true;
        string saveData = JsonUtility.ToJson(Database.instance.userData);
        byte[] bytes0 = System.Text.Encoding.UTF8.GetBytes(saveData);
        string encodedBytes0 = System.Convert.ToBase64String(bytes0);
        byte[] bytes2 = System.Text.Encoding.Unicode.GetBytes(encodedBytes0);
        string encodedBytes2 = System.Convert.ToBase64String(bytes2);
        byte[] bytes3 = System.Text.Encoding.BigEndianUnicode.GetBytes(encodedBytes2);
        string encodedData = System.Convert.ToBase64String(bytes3);
        File.WriteAllText(Application.persistentDataPath + "/save/UserData/UserData.dat", encodedData);
    }
}
