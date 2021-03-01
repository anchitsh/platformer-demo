using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public GameObject player;

    public void Start()
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    public void Spawn()
    {
        player.GetComponent<Rigidbody2D>().gravityScale = 1;

    }
}
