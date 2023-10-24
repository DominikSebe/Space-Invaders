using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    /// <summary>
    /// Holds information for characters that can be written in the console.
    /// </summary>
    internal class Character
    {
        #region Members
        private char key;
        private int x, y;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the position of the Character on the X axis
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less the zero.</exception>
        public int X
        {
            get { return x; }
            set 
            { 
                if (value < 0) throw new ArgumentOutOfRangeException("value", "X coordinate must be 0 ore more.");
                this.x = value; 
            }
        }
        /// <summary>
        /// Gets or sets the position of the Character on the X axis
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less the zero.</exception>
        public int Y
        {
            get { return y; }
            set 
            {
                if (value < 0) throw new ArgumentOutOfRangeException("value", "Y coordinate must be 0 ore more.");
                this.y = value; 
            }
        }
        /// <summary>
        /// Gets or sets the actual character value.
        /// </summary>
        public char Key
        {
            get { return this.key; }
            set { this.key = value; }
        }
        #endregion

        #region Methods
        #region Static
        /// <summary>
        /// Adjusts the position of a Character object.
        /// </summary>
        /// <param name="character">The character object to move.</param>
        /// <param name="x">The value by which to move the object on the X axis. Default is 0.</param>
        /// <param name="y">The value by which to move the object on the Y axis. Default is 0.</param>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to a Character.</exception>
        public static void Move(object character, int x = 0, int y = 0)
        {
            (character as Character).X += x;
            (character as Character).Y += y;
        }
        /// <summary>
        /// Sets the position of a Character object.
        /// </summary>
        /// <param name="character">The Character object to move.</param>
        /// <param name="x">The value to set the X coordinate to.</param>
        /// <param name="y">The value to set the Y coordinate to.</param>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to a Character.</exception>
        public static void MoveTo(object character, int x = 0, int y = 0)
        {
            (character as Character).X = x;
            (character as Character).Y = y;
        }
        /// <summary>
        /// Write a Character object to the console.
        /// </summary>
        /// <param name="character">The Character object, which specifies the character to write, and its position.</param>
        /// <param name="left">The value, by which to offset the position of the character on the X axis. Default is 0.</param>
        /// <param name="top">The value, by which to offset the position of the character on the Y axis. Default is 0.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the offset on either axis is less than zero.</exception>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to a Character.</exception>
        public static void Draw(object character, int left = 0, int top = 0)
        {
            if (left < 0) throw new ArgumentOutOfRangeException("left", "X offset must be 0 ore more.");
            if (top < 0) throw new ArgumentOutOfRangeException("top", "Y offset must be 0 ore more.");

            Console.SetCursorPosition(left + (character as Character).X, top + (character as Character).y);
            Console.Write((character as Character).key);
        }
        /// <summary>
        /// Write a Character object to the console with a specified color.
        /// </summary>
        /// <param name="character">The Character object, which specifies the character to write, and its position.</param>
        /// <param name="color">A ConsoleColor object, to specify the color the character to write with.</param>
        /// <param name="left">The value, by which to offset the position of the character on the X axis. Default is 0.</param>
        /// <param name="top">The value, by which to offset the position of the character on the Y axis. Default is 0.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the offset on either axis is less than zero.</exception>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to a Character.</exception>
        public static void Draw(object character, ConsoleColor color, int left = 0, int top = 0)
        {
            if (left < 0) throw new ArgumentOutOfRangeException("left", "X offset must be 0 ore more.");
            if (top < 0) throw new ArgumentOutOfRangeException("top", "Y offset must be 0 ore more.");

            ConsoleColor cosoleColor = Console.ForegroundColor;
            Draw(character, left, top);
            Console.ForegroundColor = cosoleColor;
        }
        /// <summary>
        /// Clear the space occupied by the written key of the Character object.
        /// </summary>
        /// <param name="character">The Character object, to key of which to clear.</param>
        /// <param name="left">The value, by which to offset the position of the character on the X axis. Default is 0.</param>
        /// <param name="top">The value, by which to offset the position of the character on the Y axis. Default is 0.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the offset on either axis is less than zero.</exception>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to a Character.</exception>
        public static void Cclear(object character, int left = 0, int top = 0)
        {
            if (left < 0) throw new ArgumentOutOfRangeException("left", "X offset must be 0 ore more.");
            if (top < 0) throw new ArgumentOutOfRangeException("top", "Y offset must be 0 ore more.");

            Console.SetCursorPosition(left + (character as Character).X, top + (character as Character).Y);
            Console.Write((character as Character).key);
        }
        #endregion


        #region Non-Static
        public void Draw(int left = 0, int top = 0) => Draw(this, left, top);
        public void Draw(ConsoleColor color, int left = 0, int top = 0) => Draw(this, color, left, top);
        public void Cclear(int left = 0, int top = 0) => Cclear(this, left, top);
        public void Move(int x = 0, int y = 0) => Move(this, x, y);
        public void MoveTo(int x = 0, int y = 0) => MoveTo(this, x, y);
        #endregion
        #endregion
    }
}
