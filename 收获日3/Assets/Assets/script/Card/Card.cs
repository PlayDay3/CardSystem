using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
using DG.Tweening;

public class Card : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,IPointerClickHandler, IPointerDownHandler
{
    public CardData CardData;
    public SpriteRenderer cardImage;
    public TextMeshPro Description;
    [Header("状态")]
    public bool isShow;
    [Header("原始位置")]
    public Vector3 OriginalPos;
    public Quaternion OriginalRoation;
    public int Soring;
    public Vector3 MovePos;
    // Start is called before the first frame update
    void Start()
    {
        cardImage.sprite=CardData.cardImage;
        Description.text = CardData.cardDescription;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOriginalData(Vector3 Pos,Quaternion Roa,int i)
    {
        OriginalPos= Pos;
        OriginalRoation = Roa;
        Soring= i;
    }
    //初始化
    public void InitializeCard()
    {
        cardImage.sprite = CardData.cardImage;
        Description.text = CardData.cardDescription;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {


    }
    public void ResetPos()
    {
        transform.DOMove(OriginalPos, 0.1f);
        transform.DORotateQuaternion(OriginalRoation, 0.1f);
        GetComponent<SortingGroup>().sortingOrder = Soring;
        isShow = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isShow)
        {
            isShow = true;
            Vector3 TargetPos = OriginalPos + MovePos;
            transform.DOMove(TargetPos, 0.1f);
            transform.DORotate(new Vector3(1, 1, 1), 0.1f);
            GetComponent<SortingGroup>().sortingOrder = 20;
        }
        else
        {
            ResetPos();
        }
    }
}
