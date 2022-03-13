
# NeocortexApi-Project **Image Classification**

Group Name = Metaverse
Team Members = [Mahdieh Pirmoradian](https://github.com/MahdiehPirmoradian/neocortexapi-classification/commits/Mahdieh) , [Omid Nikbakht](https://github.com/MahdiehPirmoradian/neocortexapi-classification/commits/Omid)

The images DataSet for training process are chosen from the following link: [DataSetImages](https://www.kaggle.com/abdurrahumaannazeer/handdrawnshapes)


They are 60x60 pixles images Hand Drawn Shapes. 

![image](https://user-images.githubusercontent.com/77645707/158065860-4f6ad693-8138-4a44-ac90-e8d2f2d7b42c.png)

We looked over 70 diverse HTM network setups in total. the Goal was to get the best results of learning and prediction, while variying parameters of htmConfig file in a specific range. after implementaion of these enhancements, the HTM system should be able to learn the specifications of images per each category, so that when we enter an image as a test-image, the system is able to tell us how high is the possibility of belonging the test-image to each of the learned categories.


## Progress of the project
1) Two different types of Prediction Code is created and are working accurately. one is comparing the similarity of the input test-image to average similarity of the images of each category, while another one compares the input test-image to all images and choose the image with most similarity and returns its category as the predicted category.  
2) An excel database of output information of the system is created and diagrams for comparison with different Htm Configs was created.
3) One [user](https://github.com/MahdiehPirmoradian/neocortexapi-classification/commits/Mahdieh) has done Experiments for variable Potential Radiuos while changing the Local Area Density. and another [user](https://github.com/MahdiehPirmoradian/neocortexapi-classification/commits/Omid) has investigate the NumActiveColumnsPerInhArea and Local and Global Inhibition parameters.
4) We also added some comments to make the code more readable and easier to understand.
5) We also worked on some warnings of the Ecperiment class and solved them.
6) the input test-image path doesnt need to be written completely, but its adressed based on the folder in which the program is running.


## In-Progress
1) Working on docx, pdf, pptx and mp4.
2) Planing more experiments to discover the behavior of system, while changing the above mentioned htmConfig parameters in even wider ranges.



This project is the implementaiton of the command line interfaca for the image classification based on the Hierarchical Temporal Memory (HTM) implemented in the [necortexapi](https://github.com/ddobric/neocortexapi) repository.


## How to use the classifier?

### 1 Prepare the program's directory:
 
 Before you start you need to prepare images that are required for the training. Images must be copied in the following folder structure along with the application and the config json:  

 ![](Images/WorkingDirectory.png)
 
The imagesets are stored inside "InputFolder/".  
![inputFolder image](https://user-images.githubusercontent.com/77645707/158067271-92a222f7-2513-47a7-960c-600174059ddf.jpg)


Each Imageset is stored inside a folder whose name is the set's label.  
![inputfolder sample with images](https://user-images.githubusercontent.com/77645707/158067340-11486572-420c-47bf-b1cb-3e09cd3eefa0.jpg)

for entering the test-image create a folder named "TestFolder" in the shown path, and put the test-image in it  : 
![testimage input folder](https://user-images.githubusercontent.com/77645707/158067461-8fb78505-0879-45af-9be0-98740ef1e84b.jpg)
*** name the test-image "B.jpg" please
![test image place](https://user-images.githubusercontent.com/77645707/158067596-e8af236a-d481-402d-bdeb-cb17cb23419e.jpg)



 

 Sample input folder of the project can be found [here](https://github.com/MahdiehPirmoradian/neocortexapi-classification/tree/main/ImageClassification/ImageClassification/InputFolder)  
  
 
 **HTM Configuration**  
 HTM setting of the project can be inputted to the program by means of a .json file [htmconfig.json](ImageClassification/ImageClassification/htmconfig.json).  
 Multiple experiments can therefore be conducted via changes of parameters in the json file. 
 For a reference on what each parameter does, please refer to []() on [neocortexapi](https://github.com/ddobric/neocortexapi) 
 
