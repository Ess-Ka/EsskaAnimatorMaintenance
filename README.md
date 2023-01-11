**IMPORTANT**: V2.0: Installation method has changed! Please read the "Installation" section below.

# Esska Animator Maintenance

This software allows you to analyze and clean up Unity Animation Controllers.

![grafik](https://user-images.githubusercontent.com/84975839/169013588-56194afd-397c-49e0-8b1d-e6994166a8a6.png)

## How it works

The Animation Controller stores objects of following types in the same .controller file:
- AnimatorStateMachine
- AnimatorState
- AnimatorStateTransition
- AnimatorTransition
- BlendTree
- StateMachineBehaviour

For each of this objects, the script iterates through all layers, (sub) state machines and states in the Animation Controller. An object which cannot be found, will be recognized as unused and can be removed from Animation Controller.


## Installation

There is no .unitypackage file anymore. You can use the Unity Package Manager or VRChat Creator Companion to install the package.

If you have already installed a version before 2.0, you have to delete the Assets/Esska/AnimatorMaintenance folder in the Unity "Project" pane first.

### Unity Package Manager

Package Manager: Click on the "+" icon on left top and select "Add package from git URL...". Enter the Git-URL of this repository. When a new version is available, you will be notificated and you can update to the latest version in the Package Manager.

Alternatively, download and unzip the package somewhere on your harddisk or clone the repository. Using the "+" icon in the Package Manager, select ""Add package from disk..." and select the package.json in the folder you have unzipped/cloned before.

### VRChat Creator Companion

Download and unzip the package somewhere on your harddisk or clone the repository.

Creator Companion -> Settings -> User Packages: Add the folder you have unzipped/cloned before. On your project, allow "Local User Packages" in the dropdown field on top. Now, you see "Esska Animator Maintenance" in the list. You can add or remove it to any of your projects.

## Usage

Drag the prefab into your scene and assign an Animation Controller to it. Select one of the features described below.

### List Content By Type

Counts all objects by type and writes the results to the console.  

![grafik](https://user-images.githubusercontent.com/84975839/169016325-26eaaa89-7b1d-4237-a946-a3a05d878b8e.png)

### List Unused Content

Counts all unused objects and lists them separately in the console.

![grafik](https://user-images.githubusercontent.com/84975839/169016208-976ec3b5-91aa-47fd-b8ea-72629647ee96.png)

### Show/Hide All/Unused Content

Expands your Animation Controller in the project window. This allows you to inspect all or only unused content.

![grafik](https://user-images.githubusercontent.com/84975839/169015960-4f5e32dc-e8fe-4204-a328-82b349f48049.png)

### Remove Unused Content

Removes the unused objects of your Animation Controller and shows the results in the console. Make a BACKUP before you start this action!
