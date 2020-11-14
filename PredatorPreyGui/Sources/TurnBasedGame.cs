using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PredatorPrey;
using System.Collections.Generic;

namespace PredatorPreyGui {
    public class TurnBasedGame : Game {
        // Graphic design manager
        private readonly GraphicsDeviceManager _graphics;
        // Sprite batch
        private SpriteBatch _spriteBatch;
        // Simulation environment 
        private Environment _environment;
        // Font
        private SpriteFont _font;
        // List of textures
        private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();

        /// <summary>
        /// Constructor
        /// </summary>
        public TurnBasedGame() {
            // Game data
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        /// <summary>
        /// Initialization logic
        /// </summary>
        protected override void Initialize() {
            // Turn based env data
            _environment = new Environment(this, 10); // Derived from ActressMas.TurnBasedEnvironment
            base.Initialize();
        }

        /// <summary>
        /// Use Content to load game content
        /// </summary>
        protected override void LoadContent() {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _font = Content.Load<SpriteFont>("Default");
            _textures.Add(CellState.Ant.ToString(), new Rectangle(GraphicsDevice, 10, 10, Color.Green));
            _textures.Add(CellState.Doodlebug.ToString(), new Rectangle(GraphicsDevice, 10, 10, Color.Red));
            _textures.Add(CellState.Empty.ToString(), new Rectangle(GraphicsDevice, 10, 10, Color.Black));
            _environment.Init();
        }

        // Get texture
        public Texture2D LoadTexture(string textureName) {
            return _textures[textureName];
        }

        /// <summary>
        /// Update logic
        /// </summary>
        /// <param name="gameTime">Game time</param>
        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) {
                _environment.SimulationFinished();
                Exit();
            }

            _environment.Update(gameTime.ElapsedGameTime.TotalSeconds);
            base.Update(gameTime);
        }

        /// <summary>
        /// Drawing
        /// </summary>
        /// <param name="gameTime">Game time</param>
        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();
            _spriteBatch.DrawString(_font, "Number of agents: " + _environment.NoAgents, new Vector2(550, 50), Color.Black);
            _spriteBatch.DrawString(_font, "Number of ants: " + _environment.NoAnts, new Vector2(550, 100), Color.Black);
            _spriteBatch.DrawString(_font, "Number of doodlebugs: " + _environment.NoDoodlebugs, new Vector2(550, 150), Color.Black);
            _spriteBatch.DrawString(_font, "Number of empty cells: " + _environment.NoEmpty, new Vector2(550, 200), Color.Black);
            _environment.Draw(_spriteBatch);
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
