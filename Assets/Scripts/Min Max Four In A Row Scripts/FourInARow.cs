using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class FourInARow : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI overheadText;
    [SerializeField] GameObject aiPuckPrefab;
    [SerializeField] GameObject playerPuckPrefab;
    [SerializeField] List<Transform> columnPositions = new List<Transform>();
    [SerializeField] int?[,] game = new int?[7,6];
    [SerializeField] int searchDepth = 6;

    private List<GameObject> puckList = new List<GameObject>();
    private List<int> columnsFull = new List<int>();

    private GameManager gameManager;

    private int columnsLength = 7;
    private int rowsLength = 6;

    private int player = 1;
    private int ai = 2;

    private bool gameInProgress = false;
    private bool playerTurn = false;
    
    private Random random = new Random();

    private void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        playerTurn = true;
    }

    public void PlacePuck(int position)
    {
        if (!gameInProgress)
        {
            gameInProgress = true;
            gameManager.timerPaused = true;
            Array.Clear(game, 0, game.Length);
        }

        if (playerTurn)
        {
            
            playerTurn = false;
            if (!PlayInColumn(player, position))
            {
                overheadText.text = "Column is full, Pick Another";
            }
            else
            {
                overheadText.text = "AI's Move";
                Debug.Log("DROPPING AT POSITION " + position);
                GameObject puck = Instantiate(playerPuckPrefab, columnPositions[position]);
                puck.transform.localScale = new Vector3(1, 1, 1);
                puckList.Add(puck);
            }

            if (!CheckIfGameOver(player))
            {
                AITurn();
            }
        }
        
    }


    private bool CheckIfGameOver(int whosTurn)
    {
        string winner = whosTurn == 1 ? "You" : "AI";
        if (CheckIfFull())
        {
            overheadText.text = "Game Over: Tie" + "\n" + "Pick a Column to play again";
            StartCoroutine(EndGame());
            return true;
        }
        else
        {
            if (CheckIfWinFound(whosTurn))
            {
                overheadText.text = "Game Over: " + winner + " Won " + "\n" + "Pick a Column to play again";
                StartCoroutine(EndGame());
                if(whosTurn == player)
                {
                    gameManager.AddDeathMoney(5, 30);
                }
                return true;
            }
        }
        return false;
    }

    private IEnumerator EndGame()
    {
        yield return new WaitForSeconds(4f);
        gameManager.timerPaused = false;
        gameInProgress = false;

        foreach (GameObject puck in puckList)
        {
            Destroy(puck);
        }

    }

    private void AITurn()
    {

        Hashtable potentialMoves = new Hashtable();
        for(int i = 0; i < columnsLength; i++)
        {
            if(PlayInColumn(ai, i))
            {
                potentialMoves.Add(i, GetMiniMaxScore(searchDepth, false));
                RemoveTopPuck(i);
            }
        }

        List<int> bestMoves = new();
        int maxValue = 0;
        foreach (int value in potentialMoves.Values)
        {
            if(value > maxValue)
            {
                maxValue = value;
            }
        }

        foreach(int key in potentialMoves.Keys)
        {
            if((int)potentialMoves[key] == maxValue)
            {
                bestMoves.Add(key);
            }
        }

        int randomColumnPick;
        if (bestMoves.Count <= 0)
        {
            randomColumnPick = GetRandomColumn();
        }
        else
        {
            randomColumnPick = bestMoves[random.Next(0, bestMoves.Count)];
        }
        PlayInColumn(ai, randomColumnPick);
        StartCoroutine(InstantiatePuck(randomColumnPick));
        CheckIfGameOver(ai);

    }

    private IEnumerator InstantiatePuck(int columnPicked)
    {
        yield return new WaitForSeconds(2f);
        GameObject puck = Instantiate(aiPuckPrefab, columnPositions[columnPicked]);
        puck.transform.localScale = new Vector3(1, 1, 1);
        puckList.Add(puck);
        yield return new WaitForSeconds(2f);
        overheadText.text = "Your Move";
        playerTurn = true;
    }

    private int GetMiniMaxScore(int depth, bool isMaximising)
    {
        if(depth <= 0)
        {
            return 0;
        }

        if (CheckIfWinFound(ai))
        {
            return depth;
        }

        if (CheckIfWinFound(player))
        {
            return -depth;
        }

        if (CheckIfFull())
        {
            return 0;
        }

        int bestValue = isMaximising ? -1 : 1;
        for (int i = 0; i < columnsLength; i++)
        {
            if (PlayInColumn(isMaximising ? 2 : 1, i))
            {
                int v = GetMiniMaxScore(depth - 1, !isMaximising);
                bestValue = isMaximising ? Mathf.Max(bestValue, v) : Mathf.Min(bestValue, v);
                RemoveTopPuck(i);
            }
        }

        return bestValue;

    }

    private bool RemoveTopPuck(int position)
    {
        int rowIterator;
        for (rowIterator = 0; rowIterator < rowsLength;)
        {
            if (game[position, rowIterator] == null)
            {
                rowIterator++;
                continue;
            }
            else
            {
                break;
            }
        }

        if(rowIterator != rowsLength)
        {
            game[position, rowIterator] = null;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool CheckIfWinFound(int whosTurn)
    {
        for(int j = 0; j < rowsLength-3; j++)
        {
            for(int i = 0; i < columnsLength; i++)
            {
                if(game[i,j] == whosTurn && game[i, j + 1] == whosTurn && game[i, j + 2] == whosTurn && game[i, j + 3] == whosTurn)
                {
                    return true;
                }
            }
        }

        for(int i = 0; i < columnsLength - 3; i++)
        {
            for(int j = 0; j < rowsLength; j++)
            {
                if (game[i, j] == whosTurn && game[i + 1, j] == whosTurn && game[i + 2, j] == whosTurn && game[i + 3, j] == whosTurn)
                {
                    return true;
                }
            }
        }

        for(int i = 3; i < columnsLength; i++)
        {
            for(int j = 0; j < rowsLength - 3; j++)
            {
                if (game[i, j] == whosTurn && game[i - 1, j + 1] == whosTurn && game[i - 2, j + 2] == whosTurn && game[i - 3, j + 3] == whosTurn)
                {
                    return true;
                }
            }
        }

        for(int i = 3; i < columnsLength; i++)
        {
            for(int j = 3; j < rowsLength; j++)
            {
                if (game[i, j] == whosTurn && game[i - 1, j - 1] == whosTurn && game[i - 2, j - 2] == whosTurn && game[i - 3, j - 3] == whosTurn)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool CheckIfFull()
    {
        for(int i = 0; i < columnsLength; i++)
        {
            for(int k = 0; k < rowsLength; k++)
            {
                if(game[i, k].HasValue)
                {
                    continue;
                }
                else
                {
                    return false;
                }
            }
        }
        return true;
    }

    private bool PlayInColumn(int typePuck, int position)
    {
        int rowIterator;
        for (rowIterator = 0; rowIterator < rowsLength;)
        {
            if(game[position, rowIterator] == null)
            {
                rowIterator++;
                continue;
            }
            else
            {
                break;
            }
        }

        if (rowIterator == 0)
        {
            columnsFull.Add(position);
            return false;
        }
        else
        {
            game[position, rowIterator - 1] = typePuck;
            return true;
        }
    }

    private int GetRandomColumn()
    {
        var range = Enumerable.Range(0, columnsLength).Where(i => columnsFull.Contains(i));

        var rand = new Random();
        int index = rand.Next(0, columnsLength - columnsFull.Count);
        return range.ElementAt(index);
    }

    


}
