using Esi_BusinessLayer.Abstraction;
using Esi_BusinessLayer.Parsing;
using FileHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Esi_BusinessLayer.Rules
{
    public class UploadData
    {
        public void Execute(String path)
        {
            CSVParser csvParser = new CSVParser();

            object[] result = csvParser.ReadWellGroupCSVFile(path);

            ValidateRecord validateRecord = new ValidateRecord();
            var objectRecordsList = ((IEnumerable)result).Cast<object>().ToList();
            List<IRecord> recordsList = objectRecordsList.Cast<IRecord>().ToList();
            validateRecord.Records = recordsList;

            //Remove groups wells with duplicate names
            validateRecord.ValidateNameUniqness();

            //Get Wells Groups 2 Lists
            List<GroupRecord> groupsList = new List<GroupRecord>();
            List<WellRecord> wellsList = new List<WellRecord>();
            validateRecord.GetGroupsWellsLists(validateRecord.Records, groupsList, wellsList);

            //Remove groups with duplicate locations
            groupsList = validateRecord.ValidateGroupLocationUniqness(groupsList);

            //Remove groups intersecting
            groupsList = validateRecord.RemoveGroupsIntersections(groupsList);

            //Remove wells with duplicate locations
            wellsList = validateRecord.ValidateWellLocationUniqness(wellsList);
            
            //Set well types
            validateRecord.SetWellType(wellsList);

            //Assign wells as children to groups
            Dictionary<IRecord, List<WellRecord>> groupsDictionary = validateRecord.SetGroupsChildren(groupsList, wellsList);

            
        }
        
        public void LoadDisplayErrors()
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
