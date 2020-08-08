using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using MultiTasking;
using System.IO;
using Single;


public static class DataReader
{
    public static void SaveData(string path, byte[] data)
    {
        Action saveTask = () =>
        {
            File.WriteAllBytes(path, data);
        };

        ThreadTaskQueuer.AddTask(saveTask);
    }
}
