# Godot 64 Controller
*Note: project is currently not in a usable state, check back for updates!*

## Overview

This project aims to provide a solid foundation for 90's style 3D platformer games built with the Godot engine.

## Expandable Player States

The main reason behind this project was to create a multi-use controller with a focus on extensibility and ease of use, particularly when it comes to defining and managing various player states. This with combination of a simple state flag system allows you to easily build out the controller for any use.

### Interface-Based State Design

The controller defines a standard structure for player states. Each state (like jumping, dashing, or sliding) must adhere to this structure, ensuring that new states can be added with minimal friction and existing states can be adjusted easily. 

### Create and Implement States with Ease

Adding new states or moves becomes a streamlined process:
- **Define the State:** Implement a new class adhering to the established state interface, defining the unique mechanics and transitions of the new state.
- **State Transition Logic:** Add logic to manage transitions between the new state and existing states, ensuring cohesive player movement and interaction.
- **Animation and Visual Feedback:** Connect your state logic with visual elements using Godot's animation system to provide visual feedback relative to the state.

## License
Distributed under the MIT License. See LICENSE for more information.
