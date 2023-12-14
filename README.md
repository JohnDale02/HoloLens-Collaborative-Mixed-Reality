# HoloLens 2 Collaborative Mixed Reality - ECE 535/635
<p align="center">
  <img src="https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/9b9b6806-e3a8-4f49-9cff-7a0f0b4edd45" width="400" />
  <img src="https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/31e0c7b5-53a8-4412-9847-ed7746cceddf" width="400" /> 
  <img src="https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/19310b00-0e8f-4c6a-a84c-8f8067d1e86a" width="400" />
</p>

## Motivation:
Our motivation behind this project is rooted in the applicability of mixed reality scenarios to benefit productivity, entertainment, connectivity, and beyond. Mixed reality has untapped potential, and our team would love to contribute to helping push this technology forward. Our work will provide us with hands-on experience for collaborative mixed reality scenarios, and in turn, the research community with valuable datasets. We are excited to work with this technology and help the research community.

## Design goals:
- [x] Enable a collaborative MR environment with two HoloLens devices
- [x] Mimic typical collaborative scenarios for mixed reality in a local environment
- [ ] Design experiments over the collaborative mixed reality application to best collect a robust dataset

## Deliverables:
- [x] Research and implement system setup for collaborative MR
- [x] Identify the type of application for experiments
- [x] Design experiments for two subjects wearing headsets and interacting with a common scene
- [x] Collect and organize the data

## System Blocks:
<p align="center">
  <img src="https://github.com/JohnDale02/Collaborative-MR-535/assets/116762794/9d1b625f-50e4-4018-a7fc-395fbc6652c8"  width = 700 />
  <img src="https://github.com/JohnDale02/Collaborative-MR-535/assets/116762794/9907773c-a8c3-4ac6-bb16-7584f9078321"  width = 600 />
  <img src="https://github.com/JohnDale02/Collaborative-MR-535/assets/116762794/62c6d63b-eb64-45cb-b157-0378d53627c9"  width = 600 />
  <img src="https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/601b1ffe-949c-4794-bbe5-381bd662afe9" width = 600 />
</p>


## HW/SW Requirements:
- Windows Laptop with Universal Windows Platform support
    - Windows 10 [OS build 19045.3693]
- Setup of Unity Editor on a Windows laptop
    - Unity Editor [2020.3.48f1]
        Ensure the following packages are installed:
          - Azure Spatial Anchors SDK Core [2.12.0]
          - Azure Spatial Anchors SDK for Windows [2.12.0]
          - Mixed Reality OpenXR Plugin [1.8.1]
          - Mixed Reality Toolkit Examples [2.7.0]
          - Mixed Reality Toolkit Extensions [2.7.0]
          - Mixed Reality Toolkit Foundation [2.7.0]
          - Mixed Reality Toolkit Standard Assets [2.7.0]
          - Mixed Reality Toolkit Tools [2.7.0]
- Visual Studio with Required Components
    - Visual Studio [16.11.31]
    - ASP.NET Web Development Tools Version [4.7.2]
    - Universal Windows Platform Development Component
    - Desktop Development with C++ Component
    - Game Development with Unity Toolset
- Setup of Hololens with Developer mode for accessing Windows Device Portal
- Setup of Hololens with Research mode for collecting sensor output 

## Project Settings and Configuration:
While building the application, incorrect setup of the MRTK and OpenXR profiles, and Project settings was the main cause for non-functional features. The following changes in these settings should be known to the developer.
To find these settings in the Unity editor, click File → Build Settings → Player Settings
- OpenXR Settings and Profiles:
    - The following Feature Groups should be enabled in XR Plug-in Management → OpenXR:
        - Hand Tracking: Enables the tracking of hand movements and gestures, allowing for natural interaction within virtual environments.
        - Hand Interaction Poses: Provides predefined hand poses for common interactions, simplifying the development of hand-based controls.
        - Motion Controller Model: Integrates physical controller models into the virtual environment, enhancing the realism and interactivity of user inputs.
        - Ensure that “Runtime Debugger” is not selected in the features page. This debugger encountered memory access exceptions during runtime and resulted in the crashing of our application. 
- The following Interaction Profiles should be added:
    - Eye Gaze Interaction Profile: Facilitates eye tracking for interaction, allowing applications to respond to where the user is looking.
    - Hand Interaction Profile: Defines standard interactions for hand tracking, ensuring consistent and intuitive user experiences.
    - Microsoft Hand Interaction Profile: A specialized profile tailored for Microsoft devices, optimizing hand tracking and interactions in the context of Microsoft's ecosystem and hardware.
- Project Settings: 
    - The following Capabilities should be enabled in Player → Publishing Settings:
        - InternetClient: Allows the app to access the internet
        - InternetClientServer: Enables network communication capabilities
        - PrivateNetworkClientServer: Permits communication on private networks
        - WebCam: Needed for accessing the device's webcam, crucial for AR experiences
        - Microphone: Enables voice input and audio recording functionalities
        - Spatial Perception: Allows the app to understand and interact with the physical space around the user
        - GazeInput: Enables eye tracking, allowing users to interact with the app using their gaze
- MRTK Profiles:
    - Our application utilized an imported MixedRealityToolkit profile, along with some further configuration. The profile is from the Mixed Reality Toolkit Foundation, and is called “DefaultMixedRealityToolkitConfigurationProfile.” The following changes should be made for adapting this configuration profile to our use case. 
          - Input → Pointers → Is Eye Tracking Enabled (make sure this box is checked)
          - Spatial Awareness → OpenXR Spatial Mesh Observer → Display Settings → Display Option → Set to Occlusion (This prevents the spatial mesh from being constantly displayed)
  
These settings and features will enable and include the necessary systems in the application build. 
Developers must ensure the correct build and deployment steps of their application to ensure its compatibility with the Hololens 2 platform. 

## Application Build and Deployment:
  - Unity Editor: File --> Build Settings
      - File --> Build Settings
          - Select the Universal Windows Platform
              - ![image](https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/ace8c886-2da9-4af3-85ee-015c7829767e)
          - Copy the following settings for Hololens build
              - ![image](https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/5b15552f-39d6-48f3-80a7-d4a086a6e73f)
              - ![image](https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/2df00d08-dd9e-4552-9742-d7283446c041)
          - Select "Build" and a folder you would like to save the build to



  - Visual Studio:
      - Open Project .sln file
      - Right Click Solution in Solution Explorer --> Properties
          - ![image](https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/be0e0c12-0e8c-453c-ba63-daf6d6a590cd)
          - Ensure Configuration is setup for build platform: Configuration: Active | Platform: ARM64
          - ![image](https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/7ecdcc43-5b53-4217-ba36-309e3b0d5732)
          - In Debugging Tab, enter Hololens IP address
          - ![image](https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/712268ee-3f5a-4c61-84d8-38556988625f)
          - Apply Settings
      - Begin Deployment to Hololens with Green Arrow
          - ![image](https://github.com/JohnDale02/HoloLens-Collaborative-Mixed-Reality/assets/116762794/72db4aaf-894c-4fe2-82d0-f8ca2b16e166)
          - If deployment fails, make sure you have paired with the Hololense and you are on the same WiFi network.
          - If all else fails, retry, sometimes deployment takes 2-3 tries

## Custom C# Scripts:
- PhotonRoomWordPuzzle.cs:
    - Script built upon the PhotonRoom.cs file included from the MRTK.Tutorials.MultiUserCapabilities asset.
        - Receives x prefab elements for instantiation (15 for our application)
        - Receives x anchor locations for object spawn locations
        - Facilitates creation of shared virtual environment and objects
        - Handles networking events
        - Attached to the NetworkRoom gameObject
- CollectData.cs
    - Script for logging user interaction and object positions to a .txt file
        - Recieves print delay (in seconds)
        - Ensures input systems are configured for data collection
        - Timed data: collected every 100ms
            - Poster positions: recorded once the objects are initializes (takes ~2 seconds to initialize)
            - Head position: the global position of the users head in space (Provides data instantly)
            - Hand Joint data:  Position and rotation of each joint in the hand
            - Provides data instantly (given hands can be seen)
            - Eye gaze origin and direction : returns hitobject if eye gaze intersects a gameObject (Takes ~10 seconds to initialize)
        - Event data: collected for touch events (max 1 every 100ms)
            - Poster touchpoints: global position of index finger when it touches a poster (max 1 event per 100 ms)
            - Provides data instantly

  
## Team Member Responsibilities:
- **John Dale**: Setup, Unity development, Research, Software
- **Dani Kasti**: Experiment design, Research, Writing


## References
- [Shared Experiences in Mixed Reality](https://learn.microsoft.com/en-us/windows/mixed-reality/design/shared-experiences-in-mixed-reality)
- [Research Mode in Mixed Reality](https://learn.microsoft.com/en-us/windows/mixed-reality/develop/advanced-concepts/research-mode)
- [Mixed Reality Toolkit (MRTK)](https://learn.microsoft.com/en-us/windows/mixed-reality/mrtk-unity/mrtk2/?view=mrtkunity-2022-05)
- [YouTube Video 1](https://www.youtube.com/watch?v=mSSVcT2PpKk)
- [YouTube Video 2](https://www.youtube.com/watch?v=dOsYerpKloY&t=4s)
