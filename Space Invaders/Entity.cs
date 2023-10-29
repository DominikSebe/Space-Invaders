using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Space_Invaders
{
    /// <summary>
    /// Abstract class defining entities inside the game.
    /// Implements IDisposable.
    /// </summary>
    internal abstract class Entity : IDisposable
    {
        #region Members
        private bool _disposed;
        #endregion

        #region Properties

        public abstract Character[] Characters { get; protected set; }
        public abstract Character this[int index] { get; set; }
        public abstract int X { get; set; }
        public abstract int Y { get; set; }
        public abstract int Width { get; }
        public abstract int Height { get; }
        #endregion

        #region Destructor
        /// <summary>
        /// Destructor of the class, releases managed resources and calls `Dispose()`.
        /// </summary>
        ~Entity()
        {
            this.Characters = null;
            Dispose(false);
        }
        #endregion

        #region Methods
        #region Static
        /// <summary>
        /// Adjusts the position of an Entity object.
        /// </summary>
        /// <param name="entity">The Entity object to move.</param>
        /// <param name="x">The value by which to move the object on the X axis.</param>
        /// <param name="y">The value by which to move the object on the Y axis.</param>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to an Entity.</exception>
        public static void Move(object entity, int x = 0, int y = 0)
        {
            (entity as Entity).X += x;
            (entity as Entity).Y += y;
        }
        /// <summary>
        /// Sets the position coordinates of an Entity object.
        /// </summary>
        /// <param name="entity">The Entity object to move.</param>
        /// <param name="x">The value to set the X coordinate to.</param>
        /// <param name="y">The value to set the Y coordinate to.</param>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to an Entity.</exception>
        public static void MoveTo(object entity, int x = 0, int y = 0)
        {
            (entity as Entity).X = x;
            (entity as Entity).Y = y;
        }
        /// <summary>
        /// Draw the characters of the object.
        /// </summary>
        /// <param name="entity">The Entity object, the characters of which to draw.</param>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to an Entity.</exception>
        public static void Draw(object entity)
        {
            foreach (Character character in (entity as Entity).Characters)
            {
                character.Draw((entity as Entity).X, (entity as Entity).Y);
            }
        }
        /// <summary>
        /// Draw the characters of the object with a specified color.
        /// </summary>
        /// <param name="entity">The Entity object, the characters of which to draw.</param>
        /// <param name="color">A ConsoleColor object to specify the color, the characters to draw with.</param>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to an Entity.</exception>
        public static void Draw(object entity, ConsoleColor color)
        {
            foreach (Character character in (entity as Entity).Characters)
            {
                character.Draw(color, (entity as Entity).X, (entity as Entity).Y);
            }
        }
        /// <summary>
        /// Clear the space occupied by the drawn characters of the object.
        /// </summary>
        /// <param name="entity">The Entity object, the characters of which to clear.</param>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to an Entity.</exception>
        public static void CClear(object entity)
        {
            foreach (Character character in (entity as Entity).Characters)
            {
                character.Cclear((entity as Entity).X, (entity as Entity).Y);
            }
        }
        #endregion

        #region Non-Static
        #region Implemented
        protected virtual void Dispose(bool disposing)
        {
            if(!_disposed)
            {
                if (disposing)
                {
                    this.Characters = null;
                }

                this._disposed = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion


        #region Abstract
        public abstract void Move(int x = 0, int y = 0);
        public abstract void MoveTo(int x = 0, int y = 0);
        public abstract void Draw();
        public abstract void Draw(ConsoleColor color);
        public abstract void CClear();
        #endregion
        #endregion
        #endregion

        #region Functions
        #region Static
        private static bool Collides(object entity, Character character)
        {
            return ((entity as Entity).X <= character.X && character.X </ (entity as Entity).X + (entity as Entity).Width) &&
                (entity as Entity).Y <= character.Y && character.Y <= (entity as Entity).Y + (entity as Entity).Height)
        }
        private static bool Collides(object entity, Entity other)
        {
            return ((entity as Entity).X <= other.X && other.X <= (entity as Entity).X + (entity as Entity).Width ||
                other.X <= (entity as Entity).X && (entity as Entity).X <= other.X + other.Width) &&
                ((entity as Entity).Y <= other.Y && other.Y <= (entity as Entity).Y + (entity as Entity).Height ||
                other.Y <= (entity as Entity).Y && (entity as Entity).Y <= other.Y + other.Width);
        }
        /// <summary>
        /// Check wether an Entity object and another object occupy the same space.
        /// </summary>
        /// <param name="entity">The first Entity object used in comparsion.</param>
        /// <param name="other">The second Entity  or Character object used in comparsion.</param>
        /// <returns>True if the two object overlap, False otherwise.</returns>
        /// <exception cref="NullReferenceException">Thrown when the first object can not be converted to an Entity or when the second can not be conerted to either an Entity or a Character..</exception>
        public static bool Collides(object entity, object other) => (entity is Entity) ? Collides(entity, (entity as Entity)) : Collides(entity, (other as Character));
        #endregion

        #region Non-Static
        public abstract bool Collides(object other);
        #endregion
        #endregion
    }
}
