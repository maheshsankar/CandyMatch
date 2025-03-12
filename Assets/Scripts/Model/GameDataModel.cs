using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CandyMatch.Model 
{
    /// <summary>
    /// Enums defined for game selection
    /// </summary>
    public enum GameMode 
    {
        EASY = 0,
        MEDIUM = 1,
        HARD = 2
    }

    /// <summary>
    /// Model for Grid Layout
    /// </summary>
    [System.Serializable]
    public class GameLayout
    {
        public int rowCount;
        public int columnCount;
    }

    /// <summary>
    /// Model for Game Level Data
    /// </summary>
    [System.Serializable]
    public class GameLevelData 
    {
        public GameMode gameMode;
        public List<GameLayout> gameLayouts;
        public List<Card> cards;
    }

    /// <summary>
    /// Model for Card Data
    /// </summary>
    [System.Serializable]
    public class Card
    {
        public int cardID;
        public Sprite cardIcon;
    }
}

