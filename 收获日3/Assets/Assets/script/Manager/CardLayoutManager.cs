using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;
    public float maxWidth = 7f;//全部最大间隙
    public float cardSpacing = 2f;//单个最大间隙

    [Header("弧度参数")]
    public float originalAngle = 7f;
    public float angleBetweenCards = 7f;//当个角度    
    public float radius = 17f;//半径
    public Vector3 centerCircle;//圆心
    public float maxSumAngle = 49f;//全部最大角度
    public float CardAngle;//每个卡牌角度

    public List<Vector3> cardPositons;
    public List<Quaternion> cardQuanternions;

    public Vector3 centerPoint;



    public CardTranform GetCardTranform(int indx)
    {
        
        return new CardTranform(cardPositons[indx], cardQuanternions[indx]);
    }
     
    public void SetCardPostion(int totalCards)
    {
        cardPositons.Clear();
        cardQuanternions.Clear();
        if (isHorizontal)
        {


            float currentWidth = cardSpacing * (totalCards - 1);//全部间隙
            float totalWidth = Mathf.Min(currentWidth, maxWidth);
            float currentSpacing = totalWidth > 0 ? totalWidth / (totalCards - 1) : 0;

            for(int i = 0; i < totalCards; i++)
            {
                float xPos = 0 - (totalWidth / 2) + (i * currentSpacing);
                Vector3 cardPos = new Vector3(xPos, centerPoint.y, -i*0.2f);
                var rotation = Quaternion.identity;
                cardPositons.Add(cardPos);
                cardQuanternions.Add(rotation);
            }
        }
        else
        {
            float SumAngle = angleBetweenCards * totalCards;
            float cardAngle;
            if (SumAngle > maxSumAngle)
            {
                CardAngle = maxSumAngle / totalCards;
                angleBetweenCards = CardAngle;
                cardAngle = (maxSumAngle / originalAngle) * originalAngle / 2;//保持左边角度固定

            }
            else
            {
                 cardAngle = (totalCards - 1) * originalAngle / 2;
                angleBetweenCards = originalAngle;
            }
            

            Debug.Log("cardAngle:"+cardAngle);
            for (int i = 0; i < totalCards; i++)
            {
                
                Debug.Log(cardAngle - i * angleBetweenCards);
                var pos = FanCardPos((cardAngle - i * angleBetweenCards), i);
                var rotation = Quaternion.Euler(0, 0, cardAngle - i * angleBetweenCards);
                cardPositons.Add(pos);
                cardQuanternions.Add(rotation);
            }
            //计算(单卡最大角度)*number=sumAngle
        }


    }

    public Vector3 FanCardPos(float angle,int indx)
    {
        return new Vector3(
            centerPoint.x - Mathf.Sin(Mathf.Deg2Rad * angle) * radius,
            centerPoint.y + Mathf.Cos(Mathf.Deg2Rad * angle) * radius,
            -indx * 0.2f
            );
    }


}
