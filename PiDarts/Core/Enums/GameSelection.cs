using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PiDarts.Core.Enums
{
    /// <summary>
    /// Represents different games avaialable to be played.
    /// When you're using a string representation (such as in a menu
    /// </summary>
    public enum GameSelection
    {
        Game301
    }

    public static class GameSelectionUtilities {
        public static GameSelection convertPrettyNameToEnum(string prettyName) {
            switch (prettyName) { 
                case "301":
                    return GameSelection.Game301;
            
            }
            throw new Exception("Name does not match any known games.");
        }
    }
}
