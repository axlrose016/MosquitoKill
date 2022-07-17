using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoinSystem : MonoBehaviour
{
    Transform currPos, KoinLocation;
    public float speed = 0.5f;
    public ParticleSystem koinzParticle;
    void Start()
    {
        KoinLocation = GameObject.FindGameObjectWithTag("Koin").transform;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        bool isTargetNotInstantiated = GameObject.FindGameObjectWithTag("MainTarget") == null;
        bool koinCloneInstantiated = GameObject.FindGameObjectWithTag("KoinClone") != null;
        if (!isTargetNotInstantiated)
            currPos = GameObject.FindGameObjectWithTag("MainTarget").transform;

        if (koinCloneInstantiated)
        {
            GameObject koinClone = GameObject.FindGameObjectWithTag("KoinClone");
            koinClone.transform.position = Vector3.MoveTowards(koinClone.transform.position, KoinLocation.position, speed * Time.deltaTime);
            if (koinClone.transform.position == KoinLocation.position)
            {
                koinzParticle.Play();
                FindObjectOfType<AudioManager>().Play("KoinCollect");
                GameplayManager.PlayerKoinz++;
                GameObject.Destroy(GameObject.FindGameObjectWithTag("KoinClone"));
            }
        }       
    }

    public void InstantiateKoin()
    {
        GameObject koin = STC.InstantiateGameObject("Prefabs/imgKoins");
        koin.transform.SetParent(GameObject.FindGameObjectWithTag("Respawn").transform);
        koin.transform.localScale = GameObject.FindGameObjectWithTag("Koin").transform.localScale;
        koin.transform.position = currPos.position;
        koin.tag = "KoinClone";
    }
}
