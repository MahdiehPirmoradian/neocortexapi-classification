using AConfig;
using NeoCortexApi.Entities;
using NeoCortexApi;
using NeoCortexApi.Network;
using NeoCortexApi.Utility;
using Daenet.ImageBinarizerLib;
using Daenet.ImageBinarizerLib.Entities;

namespace ConsoleApp
{
    /// <summary>
    /// Class Experiment contains the run() method
    /// </summary>
    internal class Experiment
    {
        readonly HtmConfig htmConfig;
        readonly ArgsConfig expConfig;

        /// <summary>
        /// Setiing the initialized parameters of the HTM 
        /// </summary>
        public Experiment( ArgsConfig config)
        {
            expConfig = config;
            htmConfig = config.htmConfig;
        }

        /// <summary>
        /// Method run() calls the Binarization method which is used as an Encoder for
        /// turning Input Images into arrays of 0 & 1.
        /// </summary>
        public void run()
        { 
            int height = htmConfig.InputDimensions[0];
            int width = htmConfig.InputDimensions[1];

            // By default it only returns subdirectories one level deep. 
            var directories = Directory.GetDirectories(expConfig.inputFolder).ToList();
            
            (   Dictionary<string, int[]> binaries, // List of Binarized images
                Dictionary<string, List<string>> inputsPath // Path of the list of images found in the given folder
            )   = imageBinarization(directories, width, height);

            // The key of the dictionary helps to keep track of which class the SDR belongs to

            //In the case of Black & White Pictures we use this line for training the Spatial Pooler
            (Dictionary<string, int[]> sdrs,var cortexLayer) = SPTrain(htmConfig, binaries);
            //In the case of colorful images we use this line for training the Spatial Pooler
            //(Dictionary<string, int[]> sdrs2, var cortexLayer2) = SPTrain(htmConfig, binaries, colorThreshold );

            HelpersTemp helperFunc = new HelpersTemp();

            Dictionary<string, double> listCorrelation = new();
            Dictionary<string, double> listInputCorrelation = new();
            foreach (KeyValuePair<string, List<string>> entry in inputsPath) // loop of the folder (classes) eg: Apple, banana, etc
            {
                var classLabel = entry.Key;
                var filePathList = entry.Value;
                var numberOfImages = filePathList.Count;

                for (int i = 0; i < numberOfImages; i++) // loop of the images inside the folder
                {
                    if (!sdrs.TryGetValue(filePathList[i], out var sdr1)) continue;
                    
                    foreach (KeyValuePair<string, List<string>> secondEntry in inputsPath) { // loop of the folder (again)
                        var classLabel2 = secondEntry.Key;
                        var filePathList2 = secondEntry.Value;
                        var numberOfImages2 = filePathList2.Count;
                        for (int j = 0; j < numberOfImages2; j++) // loop of the images inside the folder
                            {
                                if (!sdrs.TryGetValue(filePathList2[j], out var sdr2)) continue;
                                string fileNameofFirstImage = Path.GetFileNameWithoutExtension(filePathList[i]);
                                string fileNameOfSecondImage = Path.GetFileNameWithoutExtension(filePathList2[j]);
                                string temp = $"{classLabel + fileNameofFirstImage}__{classLabel2 + fileNameOfSecondImage}";
                                
                                //for Listing the Correlation of Output SDRs
                                listCorrelation.Add(temp, MathHelpers.CalcArraySimilarity(sdr1, sdr2));
                                //for Listing the Input Correlation
                                listInputCorrelation.Add(temp, MathHelpers.CalcArraySimilarity(binaries[filePathList[i]].IndexWhere((el) => el == 1), binaries[filePathList2[j]].IndexWhere((el) => el == 1)));
                        }
                    }
                }
            }

            var classes = inputsPath.Keys.ToList();



            //helperFunc.printSimilarityMatrix(listCorrelation, "micro", classes);
            //helperFunc.printSimilarityMatrix(listCorrelation, "macro", classes);
            
            helperFunc.printSimilarityMatrix(listCorrelation, "both", classes);



            //for getting the Input Correlation results out, for exp. between Hexagonh1__Hexagonh2  
            Console.WriteLine("INPUT Correlation Similarity_between_____HexagonH1__HexagonH2 : " + Math.Round(listInputCorrelation["Hexagonh1__Hexagonh2"],2));
            Console.WriteLine("INPUT Correlation Similarity_between_____HexagonH1__TriangleT1 : " + Math.Round(listInputCorrelation["Hexagonh1__TriangleT1"],2));

            //for getting the Output Correlation results out, for exp. between Hexagonh1__Hexagonh2 
            Console.WriteLine("Output Correlation Similarity_between_____HexagonH1__HexagonH2 : " + Math.Round(listCorrelation["Hexagonh1__Hexagonh2"],2));
            Console.WriteLine("Output Correlation Similarity_between_____HexagonH1__TriangleT1 : " + Math.Round(listCorrelation["Hexagonh1__TriangleT1"],2));

            //This Lines are for reading the TestFolder image which is saved By name B.jpg inside of TestFolder
            string MyProjectDir = DirProject();
            string TestFolder = MyProjectDir + "\\ImageClassification\\TestFolder\\B.jpg";
            //prediction code
            //Input Image encoding
            int[] encodedInputImage = ReadImageData(TestFolder, width,height);
            var temp1 = cortexLayer.Compute(encodedInputImage, false);

            // This is a general way to get the SpatialPooler result from the layer.
            var activeColumns = cortexLayer.GetResult("sp") as int[];

            var sdrOfInputImage = activeColumns.OrderBy(c => c).ToArray();



            Console.WriteLine("\n\nNow for the Test Image.......................");
            // calling the prediction function for Printing out Name of the category to which the
            // input Test Image has te mot smilarity
            string predictedLabel = PredictLabel(sdrOfInputImage, sdrs);
            Console.WriteLine($"The image is predicted as {predictedLabel}");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            #region
            /// <summary>
            /// Prediction Code Created By Group Metaverse
            /// for comparing the Test Image Similarity {Avg, Max & Min} to each category of Previous Trained Images
            /// </summary>
            /// <param name="sdrInputTestImage">SDR of the Input Test image that needs to be compared with previous trained set</param>
            /// <param name="sdrs">dictionary of SDRs of trained Input images<</param>
            /// <returns> predictedLableCat which is the name of Predicted Category Lable; </returns>
            string PredictLabel(int[] sdrInputTestImage, Dictionary<string, int[]> sdrs)
            {
                //This variable is used for comparing the similarity of Input Test Image with each trained input SDR
                int comparedSimilarity = 0;
                //maxSimilarity of the highest similar SDR in each Category with the Input Test Image
                int maxSimilarity = 0;
                //This variable is used for keeping the maximum predicted similarity overall
                int maxPredictedSimilarity = 0;
                //This variable is used for keeping the Lable of predicted Category
                string predictedLableCat = "";

                foreach (KeyValuePair<string, List<string>> secondEntry in inputsPath)
                {
                
                    List<int> list1 = new();
                    int totalsum = 0;
                    int totalnum = 0;
                    int avgSimilarity = 0;
                    int minSimilarity = 0;

                    // loop of each folder in input folder
                    var classLabel2 = secondEntry.Key;
                    var filePathList2 = secondEntry.Value;
                    var numberOfImages2 = filePathList2.Count;
                    // loop of each image in each category of inputs
                    for (int j = 0; j < numberOfImages2; j++) 
                    {
                        if (!sdrs.TryGetValue(filePathList2[j], out var sdr2)) continue;
                        string fileNameofTestImage = Path.GetFileNameWithoutExtension(TestFolder);
                        string fileNameOfSecondImage = Path.GetFileNameWithoutExtension(filePathList2[j]);
                        string temp = $"{"entered image" + fileNameofTestImage}__{classLabel2 + fileNameOfSecondImage}";


                        //calculating the similarity of the current itterated image with the input Test image
                        comparedSimilarity = (int)MathHelpers.CalcArraySimilarity(sdrInputTestImage, sdr2);
                        list1.Add(comparedSimilarity);
                        
                        
                            totalsum += comparedSimilarity;
                            totalnum = numberOfImages2;
                        
                        
                        //if the similarity of input image with the right now-itterated image is more than
                        //the similarity of the input Test image and last itterated image
                        if (comparedSimilarity > maxSimilarity)
                        {
                            maxSimilarity = comparedSimilarity;
                            predictedLableCat = secondEntry.Key;

                        }
                        minSimilarity = (int)list1.Min();

                    }
                    //calculating the Average similarity of the Input_Tested_Image with the current each category of Trained_Images
                    avgSimilarity = ((int)(totalsum / totalnum));
                    

                    if (avgSimilarity > maxPredictedSimilarity)
                    {
                        maxPredictedSimilarity = avgSimilarity;
                    }
                    
                    Console.WriteLine("\nSimilarity To Category  " + secondEntry.Key + "  " + avgSimilarity + "  ,Max Similarity: " + maxSimilarity + "  ,Min Similarity: " + minSimilarity);
                }

                Console.BackgroundColor = ConsoleColor.Cyan;
                Console.ForegroundColor = ConsoleColor.Black;
                //printing the highest similarity of the Input_Tested_Image with the previous Trained_Images category
                Console.WriteLine("\n  With Similarity  " + maxPredictedSimilarity + "  To category " + predictedLableCat );
                return predictedLableCat;

            }
            #endregion
        }


        private Tuple<Dictionary<string, int[]>, Dictionary<string, List<string>>> imageBinarization(List<string> directories, int width, int height)
        {
            Dictionary<string, List<string>> inputsPath = new Dictionary<string, List<string>>();
            Dictionary<string, int[]> binaries = new Dictionary<string, int[]>();

            foreach (var fullPath in directories)
            {
                string folderName = Path.GetFileName(fullPath);

                if (!inputsPath.ContainsKey(folderName))
                {
                    inputsPath[folderName] = new List<string>();
                }
                //for getting the address(filePathList) of all the images inside of that specific folder(fullPath)
                var filePathList = Directory.GetFiles(fullPath).Where(name => !name.EndsWith(".txt")).ToList();


                foreach (var filePath in filePathList)
                {

                    inputsPath[folderName].Add(filePath);

                    // Image binarization, inputVector means SDR
                    int[] inputVector = ReadImageData(filePath, height, width);
                    //inputVector was a 1 Dimension array we need to convert it to a 2 Dimension array to see and analyze our SDR more clear 
                    string[] savedVector = ConvertToString(inputVector, height, width);
                    // Write binarized data to a file
                    var baseDir = Path.GetDirectoryName(filePath);
                    var fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
                    var ext = "txt";
                    //Creating the text File
                    var fullFileName = $"{fileNameWithoutExt}.{ext}";
                    binaries.Add(filePath, inputVector);
                    System.IO.File.WriteAllLines(Path.Combine(baseDir, fullFileName), savedVector);
                }
            }
            return Tuple.Create(binaries, inputsPath);
        }

        private string[] ConvertToString(int[] inputVector, int height, int width)
        {
            string[] vs = new string[width];
            for (int j = 0; j < height; j++)
            {
                for (int i = 0; i < width; i++)
                {
                    vs[i] += inputVector[j * width + i].ToString()+',';
                }
            }
            return vs;
        }

        /// <summary>
        /// Returns Binarized Image in integer array
        /// </summary>
        /// <param name="imagePath">Name of Image to be binarized</param>
        /// <param name="height">Height of Binarized Image</param>
        /// <param name="width">Width of Binarized Image</param>
        /// <returns></returns>

        public int[] ReadImageData(string imagePath, int height, int width)
        {
            var parameters = new BinarizerParams
            {
                InputImagePath = imagePath,
                ImageHeight = height,
                ImageWidth = width,
                //for our dataset which are Black & Whites we do not use color Threshold
                //BlueThreshold = 200,
                //RedThreshold = 200,
                //GreenThreshold = 200
            };
            ImageBinarizer bizer = new ImageBinarizer(parameters);

            var doubleArray = bizer.GetArrayBinary();
            var hg = doubleArray.GetLength(1);
            var wd = doubleArray.GetLength(0);
            var intArray = new int[hg*wd];

            //we convert this binary array into an array of integers
            //because we use Hierarchichal Temporal Memory which use SDR and they are always Integers
            for (int j = 0; j < hg; j++)
            {
                for (int i = 0;i< wd;i++)
                {
                    intArray[j*wd+i] = (int)doubleArray[i,j,0];
                }
            } 
            return intArray;
        }
        /// <summary> Modified by Long Nguyen
        ///           Pulling out SDRs after HPC fires a STABLE event when training the SP with list of patterns
        /// </summary>
        /// <param name="cfg"></param> Spatial Pooler configuration by HtmConfig style
        /// <param name="inputValues"></param> Binary input vector (pattern) list
        private static (Dictionary<string, int[]>,CortexLayer<object, object> cortexLayer) SPTrain(HtmConfig cfg, Dictionary<string, int[]> inputValues)
        {
            // Creates the htm memory.
            var mem = new Connections(cfg);
            bool isInStableState = false;
            // HPC extends the default Spatial Pooler algorithm.
            // The purpose of HPC is to set the SP in the new-born stage at the begining of the learning process.
            // In this stage the boosting is very active, but the SP behaves instable. After this stage is over
            // (defined by the second argument) the HPC is controlling the learning process of the SP.
            // Once the SDR generated for every input gets stable, the HPC will fire event that notifies your code
            // that SP is stable now.
            HomeostaticPlasticityController hpa = new HomeostaticPlasticityController(mem, inputValues.Count * 40,
                (isStable, numPatterns, actColAvg, seenInputs) =>
                {
                    // Event should only be fired when entering the stable state.
                    // Ideal SP should never enter unstable state after stable state.
                    if (isStable == false)
                    {
                        Console.WriteLine($"INSTABLE STATE");
                        // This should usually not happen.
                        isInStableState = false;
                    }
                    else
                    {
                        Console.WriteLine($"STABLE STATE");
                        // Here you can perform any action if required.
                        isInStableState = true;
                    }
                });

            // It creates the instance of Spatial Pooler Multithreaded version.
            SpatialPooler sp = new SpatialPoolerMT(hpa);

            // Initializes the Spatial Pooler 
            sp.Init(mem, new DistributedMemory() { ColumnDictionary = new InMemoryDistributedDictionary<int, NeoCortexApi.Entities.Column>(1) });

             //mem.TraceProximalDendritePotential(true);

            // It creates the instance of the neo-cortex layer.
            // Algorithm will be performed inside of that layer.
            CortexLayer<object, object> cortexLayer = new CortexLayer<object, object>("L1");

            // Add encoder as the very first module. This model is connected to the sensory input cells
            // that receive the input. Encoder will receive the input and forward the encoded signal
            // to the next module.
            //cortexLayer.HtmModules.Add("encoder", encoder);

            // The next module in the layer is Spatial Pooler. This module will receive the output of the
            // encoder.
            cortexLayer.HtmModules.Add("sp", sp);

            // Learning process will take 1000 iterations (cycles)
            int maxSPLearningCycles = 1;

            // Save the result SDR into a list of array
            Dictionary<string, int[]> outputValues = new Dictionary<string, int[]>();

            for (int cycle = 0; cycle < maxSPLearningCycles; cycle++)
            {
                Console.WriteLine($"Cycle  ** {cycle} ** Stability: {isInStableState}");

                int iteration = 0;

                // Remove all elements in output SDR list
                outputValues.Clear();

                // This trains the layer on input pattern.

                foreach (var input in inputValues)
                {
                    iteration++;

                    // Learn the input pattern.
                    // Output lyrOut is the output of the last module in the layer.

                    var lyrOut = cortexLayer.Compute(input.Value, true) as ComputeCycle;

                    // This is a general way to get the SpatialPooler result from the layer.
                    var activeColumns = cortexLayer.GetResult("sp") as int[];

                    var actCols = activeColumns.OrderBy(c => c).ToArray();

                    outputValues[input.Key] = actCols;
                }
                if (isInStableState)
                    break;
            }
            return (outputValues,cortexLayer);
        }

        //This Line of code is used for returning the current path of the software which is used later for reading the Test Image
        public static string DirProject()
        {
            string DirDebug = System.IO.Directory.GetCurrentDirectory();
            string DirProject = DirDebug;

            for (int counter_slash = 0; counter_slash < 4; counter_slash++)
            {
                DirProject = DirProject[..DirProject.LastIndexOf(@"\")];
            }

            return DirProject;
        }
        

    }
}
