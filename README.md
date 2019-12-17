# PSICO-tests

### Description

This is an application that seeks to help conduct psychological tests through games for children with attention problems. It seeks to determine the status and progress of each child, through a virtualization of traditional games, accompanied by a more fun component thanks to the immersive environment through VR and the use of the Escape Room video game style, as a motivator for children.

It is important to note that it is for children / young people from 13 to 17 years old

## How to run


### Prerequisites

- [Git Bash](https://git-scm.com/downloads)
- [Unity Hub](https://unity3d.com/es/get-unity/download)
- [Unity](https://unity3d.com/es) (this project was developed using Unity.2019.2.5f1)
- [Android](https://developer.android.com/studio)

### Instructions

#### If you have not Unity 2019.2.5f1 installed 

1. Install [Unity Hub](https://unity3d.com/es/get-unity/download)
2. Search the Unity 2019.2.5f1 version
![Search Unity](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/unityHubvers201925.PNG)
3. Select the build configurations with SDK and NDK
![Configs SDKNDK](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/installUnitySDKNDK.PNG)

#### If you have Unity 2019.2.5f1 installed 

1. Install Unity, you can pay or use the Personal version (https://store.unity.com/download?ref=personal)
2. Install the Android SDK, or  the Android IDE (which includes the SDK). In this URL you can do it https://developer.android.com/studio
3. Using the Android IDE we need install the tools, like ndk, adb, etc. For this, you need follow this pictues that explain the configuration and instalation:

![Android Configurations](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/0.PNG)
![Settings1](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/sett1.PNG)
![Settings2](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/sett2.PNG)
![EVWindow](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/finallsett.PNG)

_

Too see the routes of SDK, JDK and JRE

![Project structure](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/1.PNG)
_

![Routes](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/2.PNG)


### Create the Environment variables:
5. Found "Environment variables" with Windows search.
6. This will open the following window, in which you must to click in "Environment variables".

![EVWindow](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/search.PNG)

7. Create the variables like is showed in the next picture (the route can change with your system username,in my case is Vivian, but please verify this)

![Variables](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/variables.PNG)

8. Open Unity > Edit > Preferences > External tools, and insert the same route of the Environment variables:

![Routes](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/5.PNG)

9. Open your git bash console in the directory of your Unity Projects (see the route in the IDE)

![GitBash](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/gibash.PNG)

10. Clone the project using your Git bash console, in this way:
```
git clone  https://github.com/VivianGomez/BottlesShooter-
```
11. Whith the project BoottlesShooter downloaded, please click on File > Open Project, and open the folder of the project

### To run  the project using your Oculus Go device.
12. Conect the Oculus Go to your PC using the USB cable
13. Click on File > Build and Run, and verify that you have the following configuration

![BR](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/buildandRun.PNG)

**Note:** If you do not have able the Android Plattaform, you must dowload it from Unity page or the assistant of Unity show to you a download button in the same window of Build and Run.

14 Activate the VR configuration at the same window:
![VR](https://raw.githubusercontent.com/VivianGomez/BottlesShooter-/master/Tutorial/VR.PNG)

**Note**: If you need more explanation about the VR and Oculus configuration, use this [official guide](https://developer.oculus.com/documentation/unity/latest/concepts/book-unity-gsg/)

15. When you have the same configuration of the picture, you can click on Build and run, and wait that the app be executed on your conected device.



