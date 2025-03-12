using CandyMatch.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CandyMatch.Utilities;
using DG.Tweening;
using CandyMatch.LevelEditor;
using CandyMatch.Model;

namespace CandyMatch.Controllers
{
    public class GameplayManager : MonoBehaviour
    {
        [SerializeField] private GameLevelController gameLevelController;
        [SerializeField] private GridManager gridManager;

        private void Start()
        {
            CustomCoroutiner.Start(InitLevel());
        }

        private IEnumerator InitLevel()
        {
            GameLevelData gameLevelData = gameLevelController.GetSelectedGameLevelData(GameManager.GetSelectedLevelIndex);
            int randomLayoutIndex = Random.Range(0, gameLevelData.gameLayouts.Count);
            GameLayout gameLayout = gameLevelController.GetSelectedGameLayout(gameLevelData, randomLayoutIndex);

            gridManager.CreateGrid(gameLayout.rowCount, gameLayout.columnCount);
            gridManager.GenerateCardDatas(gameLayout.rowCount, gameLayout.columnCount, gameLevelData.cardIcons);
            yield return new WaitForSeconds(1);

            gridManager.GenerateCards();
        }
    }
}

