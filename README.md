# LAUNCH Camera Toolkit
This project is a toolkit full of tools that make working with Cameras much easier. This was built to help Holos improve their VR camera but can be utilized in non VR apps as well.

# Demos
## Interpolator
![image](https://user-images.githubusercontent.com/8294697/110353011-30ac0c80-7ffc-11eb-8ffd-bfa0238cd25e.png)

The interpolator is a component which allows you to change the position and rotation of a game object in a smoother manner. It's purpose is double, in that it can smooth out changes in a cameras position and rotation but also do the same for non camera objects as well. In the given example we use it to smooth out the position of a cube.

> [!IMPORTANT]
> To use the interpolator component you can't set the position and rotation through the transform directly. You have to set the `targetPosition` and `targetRotation` fields. You also cannot disable the whole script if you want to disable interpolation but still move the object. Use the `interpolateRotation` and the `interpolatePosition` fields to disable their respective interpolation.

![interp demo](https://user-images.githubusercontent.com/8294697/110353620-e24b3d80-7ffc-11eb-92e2-fbbab98c174c.gif)

## Multi Camera Demo
![image](https://user-images.githubusercontent.com/8294697/109998053-7a33e900-7cd6-11eb-93a5-833032df0511.png)

The multi camera tool is a tool which allows easy management of multiple cameras. The multi camera is split into two pieces parts, the `ManagedCamera` class and the `CameraManager` class. The managed camera is an easy interface to a camera and it's possible extra functionality (like an interpolator). The camera manager is what keeps track of these cameras, keeping track of the current active camera and allowing switching between other cameras easier. The camera manager have two camera switch functions which can be accessed in the component context menu:

![unity context menu](https://user-images.githubusercontent.com/8294697/109999131-82d8ef00-7cd7-11eb-895e-dc3768b234de.gif)

The view in the bottom left is a preview of what the currently active camera is seeing. You can get the current camera by accessing the singleton instance through `CameraManager.Instance`. If there is no singleton active currently the manager will create a singleton instance automatically when accessing the instance field.

## Focus
![image](https://user-images.githubusercontent.com/8294697/110350200-158bcd80-7ff9-11eb-90bd-d2a9561a1428.png)

The focus tool is a component which allows you to focus your camera onto any target. The focus component has a field called `target` which allows you to set the target in the editor or through code. The focus tool also has an option for an interpolator to be added. This allows you to have your camera smoothly focus on anything you may want, as well as allow you to move the camera smoothly.

![distance adjuster change](https://user-images.githubusercontent.com/8294697/110350851-c1cdb400-7ff9-11eb-9615-abd12254d458.gif)

## Distance Adjuster
![image](https://user-images.githubusercontent.com/8294697/110148633-ac128180-7da2-11eb-864f-0e58c1176b45.png)

The distance adjuster tool allows you to change your distance from a target. We've created a sample scene which allows you to change the target within the focus component. There are two buttons which allow you to move the camera forwards towards the target, or backwards away from the target.

Changing Distance:

![distance adjuster](https://user-images.githubusercontent.com/8294697/110149532-c731c100-7da3-11eb-8410-f9b69f76d83a.gif)

Changing Target:

![distance adjuster change](https://user-images.githubusercontent.com/8294697/110149567-d57fdd00-7da3-11eb-83cc-06d83ec8f07c.gif)
