# Unity Emission Checker Tool

This Unity Editor tool detects materials with emissions and allows users to selectively correct materials with redundant black emissions.

## Overview

Working on large Unity projects, especially when migrating older projects to newer Unity versions, can introduce various inconsistencies in assets. One of the prevalent issues is materials having their emission property enabled but set to black, leading to unnecessary calculations. This tool helps developers identify such materials and provides options to correct them easily.

## Why is this important?

- **Performance**: Materials with emissions, even if the color is set to black, can introduce additional calculations in shaders, potentially affecting performance.
  
- **Consistency**: Keeping materials set up consistently aids in maintaining project standards and preventing unforeseen behaviors in the future.

- **Ease of Use**: Manual checking of materials can become cumbersome, especially in projects with a large number of assets. This tool offers a centralized and automated solution.

## Usage

1. Open the tool via `Window` > `Emission Checker`.
2. Toggle `Show Redundant Emissions` to switch between showing all emissions or only black emissions.
3. Click `Refresh` to populate the list based on the current toggle setting.
4. Use the checkboxes to select materials you wish to modify.
5. Click `Disable Emission for Selected Materials` to rectify the selected materials.

## Contributing

Please feel free to fork, raise issues, or submit pull requests. All feedback or contributions are appreciated.
