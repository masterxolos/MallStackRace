using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BumpWithBots : MonoBehaviour
{
    public PlayerCollectController playerScript;
    public PlayerCollectController botScript1;
    public PlayerCollectController botScript2;
    [SerializeField] private GameObject fogPrefab;



    private Vector3 m_preVelocity = Vector3.zero;
    
    void Start()
    {
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bot1"))
        {
            StartCoroutine(waitForaSecForBot1(collision.gameObject));
                if (playerScript.boxAmount < botScript1.boxAmount )
            {
                botScript1.AddMultipleBox(playerScript.boxAmount);
                playerScript.RemoveMultipleBox(playerScript.boxAmount);
            }
            else if (playerScript.boxAmount > botScript1.boxAmount)
            {
                Debug.Log("player" + playerScript.boxAmount + "      bot1 " + botScript1.boxAmount);
                playerScript.AddMultipleBox(botScript1.boxAmount);

                botScript1.RemoveMultipleBox(botScript1.boxAmount);
            }
        }
        
        else if (collision.gameObject.CompareTag("Bot2"))
        {
            StartCoroutine(waitForaSecForBot2(collision.gameObject));
            if (playerScript.boxAmount < botScript2.boxAmount )
            {
                botScript2.AddMultipleBox(playerScript.boxAmount);
                playerScript.RemoveMultipleBox(playerScript.boxAmount);
            }
            else if (playerScript.boxAmount > botScript2.boxAmount)
            {
                Debug.Log("player" + playerScript.boxAmount + "      bot2 " + botScript2.boxAmount);
                playerScript.AddMultipleBox(botScript2.boxAmount);

                botScript2.RemoveMultipleBox(botScript2.boxAmount);
            }
        }
        /*
        ContactPoint contactPoint = collision.contacts[0];
        Vector3 newDir = Vector3.zero;
        Vector3 curDir = transform.TransformDirection(Vector3.forward);
        newDir = Vector3.Reflect(curDir, contactPoint.normal);
        Quaternion rotation = Quaternion.FromToRotation(Vector3.forward, newDir);
        transform.rotation = rotation;
        Debug.Log("normalized: " + newDir.normalized + "m_pre velocity: " + m_preVelocity.x + "m pre velocity normalized: " + m_preVelocity.normalized.x);
        gameObject.GetComponent<Rigidbody>().AddForce(newDir.normalized * m_preVelocity.x / m_preVelocity.normalized.x , 0);
        */
        if ((gameObject.CompareTag("Bot1") || gameObject.CompareTag("Bot2")) && collision.gameObject.CompareTag("Player"))
        {
            Vector3 dir = collision.contacts[0].point - transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = -dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force.
            // This will push back the player
            
            var newDir = new Vector3(dir.x, 0, dir.z);
            Debug.Log(newDir);
            //      transform.position += newDir * 5;
            var location =Vector3.RotateTowards(transform.position, newDir, 180,180);
            newDir = new Vector3(location.x * 8, location.y, location.z * 10);
            transform.DOMove(transform.position+newDir, 0.5f);
            //transform.position += newDir * 5;
            //GetComponent<Rigidbody>().AddForce(dir*20);
            
        }

    }
    IEnumerator waitForaSecForBot1(GameObject collision)
    {
        botScript1.StartAnimation();
        playerScript.StartAnimation();


        Instantiate(fogPrefab, ((collision.transform.position + new Vector3(0,0,5)) + (gameObject.transform.position)+ new Vector3(0,0,5))/2, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        botScript1.EndAnimation();
        playerScript.EndAnimation();
    }
    IEnumerator waitForaSecForBot2(GameObject collision)
    {
        botScript2.StartAnimation();
        playerScript.StartAnimation();
        //z+20
        
        Instantiate(fogPrefab, ((collision.transform.position + new Vector3(0,0,5)) + (gameObject.transform.position)+ new Vector3(0,0,5))/2, Quaternion.identity);
        yield return new WaitForSeconds(0.5f);
        botScript2.EndAnimation();
        playerScript.EndAnimation();
    }
}
