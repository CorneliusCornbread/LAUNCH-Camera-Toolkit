# LAUNCH Camera Toolkit
This project is a toolkit full of tools that make working with Cameras much easier. This was built to help Holos improve their VR camera but can be utilized in non VR apps as well.

# Demos
## Interpolator
TODO

## Multi Camera Demo
![image](https://user-images.githubusercontent.com/8294697/109998053-7a33e900-7cd6-11eb-93a5-833032df0511.png)

The multi camera tool is a tool which allows easy management of multiple cameras. The multi camera is split into two pieces parts, the `ManagedCamera` class and the `CameraManager` class. The managed camera is an easy interface to a camera and it's possible extra functionality (like an interpolator). The camera manager is what keeps track of these cameras, keeping track of the current active camera and allowing switching between other cameras easier. The camera manager have two camera switch functions which can be accessed in the component context menu:

![unity context menu](https://user-images.githubusercontent.com/8294697/109999131-82d8ef00-7cd7-11eb-895e-dc3768b234de.gif)

The view in the bottom left is a preview of what the currently active camera is seeing. You can get the current camera by accessing the singleton instance through `CameraManager.Instance`. If there is no singleton active currently the manager will create a singleton instance automatically when accessing the instance field.

## Focus
TODO

## Distance Adjuster
IMAGE HERE

The distance adjuster tool allows you to change your distance from a target. We've created a sample scene which allows you to change the target within the focus component. There are two buttons which allow you to move the camera forwards towards the target, or backwards away from the target.

GIF DEMONSTRATION