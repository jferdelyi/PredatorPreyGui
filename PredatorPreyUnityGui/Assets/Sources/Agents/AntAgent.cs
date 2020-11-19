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
using System.Collections.Generic;

namespace PredatorPrey {
    /// <summary>
    /// Ant implement insect agent
    /// </summary>
    public class AntAgent : InsectAgent {
        /// <summary>
        /// Setup agent
        /// </summary>
        public override void Setup() {
            _turnsSurvived = 0;
            _world = this.Environment.Memory["World"];
        }

        /// <summary>
        /// This is the method that is called once a turn. This is where the main logic of the agent should be placed. Once a message has been handled, it should
        /// be removed from the queue, using e.g. the Dequeue method.
        /// </summary>
        /// <param name="messages">The messages that the agent has received during the previous turn(s) and should respond to (not used)</param>
        public override void Act(Queue<Message> _) {
            AntAction(); // The exception is not "catch" so raised to the caller
        }

        /// <summary>
        /// Ant action
        /// </summary>
        private void AntAction() {
            /*
                • Move: 
                    For every time step, the ants randomly try to move up, down, left, or right. If the neighboring
                    cell in the selected direction is occupied or would move the ant off the grid, then the ant stays in the
                    current cell.
                • Breed: 
                    If an ant survives for three time steps, at the end of the time step (i.e., after moving) the ant will
                    breed. This is simulated by creating a new ant in an adjacent (up, down, left, or right) cell that is
                    empty. If there is no empty cell available, no breeding occurs. Once an offspring is produced, an ant
                    cannot produce an offspring again until it has survived three more time steps.
             */

            _turnsSurvived++;

            // Move
            TryToMove(); // implemented in base class InsectAgent

            // Breed
            if (_turnsSurvived >= 3) {
                if (TryToBreed()) { // implemented in base class InsectAgent
                    _turnsSurvived = 0;
                }
            }
        }

        /// <summary>
        /// Get insect type
        /// </summary>
        /// <returns>Ant</returns>
        public override InsectType GetInsectType() {
            return InsectType.Ant;
        }
    }
}