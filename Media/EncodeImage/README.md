# Image to Base64 Encoder

This project is a simple C# console application that converts an image file to a Base64 encoded string and generates an HTML file to display the original image and its Base64 encoded version. The project also generates a JavaScript file to store the Base64 encoded string and sets the image as a favicon in the HTML file.

## Features

- Converts an image file to a Base64 encoded string.
- Saves the Base64 encoded string to a `.txt` file.
- Generates a JavaScript file to store the Base64 encoded string.
- Generates an HTML file to display the original image and its Base64 encoded version.
- Sets the image as a favicon in the HTML file.

## Usage

1. **Compile the Program:**

   Open a terminal and navigate to the directory containing `Program.cs`. Then, compile the program using the .NET CLI:

   ```sh
   dotnet build
   ```

2. **Run the Program:**

   Run the compiled program using the .NET CLI:

   ```sh
   dotnet run ./assets/image-01.jpg
   ```
