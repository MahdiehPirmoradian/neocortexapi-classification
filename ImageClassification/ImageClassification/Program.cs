using ConsoleApp;
using AConfig;


DateTime start = DateTime.Now;
// Getting the list of args from the command line
ArgsConfig config = new ArgsConfig(args);

//for setting the initialized parameter of the HTM
Experiment ex1 = new Experiment(config);
ex1.run();

DateTime end = DateTime.Now;
TimeSpan ts = (end - start);
Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Elapsed Time is {0} s", ts.TotalSeconds);
Console.ResetColor();
