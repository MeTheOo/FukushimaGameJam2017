using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHitScript : MonoBehaviour
{
    public GameObject Player;
    public float ForceHight = 3;
    public UnityEvent OnHit;
    public UnityEvent OnDeath;
    public GameObject ParentObj;
    public int Heath = 1;
    // Use this for initialization
    void Start()
    {
        Player = FindObjectOfType<SteamVR_Camera>().gameObject;

        ParentObj = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PickedUpItem>() && other.GetComponent<PickedUpItem>().Picking == false && !other.GetComponent<PickedUpItem>().Waiting)
        {
            Rigidbody rigi = other.GetComponent<Rigidbody>();
            rigi.velocity = Vector3.zero;
            Vector3 newVel = other.transform.position - Player.transform.position;
            newVel *= -1;
            rigi.AddForce((newVel + new Vector3(0, ForceHight, 0)) * 100);
            Heath--;
            if (Heath > 0)
            {
                OnHit.Invoke();
                //other.GetComponent<PickedUpItem>().OnHitSound();
                other.transform.Find("HitSound").GetComponent<AudioSource>().Play();
            }
            else
            {
                ParentObj.GetComponent<EnemyHealth>().currentHealth = 0;
                GameObject.FindWithTag("Player").GetComponent<PlayerCollider>().Kill++;
                OnDeath.Invoke();
                //Die();
                StartCoroutine(DieAction());
                //other.GetComponent<PickedUpItem>().OnKillSound();
                other.transform.Find("KillSound").GetComponent<AudioSource>().Play();
            }
        }
    }

    public void Die()
    {
        Destroy(ParentObj);
    }
    IEnumerator DieAction()
    {
        ParentObj.transform.Find("star").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Destroy(ParentObj);
        yield break;
    }
}
