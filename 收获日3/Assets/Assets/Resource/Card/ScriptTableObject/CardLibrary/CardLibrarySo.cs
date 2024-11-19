using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="CardLibrarySo",menuName = "Card/CardLibrarySo")]
public class CardLibrarySo : ScriptableObject
{
    public List<CardFile> CardLibraryList;

    [System.Serializable]
    public struct CardFile
    {
        public CardData CardData;
        public int number;
    } 


}
