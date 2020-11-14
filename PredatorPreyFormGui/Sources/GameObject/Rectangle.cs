using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PredatorPreyGui {

    /// <summary>
    /// Rectangle 2D
    /// </summary>
    class Rectangle : Texture2D {
        /// <summary>
        ///  Construcotr
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        /// <param name="heigth">Heigth</param>
        /// <param name="width">Width</param>
        /// <param name="color">Color</param>
        public Rectangle(GraphicsDevice graphics, int heigth, int width, Color color) : base(graphics, heigth, width) {
            Color[] data = new Color[heigth * width];
            for (int i = 0; i < data.Length; ++i) {
                data[i] = color;
            }
            SetData(data);
        }
    }
}
