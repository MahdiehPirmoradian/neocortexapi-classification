
# NeocortexApi-Project **Image Classification**


The images DataSet for training process are chosen from the following link: [DataSetImages](https://www.kaggle.com/abdurrahumaannazeer/handdrawnshapes)


They are 60x60 pixles images Hand Drawn Shapes. 
![Input1](https://user-images.githubusercontent.com/74245613/159688485-5d11ff47-3b50-4837-8810-042fdd5e1387.JPG)




We looked over 100 diverse HTM network setups in total. the Goal was to get the best results of learning and prediction, while variying parameters of htmConfig file in a specific range. after implementaion of these enhancements, the HTM system should be able to learn the specifications of images per each category, so that when we enter an image as a test-image, the system is able to tell us how high is the possibility of belonging the test-image to each of the learned categories.


## Progress of the project
1) Two different types of Prediction Code is created and are working accurately. one is comparing the similarity of the input test-image to average similarity of the images of each category, while another one compares the input test-image to all images and choose the image with most similarity and returns its category as the predicted category.  
2) An excel database of output information of the system is created and diagrams for comparison with different Htm Configs was created.
3) One [user](https://github.com/MahdiehPirmoradian/neocortexapi-classification/tree/main/MySEProject/Experiments/Variable%20Local%20Area%20Density%20%26%20Potential%20Radious) has done Experiments for variable Potential Radiuos while changing the Local Area Density. and another [user](https://github.com/MahdiehPirmoradian/neocortexapi-classification/commits/Omid) has investigate the NumActiveColumnsPerInhArea and Local and Global Inhibition parameters.
4) We also added some comments to make the code more readable and easier to understand.
5) We also worked on some warnings of the Ecperiment class and solved them.
6) the input test-image path doesnt need to be written completely, but its adressed based on the folder in which the program is running.


## In-Progress
1) Working on docx, pdf, pptx and mp4.
2) Planing more experiments to discover the behavior of system, while changing the above mentioned htmConfig parameters in even wider ranges.



This project is the implementaiton of the command line interfaca for the image classification based on the Hierarchical Temporal Memory (HTM) implemented in the [necortexapi](https://github.com/ddobric/neocortexapi) repository.


## How to use the classifier?

### 1) Prepare the program's directory:
 
 Before you start you need to prepare images that are required for the training. Images must be copied in the following folder structure along with the application and the config json:  

 ![](Images/WorkingDirectory.png)
 
The imagesets are stored inside "InputFolder".  
![InputInstructions1](https://user-images.githubusercontent.com/74245613/159239100-91f724a9-9e32-4403-b984-ee1dda58215a.JPG)




Please first time after cloning the project, copy this InputFolder and placed it in the shown path:

![InputInstructionss](https://user-images.githubusercontent.com/74245613/159239592-a614ea21-4746-4688-b157-a249fb0a4de9.JPG)







###  Prediction Part :

Below are link to the Prediction code which we wrote to predict to which category the Input test image belongs to.
https://github.com/MahdiehPirmoradian/neocortexapi-classification/blob/d40b281accc17316ca89ceadb6193eab6e3b4224/MySEProject/ImageClassification/Experiment.cs#L127



          
            


### 2) process of giving your input test image to the program:


Test Image is inside the TestFolder in the shown path with the name B.jpg, You can replace it. Just *** name the test-image as "B.jpg" please.
![TestInstructionss](https://user-images.githubusercontent.com/74245613/159239791-3b76c677-4404-4b96-b679-7334050ec04a.JPG)





 

 Sample input folder of the project can be found [here](https://github.com/MahdiehPirmoradian/neocortexapi-classification/tree/main/ImageClassification/ImageClassification/InputFolder)  
  
 
 **HTM Configuration**  
 HTM setting of the project can be inputted to the program by means of a .json file [htmconfig.json](https://github.com/MahdiehPirmoradian/neocortexapi-classification/blob/main/ImageClassification/ImageClassification/htmconfig.json).  
 Multiple experiments can therefore be conducted via changes of parameters in the json file. 
 For a reference on what each parameter does, please refer to []() on [neocortexapi](https://github.com/ddobric/neocortexapi) 
 
### The best output for LocalAreaDensity and PotentialRadius is shown here
 
 ![image](https://github.com/MahdiehPirmoradian/neocortexapi-classification/blob/main/MySEProject/Experiments/Variable%20Local%20Area%20Density%20%26%20Potential%20Radious/Best%20Experiment%20Variable%20Local%20AreaDensity%26PotentialRadious.JPG)







### The best output for NumActiveColumnsPerInhArea PotentialRadius is shown here

![bedune khatte-local- NumActive-30-PotRad-20](https://user-images.githubusercontent.com/77645707/159194666-1ebc1f2b-0003-431b-a301-61494cec47b8.jpg)

#After Increasing the number of Input images
After Increasing the number of Input images we get some high values for Macro values of some categories which was not desired:

### Input Images:
![1](https://user-images.githubusercontent.com/74245613/160407655-1b017536-2181-47ad-b4ff-4c01e331b255.JPG)

### Results
![2](https://user-images.githubusercontent.com/74245613/160407669-4e3f8b9e-b6ae-44ac-934d-c20209694653.JPG)
![3](https://user-images.githubusercontent.com/74245613/160407679-fbd9a94e-2abe-4802-877a-636485c37eb3.JPG)



