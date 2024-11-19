using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class CardLayoutManager : MonoBehaviour
{
    public bool isHorizontal;
    public float maxWidth = 7f;//ȫ������϶
    public float cardSpacing = 2f;//��������϶

    [Header("���Ȳ���")]
    public float originalAngle = 7f;
    public float angleBetweenCards = 7f;//�����Ƕ�    
    public float radius = 17f;//�뾶
    public Vector3 centerCircle;//Բ��
    public float maxSumAngle = 49f;//ȫ�����Ƕ�
    public float CardAngle;//ÿ�����ƽǶ�

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


            float currentWidth = cardSpacing * (totalCards - 1);//ȫ����϶
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
                cardAngle = (maxSumAngle / originalAngle) * originalAngle / 2;//������߽Ƕȹ̶�

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
            //����(�������Ƕ�)*number=sumAngle
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
