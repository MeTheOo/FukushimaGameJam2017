using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollider : MonoBehaviour
{
    public int Health = 10;
    public int Kill = 0;
    public int EndKillCount = 30;
    public int StartHealth = 10;
    public GameObject EnemyManager;
    bool inGame = true;
    public GameObject Restartbutton;

    public Text LeftHeathCount;
    public Text RightHeathCount;
    public Text PlayerInfo;
    public SoundManager Sound;
    // Use this for initialization
    private void Awake()
    {
        //Sound = FindObjectOfType<SoundManager>();
    }
    void Start()
    {
        //Sound.PlaySound("BGM_GameMain", SoundManager.SoundType.BGM_MAINGAME);
        Health = StartHealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(Vector3.zero);
        if (Health <= 0 && inGame)
        {
            inGame = false;
            for (int i = 0; i < EnemyManager.transform.childCount; i++)
            {
                EnemyManager.transform.GetChild(i).transform.Find("EnemyHitTrigger").GetComponent<EnemyHitScript>().Die();
            }
            EnemyManager.SetActive(false);
            Restartbutton.SetActive(true);
        }
        if (Kill >= EndKillCount)
        {
            inGame = false;
            for (int i = 0; i < EnemyManager.transform.childCount; i++)
            {
                EnemyManager.transform.GetChild(i).transform.Find("EnemyHitTrigger").GetComponent<EnemyHitScript>().Die();
            }
            EnemyManager.SetActive(false);
            Restartbutton.SetActive(true);
        }
        GetComponent<PlayerCollider>().Health = Health;

        if (LeftHeathCount)
        {
            LeftHeathCount.text = Health.ToString();
        }
        if (RightHeathCount)
        {
            RightHeathCount.text = Health.ToString();
        }

        if (PlayerInfo)
        {
            PlayerInfo.text = "Health: " + Health + "/ " + StartHealth + "\r\nKill: " + Kill + "/ " + EndKillCount;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>())
        {
            
            other.transform.Find("EnemyHitTrigger").GetComponent<EnemyHitScript>().Die();
            other.enabled = false;
            Health--;
        }
    }
}
