
# 2D Box ESP

A simple Unity script for creating 2D box ESP (Extra Sensory Perception) indicators for game objects in a scene. This script utilizes Object-Oriented Programming (OOP) principles and is organized within the `unreliablecode` namespace.

## Features

- Create and manage ESP boxes for any game object.
- Automatically updates the position and size of the ESP boxes based on the target object's position and dimensions.
- Toggleable ESP functionality to enable or disable the display of boxes.

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/unreliablecode/2D-Box-ESP.git
   ```

2. Open the project in Unity.

3. Attach the `BoxESPManager` script to an empty GameObject in your scene.

4. Assign the `espCanvas` (a UI Canvas) and `mainCamera` (the main camera) in the inspector.

5. Call `CreateEspBoxForObject(targetObject)` for each GameObject you want to track with an ESP box.

## Usage

- The ESP boxes will automatically update their position and size based on the target objects in the scene.
- You can toggle the visibility of the ESP boxes by changing the `isEspEnabled` variable in the `BoxESPManager` script.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contributing

Contributions are welcome! Please feel free to submit a pull request or open an issue for any suggestions or improvements.

## Author

[unreliablecode](https://github.com/unreliablecode)
