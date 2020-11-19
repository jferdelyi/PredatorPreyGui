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

using System;
using UnityEngine;

namespace PredatorPrey {

    /// <summary>
    /// The world
    /// </summary>
    public class World {
         // Environment
        private Environment _environment;
        // Cell map
        public Cell[,] Map { get; private set; }
        // Current ID used for agents creations
        private int _currentId;

        /// <summary>
        /// Load game object
        /// </summary>
        /// <param name="cell">Cell</param>
        public void LoadGameObject(Cell cell) {
            _environment.LoadGameObject(cell);
        }

        /// <summary>
        /// Contructor build the grid
        /// </summary>
        /// <param name="game">The game</param>
        public World(Environment environment) {
            _environment = environment;
            Map = new Cell[Settings.GridSize, Settings.GridSize];
            for (int i = 0; i < Settings.GridSize; i++) {
                for (int j = 0; j < Settings.GridSize; j++) {
                    Map[i, j] = new Cell(i, j, this);
                }
            }

            _currentId = 0;
        }

        /// <summary>
        /// Add agent
        /// </summary>
        /// <param name="a">Agent</param>
        /// <param name="line">Line</param>
        /// <param name="column">Column</param>
        public void AddAgentToMap(InsectAgent a, int line, int column) {
            a.CurrentCell = Map[line, column];
            a.CurrentCell.SetState(a);
        }

        /// <summary>
        /// Add agent
        /// </summary>
        /// <param name="a">Agent</param>
        /// <param name="vectorPosition">Position in the grid</param>
        public void AddAgentToMap(InsectAgent a, int vectorPosition) {
            int line = vectorPosition / Settings.GridSize;
            int column = vectorPosition % Settings.GridSize;
            AddAgentToMap(a, line, column);
        }

        /// <summary>
        /// Create agent name
        /// </summary>
        /// <param name="a">The agent</param>
        /// <returns>Name</returns>
        public string CreateName(InsectAgent a) {
            if (a.GetInsectType() == InsectType.Ant) {
                return $"a{_currentId++}";
            } else if (a.GetInsectType() == InsectType.Doodlebug) {
                return $"d{_currentId++}";
            }

            throw new Exception($"Unknown agent type: {a.GetType()}");
        }

        /// <summary>
        /// Count agents
        /// </summary>
        /// <param name="noDoodlebugs">Number of doodle bugs</param>
        /// <param name="noAnts">Number of ants</param>
        public void CountInsects(out int noDoodlebugs, out int noAnts) {
            noAnts = 0;
            noDoodlebugs = 0;

            for (int i = 0; i < Settings.GridSize; i++) {
                for (int j = 0; j < Settings.GridSize; j++) {
                    if (Map[i, j].GetState() == CellState.Doodlebug) {
                        noDoodlebugs++;
                    } else if (Map[i, j].GetState() == CellState.Ant) {
                        noAnts++;
                    }
                }
            }
        }

        /// <summary>
        /// Move agent at new location
        /// </summary>
        /// <param name="a">Agent</param>
        /// <param name="newLine">New line</param>
        /// <param name="newColumn">New Column</param>
        public void Move(InsectAgent a, int newLine, int newColumn) {
            a.CurrentCell.SetState(null);
            a.CurrentCell = Map[newLine, newColumn];
            a.CurrentCell.SetState(a);
        }

        /// <summary>
        /// Agent breed
        /// </summary>
        /// <param name="a">Agent</param>
        /// <param name="newLine">Line</param>
        /// <param name="newColumn">Column</param>
        /// <returns>New agent</returns>
        public InsectAgent Breed(InsectAgent a, int newLine, int newColumn) {
            InsectAgent offspring = null;

            if (a.GetInsectType() == InsectType.Ant) {
                offspring = new AntAgent();
            } else if (a.GetInsectType() == InsectType.Doodlebug) {
                offspring = new DoodlebugAgent();
            }

            string name = CreateName(offspring);
            offspring.Name = name;
            AddAgentToMap(offspring, newLine, newColumn);

            return offspring;
        }

        /// <summary>
        /// Eat
        /// </summary>
        /// <param name="da">Doodle agent</param>
        /// <param name="newLine">Line</param>
        /// <param name="newColumn">Column</param>
        public void Eat(DoodlebugAgent da, int newLine, int newColumn) {
            // Get ant
            AntAgent ant = (AntAgent)Map[newLine, newColumn].AgentInCell;
            // Delete game object
            Map[newLine, newColumn].SetState(null);
            // Remove agent
            _environment.Remove(ant);
            // Move doodlebug to the new location
            Move(da, newLine, newColumn);
        }

        /// <summary>
        /// Kill doodle agent
        /// </summary>
        /// <param name="da">Insect agent</param>
        public void Die(InsectAgent insect) {
            insect.CurrentCell.SetState(null);
        }

        /// <summary>
        /// Return true if the movement is valid
        /// </summary>
        /// <param name="a">Agent</param>
        /// <param name="direction">Direction</param>
        /// <param name="desiredState">Desired state</param>
        /// <param name="newLine">New line</param>
        /// <param name="newColumn">new Column</param>
        /// <returns>True if the movement is valid (and new line/column in out variables)</returns>
        public bool ValidMovement(InsectAgent a, Direction direction, CellState desiredState, out int newLine, out int newColumn) {
            int currentLine = a.CurrentCell.Index.Item1;
            int currentColumn = a.CurrentCell.Index.Item2;
            newLine = currentLine;
            newColumn = currentColumn;

            switch (direction) {
                case Direction.Up:
                    if (currentLine == 0) {
                        return false;
                    }

                    if (Map[currentLine - 1, currentColumn].GetState() != desiredState) {
                        return false;
                    }

                    newLine = currentLine - 1;
                    return true;

                case Direction.Down:
                    if (currentLine == Settings.GridSize - 1) {
                        return false;
                    }

                    if (Map[currentLine + 1, currentColumn].GetState() != desiredState) {
                        return false;
                    }

                    newLine = currentLine + 1;
                    return true;

                case Direction.Left:
                    if (currentColumn == 0) {
                        return false;
                    }

                    if (Map[currentLine, currentColumn - 1].GetState() != desiredState) {
                        return false;
                    }

                    newColumn = currentColumn - 1;
                    return true;

                case Direction.Right:
                    if (currentColumn == Settings.GridSize - 1) {
                        return false;
                    }

                    if (Map[currentLine, currentColumn + 1].GetState() != desiredState) {
                        return false;
                    }

                    newColumn = currentColumn + 1;
                    return true;

                default:
                    break;
            }

            throw new Exception("Invalid direction");
        }
    }
}