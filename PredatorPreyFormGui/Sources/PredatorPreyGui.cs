using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Forms.Controls;
using PredatorPrey;
using PredatorPreyFormGui;
using System;
using System.Collections.Generic;

namespace PredatorPreyGui {
    public class PredatorPreyGui : MonoGameControl {
        // Simulation environment 
        public PredatorPrey.Environment Environment { get; set; }

        // The form
        public PredatorPreyForm Form { get; set; }

        // List of textures
        private readonly Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// Initialization logic
        /// </summary>
        protected override void Initialize() {
            base.Initialize();

            // Turn based env data
            Environment = new PredatorPrey.Environment(this, 10); // Derived from ActressMas.TurnBasedEnvironment
            LoadContent();
        }

        /// <summary>
        /// Update logic
        /// </summary>
        /// <param name="gameTime">Game time</param>
        protected override void Update(GameTime gameTime) {
            Environment.Update(gameTime.ElapsedGameTime.TotalSeconds);
            Form.UpdateLabels();
            base.Update(gameTime);
        }

        /// <summary>
        /// Drawing
        /// </summary>
        /// <param name="gameTime">Game time</param>
        protected override void Draw() {
            base.Draw();

            Editor.spriteBatch.Begin();
            Environment.Draw(Editor.spriteBatch);
            Editor.spriteBatch.End();

        }

        /// <summary>
        /// Use Content to load game content
        /// </summary>
        protected void LoadContent() {
            _textures.Add(CellState.Ant.ToString(), new Rectangle(GraphicsDevice, 10, 10, Color.Green));
            _textures.Add(CellState.Doodlebug.ToString(), new Rectangle(GraphicsDevice, 10, 10, Color.Red));
            _textures.Add(CellState.Empty.ToString(), new Rectangle(GraphicsDevice, 10, 10, Color.Black));
            Environment.Init();
        }

        // Get texture
        public Texture2D LoadTexture(string textureName) {
            return _textures[textureName];
        }
    }
}
