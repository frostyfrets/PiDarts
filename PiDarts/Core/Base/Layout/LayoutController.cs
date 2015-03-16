using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PiDarts.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDarts.Core.Layout
{
    /// <summary>
    /// Controlls how entities are position on the screen when drawn.
    /// </summary>
    public static class LayoutController
    {

        //baseRegion represents the size of a score region on the screen.
        //it will be smaller based on the number of players 
        private static Region baseRegion;
        public static SpriteFont lastHitFont;
        private static Vector2[] regionPositions;
        public static SpriteFont[] spriteFontsByNumPlayers;

        //Screen dimensions in pixels
        private static float screenHeight;
        private static float screenWidth;
        private static float screenPadding;

        //Assets for games from 1-4 players
        private static string[] spriteFontAssets = {
        "score1p","score2p","score3p","score4p"};

        /// <summary>
        /// Loads all resource files from disk.
        /// </summary>
        public static void loadContent(ContentManager c)
        {
            spriteFontsByNumPlayers = new SpriteFont[spriteFontAssets.Length];
            for (int i = 0; i < spriteFontAssets.Length; i++)
            {
                spriteFontsByNumPlayers[i] = c.Load<SpriteFont>(String.Format("Content/Assets/Fonts/{0}", spriteFontAssets[i]));
            }
            lastHitFont = c.Load<SpriteFont>("Content/Assets/Fonts/lastHit");
        }

        /// <summary>
        /// Should be called if screen size ever changes
        /// </summary>
        public static void configureLayout(float _screenWidth, float _screenHeight, float _screenPadding)
        {
            screenWidth = _screenWidth;
            screenHeight = _screenHeight;
            screenPadding = _screenPadding;
        }


        /// <summary>
        /// Called on every frame to calculate size of new player score region.
        /// </summary>
        private static Region calculateBaseRegion(int _numRegions)
        {

            //TODO:Figure out a clean way to calculate the size of a region
            // if we want to calculate two rows (for a > 4 player game)
            Region newBase = new Region();

            newBase.width = LayoutController.screenWidth / _numRegions;
            newBase.height = LayoutController.screenHeight;

            return newBase;
        }

        /// <summary>
        /// Should be called if screen size ever changes
        /// </summary>
        private static Vector2[] calculateRegionPositions(int _numRegions)
        {
            Vector2[] newRegions = new Vector2[_numRegions];

            for (int i = 0; i < newRegions.Length; i++)
            {
                newRegions[i] = new Vector2(i * baseRegion.width, 0);
            }

            return newRegions;
        }

        /// <summary>
        /// Calculates positions of regions and updates them on every frame.
        /// </summary>
        public static void DrawScores(SpriteBatch _spriteBatch, IScoreEntity[] _scores)
        {

            baseRegion = calculateBaseRegion(_scores.Length);
            regionPositions = calculateRegionPositions(_scores.Length);

            for (int i = 0; i < _scores.Length; i++)
            {
                _scores[i].Draw(_spriteBatch, regionPositions[i], spriteFontsByNumPlayers[_scores.Length - 1]);
            }
        }

        /// <summary>
        /// Draws the location and human readable text for the last dart hit.
        /// i.e modifier = 2, value = 45, text drawn is "Double 15".
        /// </summary>
        public static void DrawLastHit(SpriteBatch _spriteBatch, Hit _lastHit)
        {
            if (_lastHit.modifier < 0)
            {
                return;
            }
            string modifier_string = "";

            switch (_lastHit.modifier)
            {
                case 1:
                    break;
                case 2:
                    modifier_string = "Double";
                    break;
                case 3:
                    modifier_string = "Triple";
                    break;
            }

            string score = String.Format("{0} {1}", modifier_string, _lastHit.value);
            _spriteBatch.DrawString(lastHitFont, score, new Vector2(screenWidth / 2, screenHeight / 2) - (lastHitFont.MeasureString(score) / 2), Color.Black);
        }
    }
}
