using System;
using FileHelpers;

namespace Esi_BusinessLayer.Parsing
{
    public class CSVParser
    {
        #region Global Variables
        #endregion

        #region Properties
        #endregion

        public object[] ReadWellGroupCSVFile(String filePath)
        {
            //parse with 2 record types for Well and Group
            var engine = new MultiRecordEngine(typeof(WellRecord), typeof(GroupRecord));
            engine.RecordSelector = new RecordTypeSelector(CustomSelector);

            //to return errors encountered while parsing
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            
            //Read
            object[] result = engine.ReadFile(filePath);
            
            if (engine.ErrorManager.HasErrors)
                engine.ErrorManager.SaveErrors("errors.out");

            return result;
        }

        private Type CustomSelector(MultiRecordEngine engine, string recordLine)
        {
            switch (recordLine.Substring(0, 5))
            {
                case "Well,":
                    return typeof(WellRecord);
                case "Group":
                    return typeof(GroupRecord);
                default:
                    throw new Exception();
            }
        }        
    }
}
