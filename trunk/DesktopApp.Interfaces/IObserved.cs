using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kanji.DesktopApp.Interfaces
{
    public abstract class IObserved
    {
        /// <summary>
        /// Gets or sets the list of observers.
        /// </summary>
        /// <value>The observers.</value>
        protected List<IObserver> Observers = new List<IObserver>();

        /// <summary>
        /// Registers the specified observer to this IObserved instance.
        /// </summary>
        /// <param name="observer">The observer.</param>
        public void RegisterObserver(IObserver observer)
        {
            if (!Observers.Contains(observer))
                Observers.Add(observer);
        }

        /// <summary>
        /// Deregisters the observer from this observed instance.
        /// </summary>
        /// <param name="observer">The observer.</param>
        /// <returns>True if the Observer was successfully removed 
        /// from the Observed instance.</returns>
        public bool DeregisterObserver(IObserver observer)
        {
            if (Observers.Contains(observer))
                return Observers.Remove(observer);
            else return false;
        }

        /// <summary>
        /// Updates the observers. In this method all the observers should
        /// be notified of the changes.
        /// </summary>
        public abstract void Update();
    }
}
