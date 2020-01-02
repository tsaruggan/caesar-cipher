using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using OfficeOpenXml;

namespace caesar_cipher
{
    class Testing
    {
        public static void test(String inputFilename, String outputFilename)
        {
            List<String> testMessagesList = loadTestMessages(inputFilename); //store cleaned messages from given text file (.txt)
            String[,] results = new String[testMessagesList.Count,3]; // create multidimensional string array to hold results

            Random random = new Random();
            Stopwatch stopwatch = new Stopwatch();

            Console.Clear();
            Console.WriteLine("Please wait until testing is complete...");

            for (int index  = 0; index < testMessagesList.Count; index++) //for each message 
            {
                String message = testMessagesList[index]; //store current message 
                String length = message.Length.ToString(); //store length of current message

                int key = random.Next(1, 27); //generate random key
                String encrypted = Program.encrypt(message, key); //encrypt current message using key

                stopwatch.Start(); //start stopwatch
                String hacked = Program.hack(encrypted); //hack encrypted message
                stopwatch.Stop(); //stop stopwatch

                String time = Math.Round(stopwatch.Elapsed.TotalSeconds, 2).ToString(); //store time in seconds
                stopwatch.Reset(); //reset timer

                String success = (message == hacked).ToString(); //check if hack was succesful and store boolean

                results[index, 0] = length;
                results[index, 1] = time;
                results[index, 2] = success;
            }
            exportToExcel(results, outputFilename); //export results to excel
            Console.WriteLine($"Testing is complete. Results saved as: {outputFilename}");
        }


        private static List<String> loadTestMessages(String filename)
        {
            List<String> messages = new List<String>(); //string list to hold each cleaned message
            try
            {
                //store text file from given directory relative to bin directory of project
                System.IO.StreamReader file = new System.IO.StreamReader(Path.Combine(Environment.CurrentDirectory, filename));
                String word;

                // loop for every word in .txt file
                while ((word = file.ReadLine()) != null)  //while still lines in text file, hold each line
                {
                    word = Regex.Replace(word, "[^a-zA-Z]", "").ToUpper();  // remove all non letter characters from string +  make upper case
                    messages.Add(word); //store word in string list
                }
            }
            catch (System.IO.FileNotFoundException) //catch if reading file directory does not work
            {
                Console.WriteLine("Error. Input file directory does not exist or is not the correct type (.txt).\n\nRestart");
            }
            return messages; //return string list of cleaned messages
        }

        private static void exportToExcel(String [,] results,  String filename)
        {
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Worksheet1"); //create worksheet
            FileInfo excelFile = new FileInfo(filename); 

            DataTable dataTable = new DataTable(); //create datatable
           
            //create excel table headers
            dataTable.Columns.Add("Length", typeof(int));
            dataTable.Columns.Add("Time to Hack (s)", typeof(double));
            dataTable.Columns.Add("Success", typeof(Boolean));

            for (int index = 0; index < results.GetLength(0); index++) //store data from test results in each row
            {
                dataTable.Rows.Add(results[index, 0], results[index, 1], results[index, 2]);
            }

            worksheet.Cells["A1"].LoadFromDataTable(dataTable, true); //add table to worksheet

            for (int index = 0; index < results.GetLength(0); index++)
            {
                //set fill type of first 3 cells in data rows to solid fill
                worksheet.Cells[$"A{index + 2}:C{index + 2}"].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                if (Convert.ToBoolean(results[index, 2])) //check if hack was successful
                {       
                    worksheet.Cells[$"A{index+2}:C{index+2}"].Style.Fill.BackgroundColor.SetColor(Color.Green); //if true fill green
                } else
                {
                    worksheet.Cells[$"A{index + 2}:C{index + 2}"].Style.Fill.BackgroundColor.SetColor(Color.Red); //if false fill red
                }
            }

            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns(); //autofit each column
            excel.SaveAs(excelFile); //save excel file with given filename
        }
    }
}
