using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyMatch.Model;

namespace CandyMatch.LevelEditor
{
    [CreateAssetMenu(fileName = "GameLevelData", menuName = "CandyMatch/GameLevelDataEditor")]
    public class GameLevelController : ScriptableObject
    {
        public List<GameLevelData> gameLevelDatas;

        public GameLevelData GetSelectedGameLevelData(int levelIndex)
        {
            return gameLevelDatas.Find(x => (int)x.gameMode == levelIndex);
        }

        public GameLayout GetSelectedGameLayout(GameLevelData gameLevelData, int index)
        {
            return gameLevelData.gameLayouts[index];
        }
    }
}