using System;
using System.IO;
using Overwatch.Memory;
using Single;
using MultiTasking;


namespace Overwatch.Read
{
    public class OverwatchReader : Singleton<OverwatchReader>
    {
        public Action<OverwatchMemoryCell> OnDataLoaded { get; set; }


        public bool CanILoad(in int index)
        {
            string path = OverwatchData.Path(index);

            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Load(int index)
        {
            Action load = () =>
            {
                var cell = OverwatchData.Load(index);
                DataIsLoaded(cell);
            };

            ThreadTaskQueuer.AddTask(load);
        }


        public void DataIsLoaded(OverwatchMemoryCell cell)
        {
            OnDataLoaded?.Invoke(cell);
        }
    }
}