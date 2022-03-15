using ConsoleApp;
using AConfig;

// Getting the list of args from the command line
ArgsConfig config = new ArgsConfig(args);

//for setting the initialized parameter of the HTM
Experiment ex1 = new Experiment(config);
ex1.run();
