VR Shooting Range Demo

For PUN instructions see the PUN readme at: "Photon/PhotonUnityNetworking/readme.txt"

This demo requires a Virtual Reality headset to work properly. It supports HTC Vive (SteamVR) and Oculus Rift (OVR) as well as DayDream/Cardboard (GoogleVR) and GearVR. 
Tested on Unity 2018.1.4f1.

To get this demo working:

1) Set up your VR device correctly.
2) Create a fresh project and download the package from the Asset Store.
3) Set up PUN (see the PUN wizard or the readme). You can either host your own server or use the free Photon Cloud trial. See www.photonengine.com for more information.

Run in editor:

4) Open the scene 'DemoVrShootingRange-Scene' from the "Scenes" folder.
5) Optional: Navigate to '[Services] > Factories' in the scene hierarchy and check the 'useNonVrPlayerInEditor' option on the 'PlayerFactory' component to enable mouse control in editor-mode. 'useNonVrPlayerInEditor' is only available in the Unity Editor.
6) Hit the play button.

Run standalone build:

4) Add the scene 'DemoVrShootingRange-Scene' from the "Scenes" folder to the build settings.
5) Use the included build tool to generate builds for your chosen target platform:
	- Press Ctrl + Alt + B to highlight UMake
	- Choose one of the 4 options available and click on the "Build" button":
		* GoogleVR - Builds for DayDream / Cardboard
		* GearVR - Builds for GearVR
		* Windows-Vive - Builds for HTC Vive and OpenVR
		* Windows-Rift - Builds for the Oculus Rift
6) Run the generated build


This VR-Multiplayer-Demo was made by N-iX. More information: www.n-ix.com