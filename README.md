# NeocortexApi-Project **Image Classification**
## Overview
Implementation of the classification appliction based on the neocortexapi hierarchical temporal memory (HTM) for computing correlation between sets of image after learning with HTM with Label(1 Image contain only one label).  
This project is a collected work of thesis **htm imgclassification** by Dasu Sai Durga Sundari and SoftwareEngineering(SE) project of the same name by Long Nguyen.  

![Sample output of the experiment after learning](Images\OutputExample.png "Sample output of the experiment after learning")  
The Images used was collected from [Fruit 360](https://github.com/Horea94/Fruit-Images-Dataset).  
## Experiment Procedure:
**1 Read the configuration and passed arguments**  
either by command line after  the build:
~~~
    ImageClassifier -if "InputFolder" -cf htmconfig1.json
~~~
or by changing the arguments input of Debug/General/Debug command line arguments to -cf htmconfig1.json -if "InputFolder"  
-cf add the option of the configuration file "htmconfig1.json"
-if add the option of the training Input Folder "InputFolder/". This folder contains folders of images, where the folder names also act as the label for the images inside it.  
**2 Convert The Images to binary array via binarization**  
[The Binarization Library](https://github.com/daenetCorporation/imagebinarizer) was developed as an open source project at [Daenet](https://daenet.de/de/).  
the current implementation uses a color threshold of 200 for every color in a 8bit-RGB scale.  
The images with the same label must be stored in folder. The folder name is the images' label.   
**3 Learning with Spatial Pooler(SP) to get to stable state**  
SP iterate through all the images as it learns.
The last set of Sparse Density Representations (SDRs), the output of Spatial Pooler(SP) for each binarized image were saved for correlation validation.  
**4 Validation of SP Learning for different set of images**  
There are 2 types of correlation which are defined as follow:
1. *Micro Correlation*: Maximum/Average/Minimum correlation in similar bit percent of all images' SDRs which respect to each other in the same label.  
2. *Macro Correlation*: Maximum/Average/Minimum correlation in similar bit percent of all images' SDRs with images from 2 different labels.   

**5 Showing the Output**  
The results of the two correlation are printed in the command prompt when executing the code  




