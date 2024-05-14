# Tap

## Overview

Tap is an engaging Android game developed using the Unity engine. The objective of the game is to test and improve the player's reflexes and timing through a series of interactive challenges. This repository contains the complete source code and assets required to build and run the game.

## Features

- **Platform**: Android
- **Engine**: Unity
- **Gameplay**: Tap-based controls for intuitive and addictive gameplay
- **Graphics**: Simple and clean UI for an enjoyable experience

## Installation

### Prerequisites

- Unity Hub and Unity Editor (preferably the version used in the project)
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

## Contributing

We welcome contributions to enhance the game. To contribute:

1. Fork the repository.
2. Create a new branch for your feature or bugfix: `git checkout -b feature-name`.
3. Make your changes and commit them: `git commit -m 'Add new feature'`.
4. Push to the branch: `git push origin feature-name`.
5. Open a pull request detailing your changes.

## License

This project is licensed under the MIT License. See the LICENSE file for more details.

## Contact

For any queries or support, feel free to open an issue on GitHub or contact the repository owner.
