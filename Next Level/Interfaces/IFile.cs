using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Next_Level.Interfaces
{
    public interface IFile
    {
        void Save<T>(T obj);
        T Load<T>();
    }
}
