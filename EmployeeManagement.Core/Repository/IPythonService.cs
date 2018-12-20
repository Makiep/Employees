using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Core.Interfaces
{
    public interface IPythonService
    {
        List<int> GetDailyPredictionAsync(Dictionary<string, string> predictionParameters);
        List<string> GetAttributes();
    }
}
