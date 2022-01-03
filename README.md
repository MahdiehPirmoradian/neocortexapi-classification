# NeocortexApi-Project **Image Classification**

This project is the implementaiton of the command line interfaca for the image classification based on the Hierarchical Temporal Memory (HTM) implemented in the ![necortexapi](ttps://github.com/ddobric/neocortexapi) repository.

This project is a collected work of thesis **htm imgclassification** by Dasu Sai Durga Sundari and SoftwareEngineering(SE) project of the same name by Long Nguyen at the Frankfurt University of Applied Sciences.  

## How to use the classifier?

### 1 Prepare images 
 
 Before you start you need to prepare images that are required for the training. Images must be copied in the following folder structure:
 
 c:\Temp\TrainingImages
 c:\Temp\TrainingImages\Item1
 c:\Temp\TrainingImages\Item1\Image1
 ...
 c:\Temp\TrainingImages\Item1\Image1N
 
 TODO
 
 ### 2 Start the application by passing required command line arguments
 ~~~
 ImageClassifier -if "InputFolder" -cf htmconfig.json
 ~~~
 
 **HTM Configuration**
 
 htmconfig.json
 
### 3 Wait on results

When started the application will load images and start the training process. The trainingprocess runs in following steps.

#### 1 Convert The Images to binary array via binarization**  
[The Binarization Library](https://github.com/daenetCorporation/imagebinarizer) was developed as an open source project at [Daenet](https://daenet.de/de/).  
the current implementation uses a color threshold of 200 for every color in a 8bit-RGB scale.  
The images with the same label must be stored in folder. The folder name is the images' label.   

#### 2 Learn spatial patterns stored in images with the Spatial Pooler(SP)
SP first iterates through all images until the stable state is entered.
SP iterate through all the images as it learns.

#### 3 Validation of SP Learning for different set of images
The last set of Sparse Density Representations (SDRs), the output of Spatial Pooler(SP) for each binarized image were saved for correlation validation.  
There are 2 types of correlation which are defined as follow:
1. *Micro Correlation*: Maximum/Average/Minimum correlation in similar bit percent of all images' SDRs which respect to each other in the same label.  
2. *Macro Correlation*: Maximum/Average/Minimum correlation in similar bit percent of all images' SDRs with images from 2 different labels.   
The results of the two correlation are printed in the command prompt when executing the code  

Result example:

![Sample output of the experiment after learning](https://github.com/ddobric/neocortexapi-classification/blob/main/Images/OutputExample.png)  
The Images used was collected from [Fruit 360](https://github.com/Horea94/Fruit-Images-Dataset).  
## How to run the application in Visual Studio

By changing the arguments input of Debug/General/Debug command line arguments to -cf htmconfig1.json -if "InputFolder"  
-cf add the option of the configuration file "htmconfig1.json"
-if add the option of the training Input Folder "InputFolder/". This folder contains folders of images, where the folder names also act as the label for the images inside it.  



