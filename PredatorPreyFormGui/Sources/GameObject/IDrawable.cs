using Microsoft.Xna.Framework.Graphics;

namespace PredatorPreyGui {

    /// <summary>
    /// Everything drawable in the game should be an GameObject
    /// </summary>
    public interface IDrawable {

        /// <summary>
        /// Draw object
        /// </summary>
        /// <param name="batch"></param>
        public abstract void Draw(SpriteBatch batch);

        /// <summary>
        /// Init the drawable
        /// </summary>
        public abstract void Init();
    }
}
