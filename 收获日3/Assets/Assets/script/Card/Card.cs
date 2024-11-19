using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public CardData CardData;
    public SpriteRenderer cardImage;
    public TextMeshPro Description;
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
    //≥ı ºªØ
    public void InitializeCard()
    {
        cardImage.sprite = CardData.cardImage;
        Description.text = CardData.cardDescription;
    }
}
