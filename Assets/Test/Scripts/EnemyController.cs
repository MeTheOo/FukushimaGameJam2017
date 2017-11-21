using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
  Transform _player;               // Reference to the player's position.
  PlayerController _playerController;      // Reference to the player's health.
  public EnemyHealth _enemyHealth;        // Reference to this enemy's health.
  UnityEngine.AI.NavMeshAgent _nav;               // Reference to the nav mesh agent.

  void Awake ()
  {
    // Set up the references.
    _player = GameObject.FindGameObjectWithTag ("Player").transform;
    _playerController = _player.GetComponent <PlayerController> ();
    _enemyHealth = GetComponent <EnemyHealth> ();
    _nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
  }


  void Update ()
  {
      // If the enemy and the player have health left...
    if(_enemyHealth.currentHealth > 0 && _playerController.currentHealth > 0)
    {
            // ... set the destination of the nav mesh agent to the player.
        
      _nav.SetDestination (_player.position);
    }
    // Otherwise...
    else
    {
      // ... disable the nav mesh agent.
      _nav.enabled = false;
    }
  }
}
