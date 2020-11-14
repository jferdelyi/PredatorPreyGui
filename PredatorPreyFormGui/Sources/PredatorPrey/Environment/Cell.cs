
using Microsoft.Xna.Framework;
using PredatorPreyGui;
using System;

namespace PredatorPrey {
    /// <summary>
    /// Cell state
    /// </summary>
    public enum CellState { Empty, Ant, Doodlebug, Unknown };

    /// <summary>
    /// Cell object
    /// </summary>
    public class Cell : DrawableObject {
        // Agent in this cell
        public InsectAgent AgentInCell { get; set; }

        // Cell index
        public Tuple<int, int> Index { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cell(int x, int y, int cellSize, PredatorPreyGui.PredatorPreyGui game) {
            AgentInCell = null;
            Position = new Vector2((Single)((x * cellSize) + (cellSize / 2.0)), (Single)((y * cellSize) + (cellSize / 2.0)));
            Game = game;
            TextureName = CellState.Empty.ToString();
            Index = new Tuple<int, int>(x, y);
        }

        /// <summary>
        /// Set state
        /// </summary>
        /// <param name="agent">New agent</param>
        public void SetState(InsectAgent agent) {
            AgentInCell = agent;
            string agentName = GetState().ToString();
            if (agentName != TextureName) {
                TextureName = agentName;
                if (_init) {
                    LoadContent();
                }
            }
        }

        /// <summary>
        /// Get cell state
        /// </summary>
        /// <returns>Cell state</returns>
        public CellState GetState() {
            if (AgentInCell == null) {
                return CellState.Empty;
            }
            if (InsectType.Ant == AgentInCell.GetInsectType()) {
                return CellState.Ant;
            }
            if (InsectType.Doodlebug == AgentInCell.GetInsectType()) {
                return CellState.Doodlebug;
            }
            return CellState.Unknown;
        }
    }
}
