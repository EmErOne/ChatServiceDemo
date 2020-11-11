using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatService.Server.Data
{
    /// <summary>
    /// Dient zum 
    /// </summary>
    public class DataContext
    {
        internal IDataContext Context { get; }

        #region Instance
        private static DataContext instance = null;
        private static readonly object padlock = new object();
        public static DataContext Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new DataContext();
                        }
                    }
                }

                return instance;

            }
        }
        #endregion

        private DataContext()
        {
            Context = new MockDataContext();
        }
    }
}
