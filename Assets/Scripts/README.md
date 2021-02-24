# CSE 3541 Lab 3
## Submission for: Kevin Oliver and Matthew Tran
### Controls

Up Arrow: Increases the amount of particles being spawned per second

Down Arrow: Decreases the amount of particles being spawned per second

# Lab Points
Physically Based Object Movement: 2/2 points
- Every object has a velocity and uses acceleration. For example, objects are always accelerating down because of gravity

Object Management: 2/2 points
- Scene makes new objects based on time and once objects are older than max age they are destroyed
- Objects change color every 1/3 of their max life, so they change over time

Object Emitter Variation: 1/1 point
- Every object size and rotation is randomized

Collision Detection: 2/2 points
- Objects collide with the non-axis aligned plane and with other objects
NOTE: The plane is at y=0 but we are detecting collision with the plane, not the axis. This is demonstrated by objects dropping off around the plane

Collision Response: 2/2 points
- Objects collide in a physically interesting way
- Using law of reflection
- Also applying force in direction away from center of other object

Documentation: 1/1 points
-This README is the documentation

Extra Features 1/1points
-The feature that allows you to change how many particles per second are spawned is an extra feature

Total: 11/10
  