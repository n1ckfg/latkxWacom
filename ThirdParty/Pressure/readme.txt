
All rights reserved. 2013

----------------------------------------------
		Pressure wacom wrapper for Unity
		Copyright 2013 Zbuffers
		http://zbuffers.blogspot.fr/
		support : zbufferslab@gmail.com
----------------------------------------------

Thank you for downloading Pressure!



---------------------------------------
 Content
---------------------------------------

- dll plugin :
	- .bundle for mac os x
	- .dll for windows
	- pressure library
	
- PressureManager prefab to include in your hierarchy project.

- example projects and prefabs.



---------------------------------------
 HOW TO
---------------------------------------

Quick test example : 
- open the "PressureDrawingExample" scene.
- click Unity GameMode button.
- draw...


Monitor values example : 
- open the "PressureSimpleExample" scene.
- select the "PressureInput" object to see his inspector.
- click Unity GameMode button.
- you can see the values received from your wacom tablet in the inspector and in the game window.


Write your own script to access values : 
- drag the PressureManager prefab in your hierarchy.
- in your own script, access the field values following this convention : 
		bool any_name = PressureManager.eraser;
		Vector2 any_name = PressureManager.cursorPosition;
		float any_name = PressureManager.normalPressure;
		float any_name = PressureManager.tilt; // only intuos and cintiq
		float any_name = PressureManager.tangentialPressure; // only airbrush wacom stylus
		float any_name = PressureManager.rotation; // art pen wacom stylus only



_________________________________________

If you have any questions, suggestions, comments or feature requests, please
contact support.
