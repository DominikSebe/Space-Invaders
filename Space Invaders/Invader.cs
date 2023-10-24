using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    /// <summary>
    /// Class that defines enemies in the game.
    /// </summary>
    internal class Invader : Entity
    {
        #region Members
        private Character[] characters;
        private int x, y;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or set the array of characters, that belong to the object.
        /// </summary>
        public override Character[] Characters 
        {
            get
            {
                return this.characters;
            }
            protected set
            {
                this.characters = value;
            }
        }
        /// <summary>
        /// Gets or sets a Character, that belongs to the object.
        /// </summary>
        /// <param name="index">The 0-based index of the element to get or set.</param>
        /// <returns>The Character object of the specified index.</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the specifiedi index points outside of the array of Characters of the object.</exception>
        public override Character this[int index] 
        {
            get
            {
                return characters[index];
            }
            set
            {
                characters[index] = value;
            }
        }
        /// <summary>
        /// Gets or sets the position of the object on the X axis.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than zero.</exception>
        public override int X 
        { 
            get
            {
                return this.X;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("value", "X coordinate must be 0 or more.");

                this.x = value;
            }
        }
        /// <summary>
        /// Gets or sets the position of the object on the Y axis.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the value is less than zero.</exception>
        public override int Y 
        { 
            get
            {
                return this.Y;
            }
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException("value", "Y coordinate must be 0 or more.");

                this.y = value;
            }
        }
        /// <summary>
        /// Gets the the width of the object based on the position of its Characters objects.
        /// </summary>
        public override int Width
        {
            get
            {
                int min = this.characters[0].X, max = this.characters[0].X;

                for (int i = 1; i < this.characters.Length; i++)
                {
                    if (this.characters[i].X < min) min = this.characters[i].X;
                    if (this.characters[i].X > max) min = this.characters[i].X;
                }

                return max - min;
            }
        }
        /// <summary>
        /// Gets the the height of the object based on the position of its Characters objects.
        /// </summary>
        public override int Height
        {
            get
            {
                int min = this.characters[0].Y, max = this.characters[0].Y;

                for (int i = 1; i < this.characters.Length; i++)
                {
                    if (this.characters[i].X < min) min = this.characters[i].Y;
                    if (this.characters[i].X > max) min = this.characters[i].Y;
                }

                return max - min;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new Invader object.
        /// </summary>
        /// <param name="x">The position of the object on the X axis.</param>
        /// <param name="y">The position of the object on the Y axis.</param>
        /// <param name="characters">The characters of</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the either coordinate is less than zero.</exception>
        public Invader(int x, int y, params Character[] characters)
        {
            this.X = x;
            this.Y = y;
            this.Characters = characters;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Adjusts the position of the object.
        /// </summary>
        /// <param name="x">The value by which to move the object on the X axis.</param>
        /// <param name="y">The valie by which to move the object on the Y axis.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown either coordinate would be less than zero.</exception>
        public override void Move(int x = 0, int y = 0) => Entity.Move(this, x, y);
        /// <summary>
        /// Sets the position of the object.
        /// </summary>
        /// <param name="x">The value to set the X coordinate to.</param>
        /// <param name="y">The value to set the X coordinate to.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when either coordinate is less than zero.</exception>
        public override void MoveTo(int x = 0, int y = 0) => Entity.MoveTo(this, x, y);
        /// <summary>
        /// Draw the characters of the object.
        /// </summary>
        public override void Draw() => Entity.Draw(this);
        /// <summary>
        /// Draw the characters of the object with a specified color.
        /// </summary>
        /// <param name="color">A ConsoleColor object to specify the color, the characters to write with.</param>
        public override void Draw(ConsoleColor color) => Entity.Draw(this, color);
        /// <summary>
        /// Clear the space occupied by the drawn characters of the object.
        /// </summary>
        public override void CClear() => Entity.CClear(this);
        #endregion

        #region Functions
        /// <summary>
        /// Check wether the object and another occupy the same space.
        /// </summary>
        /// <param name="other"></param>
        public override void Collides(object other) => Entity.Collides(this, other);
        #endregion
    }
}
