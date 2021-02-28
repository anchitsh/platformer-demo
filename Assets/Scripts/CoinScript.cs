using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public GameObject ScorePlus, CoinParticle, Player;
    public float magnetDistance, speed;
    private float playerPosition, bfPosition, distance;
    Vector2 playerDirection;

    public void Update()
    {
        playerPosition = Player.transform.position.x;
        bfPosition = transform.position.x;
        distance = Mathf.Abs(playerPosition - bfPosition);
        if (distance < magnetDistance)
        {
            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, step);
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            CoinManager.coins++;
            GameObject a = Instantiate(ScorePlus);
            a.transform.position = transform.position +new Vector3(0, .3f, 0);
            GameObject b = Instantiate(CoinParticle);
            b.transform.position = transform.position + new Vector3(0, 0, 0);
            Destroy(this.gameObject);
        }
    }
}
