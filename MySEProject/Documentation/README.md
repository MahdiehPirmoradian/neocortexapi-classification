
# NeocortexApi-Application: **(Analyze Image Classification )**

This work is a new approach to image classification based on open HTM version algorithm.
The primary goal was to investigate open-source implementation of the Hierarchical Temporal Memory in C#/.NET Core. By 
conducting more than 100 experiments for different HTM configurations, the behavior of system is observed and 
documented using diagrams to get a vivid overview of dependency of HTM to each of the modified parameters.
Afterwards, a set of parameters, which have shown the better results of HTM functionality are discussed and proposed.
Followed by, implementing a [prediction code](https://github.com/MahdiehPirmoradian/neocortexapi-classification/blob/167ab147593348c16c4813e33e2d91be72a928db/MySEProject/ImageClassification/Experiment.cs#L153)  in the project, which enables HTM system to anticipate the category of an 
entered test input image.

This project is the implementaiton of the command line interfaca for the image classification based on the Hierarchical Temporal Memory (HTM) implemented in the [necortexapi](https://github.com/ddobric/neocortexapi) repository.

## Input DataSet: **(Hand Drawn Shapes)**
The images DataSet for training process are chosen from the following link: [DataSetImages](https://www.kaggle.com/abdurrahumaannazeer/handdrawnshapes)


They are 60x60 pixles images of Black & White Hand Drawn Shapes. 
![Input1](https://user-images.githubusercontent.com/74245613/159688485-5d11ff47-3b50-4837-8810-042fdd5e1387.JPG)

## METHODS

 we focused on changing various learning parameters to find the best fit that shows image classification. Most important learning parameters are:

1) Potential Radius (PotentialRadius)
2) Local Area Density (localAreaDensity)
3) Number of Active Columns per Inhibition Area 
(NumOfActiveColumnsPerInhArea)
4) Global Inhibition (GlobalInhibition)

to demonstrate how these parameters influence the learning. 

## HTM Configuration  
HTM setting of the project can be inputted to the program by means of a .json file [htmconfig.json](https://github.com/MahdiehPirmoradian/neocortexapi-classification/blob/ba8334a45c8ae552217be5ca6b219f4b692a470f/MySEProject/ImageClassification/htmconfig.json). There you can see the above parameters and change them.


Multiple experiments can therefore be conducted via changes of parameters in the json file. 
For a reference on what each parameter does, please refer to []() on [neocortexapi](https://github.com/ddobric/neocortexapi)
	
	
..........................................................................................................................................................................................................................................................................................

**We seperate this Experiments to two seperate parts:**
1) [First Part](https://github.com/MahdiehPirmoradian/neocortexapi-classification/tree/ba8334a45c8ae552217be5ca6b219f4b692a470f/MySEProject/Experiments/Variable%20Local%20Area%20Density%20%26%20Potential%20Radious) we had Variable Experiments with Potential Radious and Local Area Densities. 
2) [Second Part](https://github.com/MahdiehPirmoradian/neocortexapi-classification/tree/ba8334a45c8ae552217be5ca6b219f4b692a470f/MySEProject/Experiments/Variables%20NumActiveColumnsPerInhArea%20and%20GlobalInhibiton)  had experiments by changing Global/Local Inhibition and NumofActiveColumnsPerInArea.







## Progress of the project
1) Two different types of Prediction Code is created and are working accurately. This prediction code first read the input test images which are in the [Test Folder](https://github.com/MahdiehPirmoradian/neocortexapi-classification/tree/ba8334a45c8ae552217be5ca6b219f4b692a470f/MySEProject/ImageClassification/TestFolder), then after binarizing them, the similarity of the SDR of input test image will be 
compared to SDRs of images with wich the system has been trained.
2) An excel database of output information of the system is created and diagrams for comparison with different Htm Configs was created.
3) We also added some comments to make the code more readable and easier to understand.
4) the input test-image path doesnt need to be written completely, but its adressed based on the folder in which the program is running.
5) We also worked on some warnings of the Ecperiment class and solved them.
6) based on our limited resources (Personal Laptop) we only used 12 pictures(3 categories each contained 4 input image) for training the system, finding the best HTM config parameters for our prpgram and making he diagrams. But later after setting the best parameters in the [htmconfig.json](https://github.com/MahdiehPirmoradian/neocortexapi-classification/blob/ba8334a45c8ae552217be5ca6b219f4b692a470f/MySEProject/ImageClassification/htmconfig.json) file, we ibcreased the dataset to 30 and run this time to check for the accuracy of [prediction](https://github.com/MahdiehPirmoradian/neocortexapi-classification/blob/167ab147593348c16c4813e33e2d91be72a928db/MySEProject/ImageClassification/Experiment.cs#L153) part. 

###  Prediction Part :

Below are link to the Prediction code which we wrote to predict to which category the Input test image belongs to.
https://github.com/MahdiehPirmoradian/neocortexapi-classification/blob/167ab147593348c16c4813e33e2d91be72a928db/MySEProject/ImageClassification/Experiment.cs#L153


## In-Progress
1) In the next step we are planning to run the application for a wider input data set for training(1000) on Cloud to discover the behavior of system.






## How to use the application?
### 1) Clone the repository from GitHub.

### 2) Prepare the program's directory:
 
Before you start you need to prepare images that are required for the training. Images must be copied in the following folder structure along with the application and the config json:  

 ![](Images/WorkingDirectory.png)
 
The imagesets are stored inside ["InputFolder"](https://github.com/MahdiehPirmoradian/neocortexapi-classification/tree/ba8334a45c8ae552217be5ca6b219f4b692a470f/MySEProject/ImageClassification/InputFolder). 

![InputInstructions1](https://user-images.githubusercontent.com/74245613/159239100-91f724a9-9e32-4403-b984-ee1dda58215a.JPG)




Please first time for running the program after cloning the project, copy this InputFolder and placed it in the shown path:

![InputInstructionss](https://user-images.githubusercontent.com/74245613/159239592-a614ea21-4746-4688-b157-a249fb0a4de9.JPG)











          
            


### 3) process of giving your desired test image to the program:


please give the desired Test Images  inside the TestFolder in the shown path:

![TestFolder](https://user-images.githubusercontent.com/74245613/160682907-aa64478d-c372-43eb-96b2-7c8fd87e08fa.JPG)




### Results of Prediction Part:
  
  
 
### The best output for LocalAreaDensity and PotentialRadius is shown here
 

![IN](https://user-images.githubusercontent.com/74245613/161414574-60b19803-7e25-4e1d-bdd8-dab2dcd5aa37.JPG)





### After Increasing the number of Input images



### Prediction For the above mentioned Test files




### Results
![pp1](https://user-images.githubusercontent.com/74245613/161414864-de1f7507-dd1b-4aa2-a3f3-dd1cfc167bc0.JPG)

![pp2](https://user-images.githubusercontent.com/74245613/161414869-363b234c-b1a4-4962-8709-c9538588f05b.JPG)







