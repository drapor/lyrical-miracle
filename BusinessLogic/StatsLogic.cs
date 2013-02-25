using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyricalMiracle.Data;

namespace LyricalMiracle.BusinessLogic
{
    public class StatsLogic
    {
        Exceptions exceptionInstance = new Exceptions();
        StatsData statsDataInstance = new StatsData();

        public void WriteStats(Guid IDUser, Enums.StatsType StatsType)
        {
            try
            {
                statsDataInstance.WriteStats(IDUser, StatsType);
            }
            catch (Exception ex)
            {
                exceptionInstance.WriteException(Guid.NewGuid(), ex, "StatsLogic", "IncrementLogin", DateTime.Now);
            }
        }
    }
}
