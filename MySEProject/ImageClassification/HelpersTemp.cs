using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    internal class HelpersTemp
    {
        public Dictionary<string, Dictionary<string, double>> GetStatistics(Dictionary<string, double> correlationInfo, List<string> classes)
        {
            Dictionary<string, Dictionary<string, double>> statistics = new Dictionary<string, Dictionary<string, double>>();
            var filtered = correlationInfo.ToDictionary(p => p.Key, p => p.Value);
            foreach (string label1 in classes)
            {
                foreach (string label2 in classes)
                {
                    var dic = filtered.Where(p =>
                    {
                        var hKey = p.Key.Split("__");
                        if (hKey[0] == hKey[1])
                        {
                            return false;
                        }
                        if (hKey[0].Contains(label1) && hKey[1].Contains(label2))
                        {
                            return true;
                        }
                        return false;
                    });
                    var values = dic.ToDictionary(p => p.Key, p => p.Value).Values.ToList();
                    var tempStat = new Dictionary<string, double>();
                    tempStat["Max"] = values.Max();
                    tempStat["Avg"] = values.Average();
                    tempStat["Min"] = values.Min();
                    string tempKey = $"{label1}__{label2}";
                    statistics.Add(tempKey, tempStat);
                }
            }
            return statistics;
        }
        public Dictionary<string, Dictionary<string, double>> GetMicroStatistics(Dictionary<string, double> correlationInfo, List<string> classes)
        {
            Dictionary<string, Dictionary<string, double>> statistics = new Dictionary<string, Dictionary<string, double>>(); 
            Dictionary<string, double> filtered = new Dictionary<string,double>();
            var temp = correlationInfo.Where(p => SameKeyCheck(p.Key, classes));
            filtered = temp.ToDictionary(p => p.Key, p => p.Value);
            foreach(string label in classes)
            {
                var dic = filtered.Where(p => {
                    var hKey = p.Key.Split("__");
                    if (hKey[0] == hKey[1])
                    {
                        return false;
                    }
                    if(p.Key.Contains(label)) 
                    { 
                        return true;
                    }
                    return false;
                    });
                var values = dic.ToDictionary(p => p.Key, p => p.Value).Values.ToList();
                var tempStat = new Dictionary<string, double>();
                tempStat["Max"] = values.Max();
                tempStat["Avg"] = values.Average();
                tempStat["Min"] = values.Min();
                statistics.Add(label, tempStat);
            }
            return statistics;
        }
        public Dictionary<string, Dictionary<string, double>> GetMacroStatistics(Dictionary<string, double> correlationInfo, List<string> classes)
        {
            Dictionary<string, Dictionary<string, double>> statistics = new Dictionary<string, Dictionary<string, double>>();
            var temp = correlationInfo.Where(p => !SameKeyCheck(p.Key, classes));
            var filtered = temp.ToDictionary(p => p.Key, p => p.Value);
            foreach (string label1 in classes)
            {
                foreach(string label2 in classes.Where(p => p != label1))
                {
                    var dic = filtered.Where(p =>
                    {
                        var hKey = p.Key.Split("__");
                        if (hKey[0].Contains(label1) && hKey[1].Contains(label2))
                        {
                            return true;
                        }
                        return false;
                    });
                    var values = dic.ToDictionary(p => p.Key, p => p.Value).Values.ToList();
                    var tempStat = new Dictionary<string, double>();
                    tempStat["Max"] = values.Max();
                    tempStat["Avg"] = values.Average();
                    tempStat["Min"] = values.Min();
                    string tempKey = $"{label1}__{label2}";
                    statistics.Add(tempKey, tempStat);
                }
            }
            return statistics;
        }
        // inpuKey here are stored as Class1_imageName__Class2_imageName
        private bool SameKeyCheck(string inputKey, List<string> classes)
        {
            string[] key = inputKey.Split("__");
            string[] keyed = new string[2];
            for(int i = 0; i < key.Length; i += 1)
            {
                for(int j = 0; j < classes.Count; j += 1)
                {
                    if (key[i].Contains(classes[j]))
                    {
                        keyed[i] = classes[j];
                    }
                }
            }
            if(keyed[0] == keyed[1])
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Print the Similarity representation of micro or macro correlation
        /// </summary>
        /// <param name="correlationInfo">correlation matrix dictionary with key as class1__class2</param>
        /// <param name="mode">either "micro";"macro" or "all"</param>
        /// <param name="classes">the classes that are being evaluated</param>
        public void printSimilarityMatrix(Dictionary<string,double> correlationInfo, string mode, List<string> classes)
        {
            List<ConsoleColor> colorOrder = new List<ConsoleColor>{ ConsoleColor.Green, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Red};
            if (mode == "micro")
            {
                var matrix = GetMicroStatistics(correlationInfo, classes);
                Console.WriteLine("Printing statistics for micro correlation: ");
                int colorIndex = 0; 
                // Printing the table of micro corr
                var lineLength = classes.Count*25+classes.Count+1;
                string dashLine = "";
                for(int i = 0; i < lineLength; i++)
                {
                    dashLine += "-";
                }
                Console.WriteLine(dashLine);
                foreach (var item in matrix)
                {
                    Console.ForegroundColor = colorOrder[colorIndex];
                    var classHeader = $"Micro corr {item.Key}:";
                    Console.Write(String.Format("|{0,-25}",classHeader));
                    colorIndex = (colorIndex + 1) % colorOrder.Count;
                }
                Console.Write("|\n");
                Console.ResetColor();
                Console.WriteLine(dashLine);
                foreach (var stat in matrix[classes[0]].Keys)
                {
                    colorIndex = 0;
                    foreach (var label in classes)
                    {
                        Console.ForegroundColor = colorOrder[colorIndex];
                        var s = $"{stat}: {matrix[label][stat]}";
                        Console.Write(string.Format("|  {0,-23}", s));
                        colorIndex = (colorIndex + 1) % colorOrder.Count;
                    }
                    Console.Write("|\n");
                }
                Console.ResetColor();
                Console.WriteLine(dashLine);
            }
            else if (mode == "macro")
            {
                var matrix = GetMacroStatistics(correlationInfo, classes);
                // variables for table printing
                string tempKey = $"{classes[0]}__{classes[1]}";
                int firstColLength = 20;
                int cellLength = 25;
                int lineLength = cellLength * classes.Count + firstColLength + 1 + 1+ classes.Count;
                string dashLine = "";
                for(int i = 0; i < lineLength; i += 1)
                {
                    dashLine += "-";
                }

                Console.WriteLine("Printing statistics for macro correlation: ");
                Console.WriteLine(dashLine);
                
                // Printing first headings
                string heading = string.Format("|{0,-20}", "class");
                foreach(var classLabel in classes)
                {
                    heading += string.Format("|{0,-25}", classLabel);
                }
                heading += "|";
                Console.WriteLine(heading);

                // Printing the table
                List<string> corrTable = new List<string>();
                int tableIndex = 0;

                

                // Printing the first classes collumn
                for (int i = 0;i<classes.Count;i+=1)
                {
                    foreach (var statValue in matrix[tempKey])
                    {
                        if(tableIndex == (int)((double)matrix[tempKey].Count*((double)i+0.5)))
                        {
                            corrTable.Add(string.Format("|{0,-20}", classes[i]));
                        }
                        else
                        {
                            corrTable.Add(string.Format("|{0,-20}", ""));
                        }
                        tableIndex+=1;
                    } 
                }
                
                foreach(string classLabel1 in classes)
                {
                    tableIndex = 0;
                    foreach(string classLabel2 in classes)
                    {
                        if (classLabel1!=classLabel2) {
                            foreach (var statValue in matrix[$"{ classLabel1}__{classLabel2 }"])
                            {

                                corrTable[tableIndex] += String.Format("|{0,-25}", $"{statValue.Key} {statValue.Value}");
                                tableIndex += 1;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < matrix[tempKey].Count; i += 1)
                            {
                                corrTable[tableIndex] += String.Format("|{0,-25}", "");
                                tableIndex += 1;
                            }
                        }
                    }
                }
                // Adding the last slash to the lines
                tableIndex = 0;
                for (int i = 0; i < classes.Count; i += 1)
                {
                    foreach (var statValue in matrix[tempKey])
                    {
                        corrTable[tableIndex] += "|";
                        tableIndex += 1;
                    }
                }

                // Print out the lines
                int lineIndex = 0;
                Console.WriteLine(dashLine);
                foreach (var lines in corrTable)
                {
                    if(lineIndex% matrix[tempKey].Count == 0)
                    {
                        Console.ForegroundColor = colorOrder[(lineIndex / 3)%colorOrder.Count];
                    }
                    Console.WriteLine(lines);
                    if ((lineIndex+1)%matrix[tempKey].Count==0) {
                        Console.WriteLine(dashLine);
                    }
                    lineIndex += 1;    
                }
                Console.ResetColor();
            }
            else if (mode == "both")
            {
                var matrix = GetStatistics(correlationInfo, classes);
                // variables for table printing
                string tempKey = $"{classes[0]}__{classes[1]}";
                int firstColLength = 20;
                int cellLength = 25;
                int lineLength = cellLength * classes.Count + firstColLength + 1 + 1 + classes.Count; // 1 for the closing of the table and 1 for the first col
                string dashLine = ""; // dash line is created the same length as lineLength
                for (int i = 0; i < lineLength; i += 1)
                {
                    dashLine += "-";
                }

                Console.WriteLine("Printing statistics for experiment in micro(diagonal line from top left to bottom right) and macro (rest): ");
                Console.WriteLine(dashLine);

                // Printing first headings
                string heading = string.Format("|{0,-20}", "class");
                foreach (var classLabel in classes)
                {
                    heading += string.Format("|{0,-25}", classLabel);
                }
                heading += "|";
                Console.WriteLine(heading);

                // Printing the table
                List<string> corrTable = new List<string>();
                int tableIndex = 0;



                // Printing the first classes collumn
                for (int i = 0; i < classes.Count; i += 1)
                {
                    foreach (var statValue in matrix[tempKey])
                    {
                        if (tableIndex == (int)((double)matrix[tempKey].Count * ((double)i + 0.5)))
                        {
                            corrTable.Add(string.Format("|{0,-20}", classes[i]));
                        }
                        else
                        {
                            corrTable.Add(string.Format("|{0,-20}", ""));
                        }
                        tableIndex += 1;
                    }
                }

                foreach (string classLabel1 in classes)
                {
                    tableIndex = 0;
                    foreach (string classLabel2 in classes)
                    {
                        if (classLabel1 != classLabel2)
                        {
                            foreach (var statValue in matrix[$"{ classLabel1}__{classLabel2 }"])
                            {

                                corrTable[tableIndex] += String.Format("|{0,-25}", $"{statValue.Key} {statValue.Value}");
                                tableIndex += 1;
                            }
                        }
                        else
                        {
                            foreach (var statValue in matrix[$"{ classLabel1}__{classLabel2 }"])
                            {

                                corrTable[tableIndex] += String.Format(">|{0,-25}<", $"{statValue.Key} {statValue.Value}");
                                tableIndex += 1;
                            }
                        }
                    }
                }
                // Adding the last closing to the lines
                tableIndex = 0;
                for (int i = 0; i < classes.Count; i += 1)
                {
                    foreach (var statValue in matrix[tempKey])
                    {
                        corrTable[tableIndex] += "|";
                        tableIndex += 1;
                    }
                }

                // Print out the lines
                int lineIndex = 0;
                Console.WriteLine(dashLine);
                foreach (var line in corrTable)
                {
                    if (lineIndex % matrix[tempKey].Count == 0)
                    {
                        Console.ForegroundColor = colorOrder[(lineIndex / 3) % colorOrder.Count];
                    }
                    foreach (char word in line)
                    {
                        if (word == '>')
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        }
                        else if (word == '<')
                        {
                            Console.ForegroundColor = colorOrder[(lineIndex / 3) % colorOrder.Count];
                        }
                        else
                        {
                            Console.Write(word);
                        }
                    }
                    Console.Write("\n");
                    
                    if ((lineIndex + 1) % matrix[tempKey].Count == 0)
                    {
                        Console.WriteLine(dashLine);
                    }
                    lineIndex += 1;
                }
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Mode Not Found");
            }
        }
    }
}
