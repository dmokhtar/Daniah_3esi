using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileHelpers;
using Esi_BusinessLayer.Abstraction;

namespace Esi_BusinessLayer
{
    public class CSVParser
    {        
        #region Global Variables
        List<IRecord> recordList = new List<IRecord>();        
        #endregion

        #region Properties
        #endregion
            
        public void ReadWellGroupCSVFile(String filePath)
        {            
            //parse
            var engine = new FileHelperAsyncEngine<EntityRecord>();
            engine.ErrorManager.ErrorMode = ErrorMode.SaveAndContinue;
            // Read
            using (engine.BeginReadFile(filePath))
            {
                // The engine is IEnumerable
                foreach (EntityRecord record in engine)
                {
                    //Type recordType = Well Group

                    switch (record.RecordType)
                    {
                        case "Well":
                            recordList.Add(new WellRecord(record.Name, record.IntegerValue1, record.IntegerValue2, record.IntegerValue3, record.IntegerValue4));
                            break;
                        case "Group":
                            recordList.Add(new GroupRecord(record.Name, record.IntegerValue1, record.IntegerValue2, record.IntegerValue3));
                            break;
                        default:
                            break;
                    }
                }
            }

            if (engine.ErrorManager.HasErrors)
                engine.ErrorManager.SaveErrors("errors.out");

            LoadErrors();
                       
        }

        private void LoadErrors()
        {
            // sometime later you can read it back using:
            ErrorInfo[] errors = ErrorManager.LoadErrors("errors.out");

            // This will display error from line 2 of the file.
            foreach (var err in errors)
            {
                Console.WriteLine();
                Console.WriteLine("Error on Line number: {0}", err.LineNumber);
                Console.WriteLine("Record causing the problem: {0}", err.RecordString);
                Console.WriteLine("Complete exception information: {0}", err.ExceptionInfo.ToString());
            }
        }
    }
}
