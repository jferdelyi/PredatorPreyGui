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
    /// Doodle implement insect agent
    /// </summary>
    public class DoodlebugAgent : InsectAgent {
        // Turn without food
        private int _lastEaten;

        /// <summary>
        /// Setup agent
        /// </summary>
        public override void Setup() {
            _turnsSurvived = 0;
            _lastEaten = 0;
            _world = this.Environment.Memory["World"];
        }
        /// <summary>
        /// This is the method that is called once a turn. This is where the main logic of the agent should be placed. Once a message has been handled, it should
        /// be removed from the queue, using e.g. the Dequeue method.
        /// </summary>
        /// <param name="messages">The messages that the agent has received during the previous turn(s) and should respond to (not used)</param>
        public override void Act(Queue<Message> _) {
            DoodlebugAction(); // The exception is not "catch" so raised to the caller
        }

        /// <summary>
        /// Doodle action
        /// </summary>
        private void DoodlebugAction() {
            /*
                • Move. 
                    For every time step, the doodlebug will move to an adjacent cell containing an ant and eat the
                    ant. If there are no ants in adjoining cells, the doodlebug moves according to the same rules as the
                    ant. Note that a doodlebug cannot eat other doodlebugs.
                • Breed. 
                    If a doodlebug survives for eight time steps, at the end of the time step it will spawn off a new
                    doodlebug in the same manner as the ant.
                • Starve. 
                    If a doodlebug has not eaten an ant within three time steps, at the end of the third time step it
                    will starve and die. The doodlebug should then be removed from the grid of cells.
             */

            _turnsSurvived++;
            _lastEaten++;

            // Eat
            bool success = TryToEat();
            if (success) {
                _lastEaten = 0;
            }

            // Move
            if (!success) {
                TryToMove(); // Implemented in base class InsectAgent
            }

            // Breed
            if (_turnsSurvived >= 8) {
                if (TryToBreed()) { // Implemented in base class InsectAgent
                    _turnsSurvived = 0;
                }
            }

            // Starve
            if (_lastEaten >= 3) {
                Die();
            }
        }

        /// <summary>
        /// Try to eat
        /// </summary>
        /// <returns>True is the action is valid</returns>
        private bool TryToEat() {
            List<Tuple<int, int>> positions = new List<Tuple<int, int>>();

            for (int i = 0; i < 4; i++) {
                if (_world.ValidMovement(this, (Direction)i, CellState.Ant, out int newLine, out int newColumn)) {
                    positions.Add(new Tuple<int, int>(newLine, newColumn));
                }
            }

            if (positions.Count == 0) {
                return false;
            }

            int r = Settings.Rand.Next(positions.Count);
            AntAgent ant = _world.Eat(this, positions[r].Item1, positions[r].Item2);
            Environment.Remove(ant);
            return true;
        }

        /// <summary>
        /// Kill doodle
        /// </summary>
        private void Die() {
            // Removing the doodlebug
            _world.Die(this);
            Stop();
        }

        /// <summary>
        /// Get insect type
        /// </summary>
        /// <returns>Doodlebug</returns>
        public override InsectType GetInsectType() {
            return InsectType.Doodlebug;
        }
    }
}