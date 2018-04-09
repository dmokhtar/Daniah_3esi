using Esi_BusinessLayer.Abstraction;
using Esi_BusinessLayer.Parsing;
using FileHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;

namespace Esi_BusinessLayer.Rules
{
    public class UploadData
    {
        private ValidateRecord validateRecord;
        public UploadData()
        {
            validateRecord = new ValidateRecord();
        }

        public void Execute(String path)
        {
            CSVParser csvParser = new CSVParser();
            object[] result = csvParser.ReadWellGroupCSVFile(path);

            var objectRecordsList = ((IEnumerable)result).Cast<object>().ToList();
            List<IRecord> recordsList = objectRecordsList.Cast<IRecord>().ToList();
            validateRecord.Records = recordsList;

            validateRecord.ValidateCsv();

            //Set well types
            validateRecord.SetWellType();

            //Assign wells as children to groups
            Dictionary<IRecord, List<WellRecord>> groupsDictionary = validateRecord.SetGroupsChildren();
        }

        public void DisplayErrors()
        {
            ErrorInfo[] errors = null;
            if (File.Exists("errors.out"))
                errors = ErrorManager.LoadErrors("errors.out");
            
            // This will display error from line 2 of the fileHelper error file.
            if (errors != null)
            {
                Console.WriteLine();
                Console.WriteLine("Parsing Errors:");
                foreach (var err in errors)
                {
                    Console.WriteLine("Error on Line number: {0}", err.LineNumber);
                    Console.WriteLine("Record causing the problem: {0}", err.RecordString);
                    Console.WriteLine("Complete exception information: {0}", err.ExceptionInfo.ToString());
                    Console.WriteLine();
                }
            }

            if (validateRecord.FailedRecords != null && validateRecord.FailedRecords.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Business Rules Errors:");
                foreach (var err in validateRecord.FailedRecords)
                {
                    Console.WriteLine(err.Name);
                }
            }
        }

        public void DisplayGroupsAndWells()
        {
            if (validateRecord.WellsList != null && validateRecord.WellsList.Count > 0)
            {
                Console.WriteLine("Valid Wells List that do not belong to a group:");
                Console.WriteLine("Well, Name, Top Hole X, Top Hole Y, Bottom Hole X, Bottom Hole Y");
                DisplayWells(validateRecord.WellsList);
            }


            if (validateRecord.GroupsDictionary != null && validateRecord.GroupsDictionary.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Valid Groups List:");
                foreach (var groupDictionary in validateRecord.GroupsDictionary)
                {
                    GroupRecord groupRecord = (GroupRecord)groupDictionary.Key;
                    Console.WriteLine("Group, Name, Location X, Location Y, Radius");
                    StringBuilder groupStrBuilder = new StringBuilder();
                    groupStrBuilder.Append(groupRecord.RecordType + ",");
                    groupStrBuilder.Append(groupRecord.Name + ",");
                    groupStrBuilder.Append(groupRecord.LocationX + ",");
                    groupStrBuilder.Append(groupRecord.LocationY + ",");
                    groupStrBuilder.Append(groupRecord.Radius);

                    Console.WriteLine(groupStrBuilder.ToString());

                    if (groupDictionary.Value != null && groupDictionary.Value.Count > 0)
                    {
                        Console.WriteLine("Valid Wells List that belong to this group:");
                        Console.WriteLine("Well, Name, Top Hole X, Top Hole Y, Bottom Hole X, Bottom Hole Y");
                        DisplayWells(groupDictionary.Value);
                    }
                    Console.WriteLine();
                }
            }
        }

        public void DisplayWells(List<WellRecord> wellrecordsList)
        {
            if (wellrecordsList != null)
            {
                foreach (var well in wellrecordsList)
                {
                    StringBuilder wellStrBuilder = new StringBuilder();
                    wellStrBuilder.Append(well.RecordType + ",");
                    wellStrBuilder.Append(well.Name + ",");
                    wellStrBuilder.Append(well.TopX + ",");
                    wellStrBuilder.Append(well.TopY + ",");
                    wellStrBuilder.Append(well.BottomX + ",");
                    wellStrBuilder.Append(well.BottomY);

                    Console.WriteLine(wellStrBuilder.ToString());
                }
            }
        }
    }
}
