using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tantric.Trash
{
    // Function delegate used for cleaning up "trash"
    public delegate void CleanHandler(ITrashable trash);

    /// <summary>
    /// Interface representing an object which should be cleaned up if it doesn't report in for too long.
    /// </summary>
    public interface ITrashable
    {
        CleanHandler Clean
        {
            get;
            set;
        }
        bool Chimed
        {
            get;
        }
    }
}
