using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using DG.Tweening;

public class CardDeck : MonoBehaviour
{
    public cardManager CardManager;
    public CardLayoutManager CardLayoutManager;
    public Vector3 DeckPosition;
    public List<CardData> DrawDeck;//���ƶ�
    public List<CardData> DisCardDeck;//���ƶ�
    public List<Card> CurrentDeck;//���ƶ�


    private void Start()
    {
        InitiallizeDeck();
        
    }
    public void InitiallizeDeck()//��ʼ�����ƶ�
    {
        DrawDeck.Clear();
        foreach (var Cardfile in CardManager.cardLibrary.CardLibraryList)
        {
            
            for(int i = 0; i < Cardfile.number; i++)
            {
                DrawDeck.Add(Cardfile.CardData);//�ڳ��ƶ���ӿ�������
            }
        }

    }

    [ContextMenu("TextDrawCard")]
    public void TextDraw()
    {
        DrawCard(1);
    }
    public void DrawCard(int number)//����
    {
        for(int i = 0; i < number; i++)
        {
            if (DrawDeck.Count == 0)
            {
                return;
            }
            CardData TempCardData= DrawDeck[0];
            DrawDeck.RemoveAt(0);
            
            var card =CardManager.GetCardObj().GetComponent<Card>();//�ڶ�����л�ȡ���ƶ����е�Card
            card.CardData = TempCardData;
            
            card.InitializeCard();//��ʼ��
            card.transform.position = DeckPosition;
            
            //card.transform.DOScale(new Vector3(1.2f,1.2f,2f), 1.5f);//����
            card.transform.DORotate(new Vector3(0,360, 0), 1f, RotateMode.LocalAxisAdd);

            //�Ŵ�1.2,��ת���ƶ�����1
            //��С��1,�ƶ�����2
            CurrentDeck.Add(card);

        }

        SetCardLayout();
    }

    public void SetCardLayout()
    {
        CardLayoutManager.SetCardPostion(CurrentDeck.Count);//����λ��
        for (int i=0;i< CurrentDeck.Count; i++)
        {
            Card currentCard = CurrentDeck[i];
            CardTranform cardTranform = CardLayoutManager.GetCardTranform(i);//��ȡλ��
            
            

            currentCard.transform.DOScale(Vector3.one, 1f).onComplete = () =>
            {
                currentCard.transform.DOMove(cardTranform.Positon, 1f);
                currentCard.transform.DORotateQuaternion(cardTranform.rotation, 1f);
            };



            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            
        }
    }

}
