using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Swipe : MonoBehaviour
{
    Vector2 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;
    [Range(0.05f, 10f)]
    public float throwForce = 0.3f;
    [SerializeField] bool isThrown = false;
    [SerializeField] bool isOnGround = false;
    [SerializeField] Sprite deathSprite = null;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ThrowAction();
    }

    void ThrowAction()
    {
        if(!isThrown && isOnGround)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchTimeStart = Time.time;
                startPos = Input.GetTouch(0).position;
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (startPos == endPos)
                {
                    isOnGround = true;
                    isThrown = false;
                    return;
                }
                else
                {
                    touchTimeFinish = Time.time;
                    timeInterval = touchTimeFinish - touchTimeStart;
                    endPos = Input.GetTouch(0).position;
                    direction = startPos - endPos;
                    if (endPos.y > startPos.y && rb.velocity.y <= 0)
                    {
                        GetComponent<Rigidbody2D>().AddForce(-direction / timeInterval * throwForce);
                        isOnGround = false;
                        isThrown = true;
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Ground":
                isOnGround = true;
                if(isThrown)
                {
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    gameObject.GetComponent<SpriteRenderer>().sprite = deathSprite;
                    StartCoroutine(DestroyThrowable(.5f));
                    FindObjectOfType<UIManager>().ShowGameOver();
                }
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "MainTarget":
                if (isThrown && !isOnGround)// && !isDestroyed)
                {
                    Animator anim = GameObject.FindGameObjectWithTag("MainTarget").GetComponent<Animator>();
                    anim.SetBool("isDead", true);
                    gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
                    gameObject.GetComponent<SpriteRenderer>().sprite = deathSprite;
                    GameObject.FindGameObjectWithTag("MainTarget").GetComponent<TargetMovement>().enabled = false;
                    StartCoroutine(DestroyAllUsable(.5f));
                    FindObjectOfType<KoinSystem>().InstantiateKoin();
                    FindObjectOfType<GameplayManager>().PlayerScore++;
                    GPGS.IncrementalAchievement(GPGSIds.achievement_the_elite);
                    GPGS.IncrementalAchievement(GPGSIds.achievement_the_god_of_hunter);
                    GPGS.IncrementalAchievement(GPGSIds.achievement_the_skillful_hunter);
                    string Throwable = PlayerPrefs.GetString("DefaultThrowable");
                    if (Throwable == "item1")
                        GPGS.IncrementalAchievement(GPGSIds.achievement_bulls_eye);
                }
                break;
            default:
                break;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Ground":
                    FindObjectOfType<AudioManager>().Play("Throw");
                    string throwable = !String.IsNullOrEmpty(PlayerPrefs.GetString("DefaultThrowable")) ? PlayerPrefs.GetString("DefaultThrowable") : "item1";
                    if (throwable != "item2")
                        GetComponent<Rigidbody2D>().AddTorque(360, ForceMode2D.Impulse);
                break;
            default:
                break;
        }
    }

    IEnumerator DestroyThrowable(float duration)
    {
        FindObjectOfType<AudioManager>().Play("Splat");
        yield return new WaitForSeconds(duration);
        GameObject.Destroy(this.gameObject);
    }

    IEnumerator DestroyAllUsable(float duration)
    {
        FindObjectOfType<AudioManager>().Play("Splat");
        yield return new WaitForSeconds(duration);
        GameObject.Destroy(this.gameObject);
        GameObject.Destroy(GameObject.FindGameObjectWithTag("MainTarget"));
    }
}
