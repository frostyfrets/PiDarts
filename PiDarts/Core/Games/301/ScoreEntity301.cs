using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PiDarts.Core.GameTypes;
using PiDarts.Core.Interfaces;
using PiDarts.Core.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDarts.Core.Entities
{
    /// <summary>
    /// Represents a users score.
    /// </summary>
    public class ScoreEntity301 : IScoreEntity
    {
        public int currentScore;
        public int lastValidScore;
        public int position;

        public ScoreEntity301(int _position)
        {
            // TODO: Complete member initialization
            this.position = _position;
        }

        /// <summary>
        /// Draws score to the screen.
        /// </summary>
        public void Draw(SpriteBatch _spriteBatch, Vector2 _pos, SpriteFont font)
        {
            string score = String.Format("Player {0}: {1}", position + 1, currentScore);
            _spriteBatch.DrawString(font, score, _pos, Color.Yellow);
        }
    }
}
