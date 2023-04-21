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

Add the package to VRChat Creator Companion:
https://ess-ka.github.io/EsskaPackageListing

After the package was added, click on the "Project" tab in the Creator Companion and select "Manage Project" on your project. Then choose the package in the list and install it.

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
