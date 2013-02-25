using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LyricalMiracle.Data;
using LyricalMiracle.Entities;

namespace LyricalMiracle.BusinessLogic
{
    public class MeaningsLogic
    {
        Exceptions exceptionInstance = new Exceptions();
        MeaningsData meaningsDataInstance = new MeaningsData();

        public List<Meanings> GetMeanings(Guid IDUser)
        {
            List<Meanings> meanings = new List<Meanings>();

            try
            {
                meanings = meaningsDataInstance.GetMeanings(IDUser);

            }
            catch (Exception ex)
            {
                exceptionInstance.WriteException(Guid.NewGuid(), ex, "MeaningsLogic", "GetMeanings", DateTime.Now);
            }

            return meanings;
        }
    }
}
