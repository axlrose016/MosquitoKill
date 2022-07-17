using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovement : MonoBehaviour
{
    [SerializeField] public float speed = 20;
    public Transform target;
   
    // Update is called once per frame
    void FixedUpdate()
    {
        GameObject GM = GameObject.FindGameObjectWithTag("GameController");
        bool isReady = GM.GetComponent<GameplayManager>().isGameStart;
        if (isReady)
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            target.GetComponent<RandomPosition>().RandomPos("Ground");
        }
    }
}
