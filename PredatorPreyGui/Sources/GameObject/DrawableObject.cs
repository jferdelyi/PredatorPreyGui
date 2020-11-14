using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PredatorPreyGui {
    public class DrawableObject : IDrawable {
        // Texture
        public Texture2D Texture { get; set; }

        // Position
        public Vector2 Position { get; set; }

        // Game
        public TurnBasedGame Game { get; set; }

        // Texture name
        public string TextureName { get; set; }

        // Init
        protected bool _init;

        /// <summary>
        /// Implement Init
        /// </summary>
        public void Init() {
            if(!_init) {
                _init = true;
                LoadContent();
            }
        }

        /// <summary>
        /// Load content
        /// </summary>
        /// <param name="texture">Texture</param>
        public void LoadContent() {
            Texture = Game.LoadTexture(TextureName);
        }

        /// <summary>
        /// Draw object
        /// </summary>
        /// <param name="batch"></param>
        public void Draw(SpriteBatch batch) {
            if (Texture != null) {
                Vector2 drawPosition = Position;
                drawPosition.X -= Texture.Width / 2;
                drawPosition.Y -= Texture.Height / 2;
                batch.Draw(Texture, drawPosition, Color.White);
            }
        }
    }
}
