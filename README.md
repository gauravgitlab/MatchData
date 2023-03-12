# MatchData
Match Data - The "Applicant-test.dat" datafile consists of roughly 9 minutes of soccer data. Each line in the file is a single frame. This data is 25 frames per second.

# Data format
Frame:FrameCount:[TrackedObjects][BallData]

TrackedObject:Team,TrackingID,PlayerNumber,X-Position,Y-Position,[…];

BallData:

:X-Position,Y-Position,Z-Position,BallSpeed,[ClickerFlags]

# Use
- We are using the Unity Version 2021.3.5f1
- Go to Assets/Scenes/MatchData.Unity to run the test.
- The test is running on target frame rate 25, which takes around 9 mins 6 seconds to draw all frames in .dat file.
- In this demo, we are Instantiating the player in Capsule form and ball is instantiate as Sphere primitive gameobject.
- The scripts folder have sub-folders
    - "Core" folder contains scripts responsible for the base of the test.
    - "Data" folder contains scripts holding data related to test.
    - "Services" folder contains scripts related to running the frame and other actions in the test.
    - "UI" folder contains scripts for displaying UI element on screen.


# Improved versions suggestions
- I believe we can improve the code if the .dat comes with header, for ex
---------------------------------------------------------------------------------

#frame, team, trackingid, playerNumber

1519819,0,1,18

----------------------------------------------------------------------------------
I am imagning to take this data in dictionary with key as header and frame data as values.  


