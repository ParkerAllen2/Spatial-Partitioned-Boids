# Spatial-Partitioned-Boids
I did this project because I thought it looked cool, as for what it is. Using the Unity engine I made a 2D simulation using the Boid algorithm and optimized it with spacial partitioning. The 2's after some of the scripts are because they are the second attempts and out preformed the frist attempt.

**Code** folder contains easier access to the code described below

As for the performance of the algorithm on my laptop in a web browser. 
Spacial Partitioning | # of Boids | FPS
---------------------|------------|-----
True | 5000 | 45
False | 1000 | 30

### Boid Script
* **Boid2:** This script was in charge of calculating the movementof an object based on the vector from the standard 3 rules (coherence, separation, and alignment) and a bonus obstacle avoidance rule. Then takes the calculated vector and moves the boid.

* **BoidManager2:** This script is in charge of calculating the value of each rule and sending those values to Boid2

* **BoidSettings:** This script just stores some base values all the boids use, some examples would be weights to adjust the value of the rules

* **BoidHelper:** This script stores the vectors for the raycast that look for walls

### Spacial Partitioning
* **Grid2:** This script creates and stores an array of Cell2's as well as creates and updates a doubly linked list of Cell2's that have boids in them. This is also responsable for updating which Cell2 a boid is in.

* **Cell2:** This script stores a doubly linked list of boids positioned in it, and to speed up the boid algorithm and average position and average direction of the the boids.

* **Spawner2:** This script creates a Grid2 and spawning boids and adding them to BoidManager2
