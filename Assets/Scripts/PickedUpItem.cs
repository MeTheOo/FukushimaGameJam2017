using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickedUpItem : MonoBehaviour
{
    public bool Picking = false;
    public bool holding = false;
    public bool Waiting = true;
    public UnityEvent OnPick;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Waiting)
        {
            GetComponent<Rigidbody>().useGravity = false;
            GetComponent<Rigidbody>().isKinematic = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Floor")
        {
            Destroy(gameObject);
        }
    }
    public void OnHitSound()
    {
        SoundManager sound = FindObjectOfType<SoundManager>();
        sound.PlaySound("EnemyDamage", SoundManager.SoundType.SE_ENEMY_DAMAGE);
    }
    public void OnKillSound()
    {
        SoundManager sound = FindObjectOfType<SoundManager>();
        sound.PlaySound("EnemyDeath", SoundManager.SoundType.SE_ENEMY_DEATH);
    }
}
