using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Space_Invaders
{
    /// <summary>
    /// Container class for handling enemy Invade objects inside the game.
    /// </summary>
    internal class EnemyHandler
    {
        #region Members
        private List<Invader> invaders;
        private Timer timer;
        private readonly int[,] moves;
        private int moveIndex;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the array of Invaders of the object.
        /// </summary>
        public List<Invader> Invaders
        {
            get { return this.invaders; }
        }
        /// <summary>
        /// Gets or sets an Invader object specified by the index.
        /// </summary>
        /// <param name="index">The 0-based index of the Invader object.</param>
        /// <returns>The Invader object specified by the index</returns>
        /// <exception cref="IndexOutOfRangeException">Thrown when the index points outside of the array of Invaders.</exception>
        public Invader this[int index]
        {
            get { return invaders.ElementAt(index); }
            set { invaders[index] = value; }
        }
        /// <summary>
        /// Gets the timer used for updating the Bullet object.
        /// </summary>
        public Timer Timer
        {
            get { return this.timer; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new EnemyHandlers object.
        /// </summary>
        /// <param name="invaders">The Invaders that will be handeled by the object.</param>
        public EnemyHandler(List<Invader> invaders)
        {
            this.invaders = invaders;
            this.moveIndex = 0;
            moves = new int[8, 2]{
                { 1, 0 }, { 1, 0 }, { 1, 0 }, { 0, 1 }, { -1, 0 }, { -1, 0 }, { -1, 0 }, { 1, 0 }
            };
        }
        #endregion

        #region Destructor
        ~EnemyHandler()
        {
            timer.Dispose();
            invaders.ForEach(invader => invader.Dispose());
        }
        #endregion

        #region Methods
        #region Static
        /// <summary>
        /// Move the Invaders of an EnemyHandler object.
        /// </summary>
        /// <param name="enemyHandler">The EnemyHandler, the Invader objects of which to Move.</param>
        /// <exception cref="NullReferenceException">Thrown when the object can not be converted to an EnemyHandler.</exception>
        public static void Update(object enemyHandler)
        {
            (enemyHandler as EnemyHandler).Cclear();
            (enemyHandler as EnemyHandler).Move();
            (enemyHandler as EnemyHandler).Draw(ConsoleColor.Red);
        }
        #endregion

        #region Non-Static
        /// <summary>
        /// Move all Invaders handled by the object.
        /// </summary>
        public void Move()
        {
            invaders.ForEach(invader => invader.Move(this.moves[this.moveIndex % this.moves.GetLength(0), 0], this.moves[this.moveIndex % this.moves.GetLength(0), 1]));
            this.moveIndex++;
        }
        /// <summary>
        /// Draw all Invaders handled by the object.
        /// </summary>
        /// <param name="color">A ConsoleColor object to sepcify in which color to draw the Invdaers.</param>
        public void Draw(ConsoleColor color)
        {
            invaders.ForEach(invader => invader.Draw(color));
        }
        /// <summary>
        /// Clear all drawn Invader objects.
        /// </summary>
        public void Cclear()
        {
            invaders.ForEach(invader => invader.CClear());
        }
        /// <summary>
        /// Intialize a new <c>Threading.Timer</c> object for updating the object.
        /// </summary>
        /// <returns>The new Timer object.</returns>
        /// <exception cref="Exception">Thrown when the previous Tiemr object is still running.</exception>
        public Timer Start()
        {
            if (timer != null) throw new Exception("New timer can not be created before previous is stopped.");
            this.timer = new Timer(Update, this, 1000, 2000);
            return this.timer;
        }
        /// <summary>
        /// Disposes of the Timer that updates the object.
        /// </summary>
        public void Stop()
        {
            this.timer.Dispose();
            this.timer = null;
        }
        #endregion
        #endregion
    }
}
