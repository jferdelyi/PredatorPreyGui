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

using Microsoft.Xna.Framework.Graphics;
using PredatorPreyGui;
using System;

namespace PredatorPrey {

    /// <summary>
    /// The world
    /// </summary>
    public class World : PredatorPreyGui.IDrawable {
        // Cell map
        private readonly Cell[,] _map;
        // Current ID used for agents creations
        private int _currentId;

        /// <summary>
        /// Contructor build the grid
        /// </summary>
        /// <param name="game">The game</param>
        public World(PredatorPreyGui.PredatorPreyGui game) {
            _map = new Cell[Settings.GridSize, Settings.GridSize];
            for (int i = 0; i < Settings.GridSize; i++) {
                for (int j = 0; j < Settings.GridSize; j++) {
                    _map[i, j] = new Cell(i, j, 10, game);
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
            a.CurrentCell = _map[line, column];
            _map[line, column].SetState(a);
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
                    if (_map[i, j].GetState() == CellState.Doodlebug) {
                        noDoodlebugs++;
                    } else if (_map[i, j].GetState() == CellState.Ant) {
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
            // Moving the agent
            _map[newLine, newColumn].SetState(a.CurrentCell.AgentInCell);
            a.CurrentCell.SetState(null);
            a.CurrentCell = _map[newLine, newColumn];
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
        /// <returns>The eaten ant</returns>
        public AntAgent Eat(DoodlebugAgent da, int newLine, int newColumn) {
            AntAgent ant = (AntAgent)_map[newLine, newColumn].AgentInCell;
            Move(da, newLine, newColumn);
            return ant;
        }

        /// <summary>
        /// Kill doodle agent
        /// </summary>
        /// <param name="da">Doodle agent</param>
        public void Die(DoodlebugAgent da) {
            da.CurrentCell.SetState(null);
            da.CurrentCell = null;
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

                    if (_map[currentLine - 1, currentColumn].GetState() != desiredState) {
                        return false;
                    }

                    newLine = currentLine - 1;
                    return true;

                case Direction.Down:
                    if (currentLine == Settings.GridSize - 1) {
                        return false;
                    }

                    if (_map[currentLine + 1, currentColumn].GetState() != desiredState) {
                        return false;
                    }

                    newLine = currentLine + 1;
                    return true;

                case Direction.Left:
                    if (currentColumn == 0) {
                        return false;
                    }

                    if (_map[currentLine, currentColumn - 1].GetState() != desiredState) {
                        return false;
                    }

                    newColumn = currentColumn - 1;
                    return true;

                case Direction.Right:
                    if (currentColumn == Settings.GridSize - 1) {
                        return false;
                    }

                    if (_map[currentLine, currentColumn + 1].GetState() != desiredState) {
                        return false;
                    }

                    newColumn = currentColumn + 1;
                    return true;

                default:
                    break;
            }

            throw new Exception("Invalid direction");
        }

        /// <summary>
        /// Draw world
        /// </summary>
        /// <param name="batch">The batch</param>
        public void Draw(SpriteBatch batch) {
            for (int i = 0; i < Settings.GridSize; i++) {
                for (int j = 0; j < Settings.GridSize; j++) {
                    _map[i, j].Draw(batch);
                }
            }
        }

        /// <summary>
        /// Init environment
        /// </summary>
        public void Init() {
            for (int i = 0; i < Settings.GridSize; i++) {
                for (int j = 0; j < Settings.GridSize; j++) {
                    _map[i, j].Init();
                }
            }
        }
    }
}