using System;

namespace PredatorPreyGui {
    /// <summary>
    /// Predator prey GUI (using ActressMAS agent based framework: https://github.com/florinleon/ActressMas)
    /// </summary>
    public class PredatorPreyGui {
        /// <summary>
        /// Main
        /// </summary>
        [STAThread]
        static void Main() {
            // New game
            using var game = new TurnBasedGame();
            game.Run();
        }
    }
}