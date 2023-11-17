# Work in progress

# Experimental Project Setup

## Dependencies
- Visual Studio 2019
- .NET Environment

Using the MixedRealityFeatureTool.exe our installed packages are listed below:
  - | Version 2.7
  - | Version 2.7
  - | Version 2.7

## Project & MRTK Settings   
  - OpenXR
  - Interaction Profiles:

## Collaborative Networking (Photon 2)
- Photon 2
- Make sure to drag Photon view script into GameObject
      - Ownership must be set to 'takeover' for collaborative users to interact with the same objects

## Connecting Hololens For Deployment
- Get IP address by viewing network adpater properties in Wifi settings
- Go to developer settings and pair the HoloLense with your computer
- Build your Unity application
- Open the built project .sln file in Visual Studio 2019
- Make sure deployment is set to 'Release' and platform is 'ARM64'
- Go into Properties --> Debugging --> Change configuration to Release and Platform to ARM64 then enter the IP address of the HoloLens in the 'Machine Name' setting
- Click the green checkmark to begin building and deploying
- If the deployment fails, make sure you are on the same WiFi as the HoloLens and try again
- The deployment might take a few attempts, even when correctly setup
- If deployment succeeds, the application will automatically run on the Hololens

## Eye Tracking
- It is required to enable
- Add a Eye Gaze data provider by navigating to MRTK Toolkit --> Input -.............

## Azure Spatial Anchors

## 
