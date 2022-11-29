 using Next_Level.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


namespace Next_Level.Classes
{
    class BinnaryFile : IFile
    {
        FileStream stream = null;
        BinaryFormatter binary = null;
        string path;
        public BinnaryFile(string path)
        {
            this.path = path;
        }
        public void Save<T>(T obj)
        {
            stream = new FileStream(path, FileMode.Create);
            binary = new BinaryFormatter();
            binary.Serialize(stream, obj);
            stream.Close();
        }
        public T Load<T>()
        {
            T obj;
            if (!File.Exists(path))
            {
                return default(T);
            }
            else
            {
                stream = new FileStream(path, FileMode.Open);
                binary = new BinaryFormatter();
                obj = (T)binary.Deserialize(stream);
                stream.Close();
                return obj;
            }
        }
    }
}
