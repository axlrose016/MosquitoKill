using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPosition : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RandomPos(collision.gameObject.tag);
    }

    public void RandomPos(string col)
    {
        float x = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).x, Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x);
        float y = Random.Range(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)).y, Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)).y);
        if (col == "Reactor" || col == "Ground")
            this.transform.position = new Vector2(x, y);

    }
}
