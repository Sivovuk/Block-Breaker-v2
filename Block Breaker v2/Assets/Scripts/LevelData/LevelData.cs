using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    [System.Serializable]
    public class GameData
    {
        public List<Board> boards;
    }

    [System.Serializable]
    public class Board
    {
        public List<Row> board;
        public List<int> allowedPowerUps;
    }
    
    [System.Serializable]
    public class Row
    {
        public List<int> row;
    }
}