using CardGrid.Types;
using UnityEngine;

namespace CardGrid
{
    [CreateAssetMenu(menuName = "Create CardData", fileName = "CardData", order = 0)]
    public class CardDataSo : ScriptableObject
    {
        public CardRarity rarity;
        public CardType   type;
    
        public Sprite sprite;
        public string cardName;
        public string cardDescription;
    }
}