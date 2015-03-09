using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDarts.Core.Interfaces
{
    /// <summary>
    /// Allows scores to be drawn to screen.
    /// </summary>
    public interface IScoreEntity
    {
        void Draw(SpriteBatch _spriteBatch ,Vector2 _pos, SpriteFont _font);
    }
}
