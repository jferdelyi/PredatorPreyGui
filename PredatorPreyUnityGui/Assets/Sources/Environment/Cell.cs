using System;
using UnityEngine;

namespace PredatorPrey {
    /// <summary>
    /// Cell state
    /// </summary>
    public enum CellState { Empty, Ant, Doodlebug, Unknown };

    /// <summary>
    /// Cell object
    /// </summary>
    public class Cell {
        // Agent in this cell
        public InsectAgent AgentInCell { get; set; }

        // Cell index
        public Tuple<int, int> Index { get; set; }

        // Shared world
        protected World _world;

         // The game object
        public GameObject GameObject { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Cell(int x, int y, World world) {
            _world = world;
            AgentInCell = null;
            Index = new Tuple<int, int>(x, y);
        }

        /// <summary>
        /// Set cell state
        /// </summary>
        /// <param name="insect">Agent</param>
        public void SetState(InsectAgent insect) {
            AgentInCell = insect;
            _world.LoadGameObject(this);
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
