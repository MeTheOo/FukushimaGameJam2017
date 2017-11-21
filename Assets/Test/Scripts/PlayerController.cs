using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public int startingHealth = 100;                            // The amount of health the player starts the game with.
  public int currentHealth;                                   // The current health the player has.

  void Awake ()
  {
    // Set the initial health of the player.
    currentHealth = startingHealth;
  }

}
