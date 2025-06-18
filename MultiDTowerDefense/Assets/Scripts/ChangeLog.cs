using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLog 
{
    //

    /*
     
     6/10/2025  - 9:30pm
            Coordinates, Changelog, TODO file, Dimension Handler, DimensionClass
    6/12/2025  - 4:45pm
             Created Movement controller class
                -5Pm
            Modified MultiCoord Class
            Modified Dimension Class
            Modified Dimension Handler Class

                -8p:20m
             Modified MultiCoord - indexable with index in for coord values
            Modified Dimension class.IsOrientedDimension function - fixedxc syntax error
    
     
     6/13/2025  -   2pm
            Modified Movement Controller class. Now properly moves parent object when destination is changed
                    4Pm
            Created Units folder. created UnitObject and Health classes
                    5pm
            Modified Health class; added functionality. Added Attack/Defense/DamageType classes
     6/14/2025   -12:20pm
                Created Item folder (class not created yet). Created RandomFloat class. Modified UnitObject class to determine random success of attacks
     
     6/15/2025      10:33am
                Created User Input Handling and Camera Controller Classes
                    11:15pm
                Copied user input handling files from alternative project. edited to remove incompatobilities
                Modified Movement Controller to control object rotation
                    12:20pm
                User keyboard input has rudimentary control over camera movement and rotation
                    1pm
                Added waypoints to movement controller
                    1:40pm
                Movement controller now makes units face direction they are moving towards when new destination is implemented.
    6/16/2025
                    10:30pm
            Created UnitSpawner class     
            Capped Waypoints to 25 max.
                    12:30pm
            Testscript 4 periodically despawns units
            
    6/17/2025          
                    10:30am
            Created weapons / items. created inventory class
            Testscript5: adds test machine gun to base tower inventory
            MovementController: added IsMoving bool
                    11:20am
            Fixed spawner to give waypoints to spawned units
            Created UnitDetection script
                    12:10pm
            UnitDetection script detects and tracks closest unit objects
                    2:45pm
            Added action mode enum for unit behaviour
                    3pm
            UnitObject now interfaces with UnitDetector to target closest unit
    6/18/2025
                    8:30am
            Fixed some camera control errors.
            Worked on Unit action logic
            Fixed movement controller to not rotate camera accidently when moving
                    9am
            Fixed minor bugs in interactions between UnitObject class and Movement controller class
                    10am
            Attacks succesfully working. 
            HP percent now calculated correctly
                    10:30am
            Fixed Spawner Unit ID bug
            Fixed timing errors with unit spawner by adding delta time calculations
     
                    5:30pm
            Changed motion controller Lerp function
     
     */
    //
}
