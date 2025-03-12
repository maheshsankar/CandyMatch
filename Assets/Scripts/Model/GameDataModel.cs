using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandyMatch.Model 
{
    public enum GameMode 
    {
        EASY = 0,
        MEDIUM = 1,
        HARD = 2
    }

    [System.Serializable]
    public class GameLayout
    {
        public int rowCount;
        public int columnCount;
    }

    [System.Serializable]
    public class GameLevelData 
    {
        public GameMode gameMode;
        public List<GameLayout> gameLayouts;
        public List<Card> cards;
    }

    [System.Serializable]
    public class Card
    {
        public int cardID;
        public Sprite cardIcon;
    }
}

