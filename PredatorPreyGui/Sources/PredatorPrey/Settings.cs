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

namespace PredatorPrey {
    /// <summary>
    /// All general options
    /// </summary>
    public class Settings {
        // Grid size (the number of cells is GridSize * GridSize)
        public static int GridSize = 50;
        // Nuumber of ants
        public static int NoAnts = 1000;
        // Number of doodle
        public static int NoDoodlebugs = 20;
        // Global random generator
        public static Random Rand = new Random();
    }
}