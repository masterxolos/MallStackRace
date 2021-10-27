using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PlayerCollectController : MonoBehaviour
{
    


    [SerializeField] private Transform shoppingCartTransform;
    
    [SerializeField] private Transform mainFallenGiftBoxes;

    private GameObject boxObject;

    public GameObject[] giftBox = new GameObject[10];
    public GameObject[] fallenGiftBox = new GameObject[6];

    public int boxAmount = 0;
    public float barValue = 1f;

    public Slider slider;
    public Image fill;

    public GameObject goToCheckout;

    public GameObject cashPrefab;
    public Transform PrefabLocation;
    public GameObject dislikePrefab;
    
    public GameObject firstboxPosition;
    
    [SerializeField] private Animator Animator;
    
    [SerializeField] private GameObject winCanvas;
    [SerializeField] private GameObject failCanvas;
    [SerializeField] private Canvas playerCanvas;
    [SerializeField] private GameObject topbarCanvas;

    private bool canFinish = false;
    private void OnCollisionEnter(Collision hit)
    {
        if (gameObject.CompareTag("Player"))
        {
            if (hit.gameObject.CompareTag("RightObject"))
            {
                var cash = new GameObject();
                cash.transform.parent = gameObject.transform;
                cash = Instantiate(cashPrefab, PrefabLocation.position, Quaternion.identity);
                CollectedObj(hit.gameObject);
                barValue++;
                FillSlider();
            }
            else if (hit.gameObject.CompareTag("WrongObject"))
            {
                Instantiate(dislikePrefab, PrefabLocation.position, Quaternion.identity);
                CollectedObj(hit.gameObject);
                if(barValue >= 0)
                    barValue--;
                FillSlider();
            }
        }
        else if(gameObject.CompareTag("Bot1") || gameObject.CompareTag("Bot2"))
        {
            if (hit.gameObject.CompareTag("RightObject"))
            {
                Destroy(hit.gameObject);
                AddBox("right");
            }
            else if (hit.gameObject.CompareTag("WrongObject"))
            {
                Destroy(hit.gameObject);
                AddBox("wrong");
            }
        }
        if (hit.gameObject.CompareTag("Checkout"))
        {
            Debug.Log(hit.gameObject.tag);
            if (gameObject.CompareTag("Player"))
            {
                if (canFinish)
                {
                    winCanvas.SetActive(true);
                    topbarCanvas.SetActive(false);
                    playerCanvas.enabled = false;
                }
            }

            else if (gameObject.CompareTag("Bot1") || gameObject.CompareTag("Bot2")) 
            {
                if (canFinish)
                {
                    winCanvas.SetActive(true);
                }
            }

            else if (gameObject.CompareTag("Bot2"))
            {
                if (canFinish)
                {
                    winCanvas.SetActive(true);
                }
            }
        }
    }

    private void CollectedObj(GameObject hit)
    {
        StartCoroutine(CollectObj());
        IEnumerator CollectObj()
        {
            hit.GetComponent<Collider>().enabled = false;
            hit.transform.parent.gameObject.transform.DORotate(new Vector3(0, 180, 0), 0.125f).SetEase(Ease.Linear).SetLoops(4);
            GameObject newItem = Instantiate(giftBox[boxAmount], hit.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(0.25f);
            newItem.SetActive(true);
            newItem.transform.parent = firstboxPosition.transform.parent;
            Destroy(hit);
            newItem.transform.DOLocalMove(giftBox[boxAmount].transform.localPosition, 0.5f);
            boxAmount++;
        }
    }

    private void FillSlider()
    {
        slider.value = barValue;
        fill.fillAmount = slider.value;
        if (slider.value == slider.maxValue)
        {
            goToCheckout.SetActive(true);
            canFinish = true;
        }
    }
    
    private void AddBox(string type)
    {
        if (type == "right")
        {
            giftBox[boxAmount].gameObject.SetActive(true);
            boxAmount++;
            barValue++;
            FillSlider();
        }
        
        else if (type == "wrong")
        {
            giftBox[boxAmount].gameObject.SetActive(true);
            boxAmount++;
            barValue++;
            FillSlider();
        }
        Debug.Log("eklendi : " + boxAmount);
    }

    public void AddMultipleBox(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            giftBox[boxAmount].gameObject.SetActive(true);
            boxAmount++;
            barValue++;
            FillSlider();
        }
    }
    
    private void RemoveBox()
    {
        if (boxAmount >= 0)
        {
            giftBox[boxAmount].gameObject.SetActive(false);
            boxAmount--;
            if(barValue >= 0)
                barValue--;
            FillSlider();
        }
    }

    public void RemoveMultipleBox(int amount)
    {
        Debug.Log("amount " + amount + "boxamount " +boxAmount );

        while (amount > -1)
        {
            amount--;
            giftBox[boxAmount].gameObject.SetActive(false);
            if (boxAmount != 0)
            {
                boxAmount--;
                if(barValue >= 0)
                    barValue--;
            }

            FillSlider();
        }
    }

    public void StartAnimation()
    {
        Animator.SetBool("isCrashed", true);
    }
    public void EndAnimation()
    {
        Animator.SetBool("isCrashed", false);
    }
}
