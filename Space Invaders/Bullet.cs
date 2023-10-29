using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Space_Invaders
{
    /// <summary>
    /// Class that defines bullet objects in the game.
    /// </summary>
    internal class Bullet: Character
    {
        #region Delegates
        public delegate void UpdateEvent(object bullet);
        #endregion

        #region Members
        private Timer timer;
        private UpdateEvent updateFunction;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the character that represents the bullet
        /// </summary>
        public new char Key
        {
            get { return base.Key; }
        }
        /// <summary>
        /// Gets the timer used for updating the Bullet object.
        /// </summary>
        public Timer Timer 
        { 
            get { return timer; }
        }
        /// <summary>
        /// Gets or sents the function to invoke when updating the bullet.
        /// </summary>
        public UpdateEvent UpdateFunction
        {
            get { return this.updateFunction; }
            set { this.updateFunction = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Initilaizes a new a Bullet object.
        /// </summary>
        /// <param name="x">Starting position of the object on the X axis.</param>
        /// <param name="y">Starting position of the object on the Y axis.</param>
        /// <param name="update">The function(s) to invoke when updating the bullet</param>
        public Bullet(int x = 0, int y = 0, UpdateEvent update = null): base('|', x, y)
        {
            this.updateFunction = update;
        }
        #endregion

        #region Destructor
        ~Bullet() 
        {
            updateFunction = null;
            timer.Dispose();
        }
        #endregion

        #region Methods
        #region Static
        /// <summary>
        /// Invokes the Update functions of a Bullet object.
        /// </summary>
        /// <param name="bullet">The Bullet object, the update functions of which to invoke.</param>
        /// <exception cref="NullReferenceException">Thrown when the Bullet object has not Update functions.</exception>
        private static void Update(object bullet) => (bullet as Bullet).UpdateFunction.Invoke(bullet);
        #endregion

        #region Non-Static
        /// <summary>
        /// Invokes the Update functions of the object.
        /// </summary>
        /// <exception cref="NullReferenceException">Thrown when the object has not Update functions.</exception>
        public void Update() => Update(this);
        /// <summary>
        /// Intialize a new <c>Threading.Timer</c> object for updating the object.
        /// </summary>
        /// <returns>The new Timer object.</returns>
        /// <exception cref="Exception">Thrown when the previous Tiemr object is still running.</exception>
        public Timer Start()
        {
            if (timer != null) throw new Exception("New timer can not be created before previous is stopped.");
            this.timer = new Timer(Update, this, 100, 100);
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
