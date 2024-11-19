using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="CardDataSo",menuName ="Card/CardDataSo")]
public class CardData : ScriptableObject
{
    // Start is called before the first frame update
    public string cardName;
    public Sprite cardImage;
    public string cardDescription;

}
