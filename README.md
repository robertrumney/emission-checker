# Unity Emission Checker Tool

This Unity Editor tool detects and corrects materials with redundant black emissions.

## Overview

Working on large Unity projects, especially when migrating older projects to newer Unity versions, can introduce various inconsistencies in assets. One of the most common is materials having their emission property enabled but set to black. This configuration is visually indistinguishable from having emissions turned off and can introduce unnecessary overhead, potentially impacting rendering performance.

This tool aims to streamline the optimization process by automatically identifying and, optionally, rectifying these materials.

## Why is this important?

- **Performance**: Even if the emission color is set to black, enabling the emission property can introduce additional calculations in shaders, leading to potential performance hits.
  
- **Consistency**: Ensuring that materials are set up correctly and consistently helps maintain project standards and can prevent unexpected behaviors in the future.

- **Ease of Use**: Manual checking can be tedious, especially in projects with a vast number of materials. This tool provides a centralized and automated solution.

## Usage

1. Open the tool via `Window` > `Emission Checker`.
2. Click `Refresh` to list all materials with redundant black emissions.
3. If desired, click `Disable Emission for All Listed Materials` to fix all detected materials.

## Contributing

Feel free to fork, raise issues, or submit pull requests. Any feedback or contribution is appreciated.
