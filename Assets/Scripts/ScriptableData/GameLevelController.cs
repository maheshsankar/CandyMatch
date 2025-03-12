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

        /// <summary>
        /// Getting current selected Game Level data based on selected level index
        /// </summary>
        /// <param name="levelIndex"></param>
        /// <returns></returns>
        public GameLevelData GetSelectedGameLevelData(int levelIndex)
        {
            return gameLevelDatas.Find(x => (int)x.gameMode == levelIndex);
        }

        /// <summary>
        /// Getting current grid layout based on level mode and game level data
        /// </summary>
        /// <param name="gameLevelData"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public GameLayout GetSelectedGameLayout(GameLevelData gameLevelData, int index)
        {
            return gameLevelData.gameLayouts[index];
        }
    }
}