/**************************************************************************
 *                                                                        *
 *  Website:     https://github.com/florinleon/ActressMas                 *
 *  Description: Predator-prey simulation (ants and doodlebugs) using     *
 *               ActressMas framework                                     *
 *  Copyright:   (c) 2018, Florin Leon                                    *
 *                                                                        *
 *  This program is free software; you can redistribute it and/or modify  *
 *  it under the terms of the GNU General Public License as published by  *
 *  the Free Software Foundation. This program is distributed in the      *
 *  hope that it will be useful, but WITHOUT ANY WARRANTY; without even   *
 *  the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR   *
 *  PURPOSE. See the GNU General Public License for more details.         *
 *                                                                        *
 **************************************************************************/

using ActressMas;
using System;
using System.Collections.Generic;

namespace PredatorPrey {

    /// <summary>
    /// Direction of the agent
    /// </summary>
    public enum Direction { Up, Down, Left, Right };

    /// <summary>
    /// Insect type
    /// </summary>
    public enum InsectType { Ant, Doodlebug };

    /// <summary>
    /// Insect agent implementation of turn based agent
    /// </summary>
    public abstract class InsectAgent : TurnBasedAgent {
        // Number of turn survived
        protected int _turnsSurvived;
        // Shared world
        protected World _world;
        // Current cell
        public Cell CurrentCell;

        /// <summary>
        /// Get insect type
        /// </summary>
        /// <returns>Insect type</returns>
        public abstract InsectType GetInsectType();

        /// <summary>
        /// Try to move
        /// </summary>
        /// <returns>True if it's a valid action</returns>
        protected bool TryToMove() {
            Direction direction = (Direction)Settings.Rand.Next(4);

            if (_world.ValidMovement(this, direction, CellState.Empty, out int newLine, out int newColumn)) {
                _world.Move(this, newLine, newColumn);

                return true;
            } else {
                return false;
            }
        }

        /// <summary>
        /// Try to breed
        /// </summary>
        /// <returns>True if it's a valid action</returns>
        protected bool TryToBreed() {
            List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

            for (int i = 0; i < 4; i++) {
                if (_world.ValidMovement(this, (Direction)i, CellState.Empty, out Int32 newLine, out Int32 newColumn)) {
                    positions.Add(new Tuple<int, int>(newLine, newColumn));
                }
            }

            if (positions.Count == 0) {
                return false;
            }

            int r = Settings.Rand.Next(positions.Count);
            var newInsect = _world.Breed(this, positions[r].Item1, positions[r].Item2);
            Environment.Add(newInsect);

            return true;
        }
    }
}