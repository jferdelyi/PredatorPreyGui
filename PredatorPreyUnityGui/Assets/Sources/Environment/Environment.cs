﻿/**************************************************************************
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
using System.Linq;
using UnityEngine;

namespace PredatorPrey {
    /// <summary>
    /// World environment implementation of TurnBasedEnvironment
    /// </summary>
    public class Environment : TurnBasedEnvironment {
        // Behavior
        EnvironmentBehaviour _behavior;

        // Number of turns
        public int Turn { get; private set; }

        // Simulation per second
        public int SimulationPerSeconds { get; private set; }

        // Internal numbers
        public int NoAnts { get; private set; }
        public int NoDoodlebugs { get; private set; }
        public int NoEmpty { get; private set; }

        // Sum time
        private double _sumTime = 0.0;

        /// <summary>
        /// Constructor create shared world
        /// </summary>
        /// <param name="game">The game</param>
        public Environment(int simulationPerSeconds, EnvironmentBehaviour behavior) : base() {
            Turn = 0;
            _behavior = behavior;
            SimulationPerSeconds = simulationPerSeconds;
            World world = new World(this);
            InitAgents(world);
            Memory["World"] = world;
        }

        /// <summary>
        /// Load game object
        /// </summary>
        /// <param name="cell">Cell</param>
        public void LoadGameObject(Cell cell) {
            _behavior.LoadGameObject(cell);
        }

        /// <summary>
        /// Kill gameobject
        /// </summary>
        /// <param name="cell">Cell</param>
        public void Die(Cell cell) {
            _behavior.Die(cell);
        }

        /// <summary>
        /// Update simulation
        /// </summary>
        public void Update(double deltaTime) {
            _sumTime += deltaTime;

            if (_sumTime >= (1 / (double)SimulationPerSeconds)) {
                _sumTime %= (1 / (double)SimulationPerSeconds);
                RunTurn(Turn);
            }
            Turn++;
        }

        /// <summary>
        /// Init environment
        /// </summary>
        public void Init() {
            Memory["World"].Init();
        }

        /// <summary>
        /// Init agents in the world
        /// </summary>
        /// <param name="environment"></param>
        public void InitAgents(World world) {
            int noCells = Settings.GridSize * Settings.GridSize;
            int[] randVect = RandomPermutation(noCells);

            // Create agents Doodle bugs
            for (int i = 0; i < Settings.NoDoodlebugs; i++) {
                var a = new DoodlebugAgent();
                Add(a, world.CreateName(a)); // Unique name
                world.AddAgentToMap(a, randVect[i]);
            }

            // Create agents Ants
            for (int i = Settings.NoDoodlebugs; i < Settings.NoDoodlebugs + Settings.NoAnts; i++) {
                var a = new AntAgent();
                Add(a, world.CreateName(a));
                world.AddAgentToMap(a, randVect[i]);
            }
        }

        /// <summary>
        /// Get random position
        /// </summary>
        /// <param name="n">Number of permutation</param>
        /// <returns>Premuted</returns>
        private static int[] RandomPermutation(int n) {
            int[] numbers = new int[n];
            for (int i = 0; i < n; i++) {
                numbers[i] = i;
            }
            int[] randPerm = numbers.OrderBy(x => Settings.Rand.Next()).ToArray();
            return randPerm;
        }

        /// <summary>
        /// Perform additional processing after a turn of the the simulation has finished
        /// </summary>
        /// <param name="turn">Current turn</param>
        public override void TurnFinished(int turn) {
            Memory["World"].CountInsects(out int noDoodlebugs, out int noAnts);
            int noAgents = noAnts + noDoodlebugs;
            NoAnts = noAnts;
            NoDoodlebugs = noDoodlebugs;
            NoEmpty = (Settings.GridSize * Settings.GridSize) - noAgents;
        }

        /// <summary>
        /// Perform additional processing after the simulation has finished
        /// </summary>
        public override void SimulationFinished() {
        }
    }
}