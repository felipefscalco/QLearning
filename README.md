# QLearning

### C# Implementation of Q-Learning algorithm to solve a defined problem

Q-learning is a model-free reinforcement learning algorithm and values-based learning algorithm.      
Value based algorithms updates the value function based on an certain equation.

Basically an agent is placed in an environment and has to learn how to behave successfully in that environment.

### Picture of the specific problem
![Server and Client running](https://i.imgur.com/7KSKRCN.png)


The goal here is to find the optimal path to go from state 1 to state 50.

Rewards:  
* R = 100 for state 50 (final state)
* R = -1 for states blue
* R = -100 for states black

The black states represent impassable locations

The Q-Table is updated in the current way:  
Let's say we are at the state 1.
The possible moves are 2 and 10, we will choose 2.  
Update the QLine: Q(1, north) = r + 0.5 * max(Q(2, north), Q(2, east), Q(2, south))
