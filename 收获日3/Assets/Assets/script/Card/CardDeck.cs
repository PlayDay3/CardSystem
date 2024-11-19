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
    public List<CardData> DrawDeck;//抽牌堆
    public List<CardData> DisCardDeck;//弃牌堆
    public List<Card> CurrentDeck;//手牌堆


    private void Start()
    {
        InitiallizeDeck();
        
    }
    public void InitiallizeDeck()//初始化抽牌堆
    {
        DrawDeck.Clear();
        foreach (var Cardfile in CardManager.cardLibrary.CardLibraryList)
        {
            
            for(int i = 0; i < Cardfile.number; i++)
            {
                DrawDeck.Add(Cardfile.CardData);//在抽牌队添加卡牌数据
            }
        }

    }

    [ContextMenu("TextDrawCard")]
    public void TextDraw()
    {
        DrawCard(1);
    }
    public void DrawCard(int number)//抽牌
    {
        for(int i = 0; i < number; i++)
        {
            if (DrawDeck.Count == 0)
            {
                return;
            }
            CardData TempCardData= DrawDeck[0];
            DrawDeck.RemoveAt(0);
            
            var card =CardManager.GetCardObj().GetComponent<Card>();//在对象池中获取卡牌对象中的Card
            card.CardData = TempCardData;
            
            card.InitializeCard();//初始化
            card.transform.position = DeckPosition;
            
            //card.transform.DOScale(new Vector3(1.2f,1.2f,2f), 1.5f);//缩放
            card.transform.DORotate(new Vector3(0,360, 0), 1f, RotateMode.LocalAxisAdd);

            //放大到1.2,翻转，移动到点1
            //缩小到1,移动到点2
            CurrentDeck.Add(card);

        }

        SetCardLayout();
    }

    public void SetCardLayout()
    {
        CardLayoutManager.SetCardPostion(CurrentDeck.Count);//计算位置
        for (int i=0;i< CurrentDeck.Count; i++)
        {
            Card currentCard = CurrentDeck[i];
            CardTranform cardTranform = CardLayoutManager.GetCardTranform(i);//获取位置
            
            

            currentCard.transform.DOScale(Vector3.one, 1f).onComplete = () =>
            {
                currentCard.transform.DOMove(cardTranform.Positon, 1f);
                currentCard.transform.DORotateQuaternion(cardTranform.rotation, 1f);
            };



            currentCard.GetComponent<SortingGroup>().sortingOrder = i;
            
        }
    }

}
