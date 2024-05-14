# Tap

## Overview

Tap is an engaging Android game developed using the Unity engine. The objective of the game is to test and improve the player's reflexes and timing through a series of interactive challenges. This repository contains the complete source code and assets required to build and run the game.
It has really simple graphics and will run on any android device higher than v5.0

![Game Screenshot 1](Game%20Screenshot/Tap_1.png)

## Features

- **Platform**: Android
- **Engine**: Unity
- **Gameplay**: Tap-based controls for intuitive and addictive gameplay
- **Graphics**: Simple and clean UI for an enjoyable experience

![Game Screenshot 2](Game%20Screenshot/Tap_2.png)

## Installation

### Prerequisites

- Unity Hub and Unity Editor (preferably the Unity v5.4)
- Android SDK and NDK
- Git

### Steps

1. **Clone the Repository**:
    ```bash
    git clone https://github.com/adibakshi/Tap.git
    ```
2. **Open the Project in Unity**:
    - Open Unity Hub.
    - Click on the "Add" button and navigate to the cloned repository folder.
    - Select the folder to open the project in Unity.
3. **Build the Project**:
    - Go to `File > Build Settings`.
    - Select `Android` and click `Switch Platform`.
    - Configure the build settings as needed (e.g., resolution, orientation).
    - Click `Build` to generate the APK file.
4. **Install and Run on Android Device**:
    - Transfer the generated APK file to your Android device.
    - Install the APK and start playing.

## Code Structure

### Assets Folder

- **Animations**: Contains animation files for various game elements.
- **Audio**: Holds audio clips used in the game.
- **Prefabs**: Contains prefab files, which are reusable game objects.
- **Scenes**: Contains the Unity scene files. The main scene for the game will be here.
- **Scripts**: Contains all the C# scripts that define game logic and mechanics.
- **Sprites**: Holds image assets used for game graphics.

### Key Scripts

- **GameController.cs**: Manages the overall game state, including start, pause, and game over conditions.
- **PlayerController.cs**: Handles player input and interactions.
- **ObstacleSpawner.cs**: Controls the spawning of obstacles or game elements.
- **ScoreManager.cs**: Keeps track of the player's score and updates the UI accordingly.
- **UIManager.cs**: Manages UI elements like menus, score displays, and game messages.

### Scenes

- **MainScene.unity**: The primary scene where the gameplay takes place.
- **MenuScene.unity**: The scene for the main menu of the game.
- **GameOverScene.unity**: The scene displayed when the game ends.

## Detailed Script Overview

### GameController.cs

The GameController script is the heart of the game. It manages the game state, including starting, pausing, and ending the game. It also handles the game's main loop and coordinates with other scripts.

### PlayerController.cs

The PlayerController script handles player input and interactions. It processes touch inputs and translates them into game actions. It also manages player-related states and animations.

### ObstacleSpawner.cs

The ObstacleSpawner script is responsible for generating obstacles in the game. It controls the timing and positioning of obstacles to create a challenging and engaging gameplay experience.

### ScoreManager.cs

The ScoreManager script keeps track of the player's score. It updates the score based on game events and interacts with the UI to display the current score to the player.

### UIManager.cs

The UIManager script manages all the UI elements in the game. It handles transitions between different UI screens (e.g., menu, game over) and updates UI elements based on the game state.

## Contributing

We welcome contributions to enhance the game. To contribute:

1. Fork the repository.
2. Create a new branch for your feature or bugfix: `git checkout -b feature-name`.
3. Make your changes and commit them: `git commit -m 'Add new feature'`.
4. Push to the branch: `git push origin feature-name`.
5. Open a pull request detailing your changes.

### Contribution Guidelines

- Follow the coding standards used in the project.
- Write clear and descriptive commit messages.
- Test your changes thoroughly before submitting a pull request.
- Ensure your changes do not break existing functionality.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

## Contact

For any queries or support, feel free to open an issue on GitHub or contact me (Aditya Bakshi).
