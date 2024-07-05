# Touchpad-Controlled 3D Navigation with WebView2 via RawInput.TouchPad

This project is a PhD research demonstrates a proof-of-concept for using raw touchpad input data to control 3D navigation within a WebView2-embedded web page. It leverages C# (WPF) to capture touchpad data, communicate with the embedded web page, and visualize touch points in real time.
The original RawInput.Touchpad source code by [emoacht](https://github.com/emoacht/RawInput.Touchpad) and Extended from my research [banyapon](https://github.com/banyapon/RawInputTouchPadWindows).

## Key Features
* **Enhanced Multitouch Support:**  Builds upon the RawInput.Touchpad foundation to enable more robust multitouch interactions.
* **Research-Oriented:** Designed for academic exploration and experimentation with multitouch technologies.
* **Windows Compatibility:** Specifically tailored for the Windows operating system.

![Image of This Application](https://github.com/banyapon/RawInputWebViewJSONTouch/blob/main/images/pic1.png?raw=true)

## Extended Key Features
* **Raw Touchpad Input:** Captures detailed touch data from precision touchpads (including contact ID, X/Y coordinates).
* **C#-JavaScript Communication:** Utilizes WebView2's PostWebMessageAsString to send touchpad data to the web page.
* **Real-time Visualization:** Renders touch points as circles on an HTML canvas within the WebView2 control.

![Image of This Application](https://github.com/banyapon/RawInputWebViewJSONTouch/blob/main/images/pic2.png?raw=true)

![Image of This Application](https://github.com/banyapon/RawInputWebViewJSONTouch/blob/main/images/pic3.png?raw=true)

## Project Structure
* **RMainWindow.xaml:** Defines the WPF window layout, including the WebView2 control.
* **RMainWindow.xaml.cs:** Contains the C# code for handling touchpad input, communicating with the web page, and visualizing the data.
* **Rindex.html:** The embedded HTML page with the JavaScript code to display the touch points on a canvas.
* **TouchpadHelper.cs:** (Assumed) Helper class for touchpad interaction (not included in this example).

## How it Works
* **Touchpad Data Capture:** The C# code uses the Raw Input API (through TouchpadHelper) to receive raw touchpad input events.
* **Data Formatting:** The captured data is formatted into a string representing contact points (e.g., "Contact ID:0 Point:879,507").
* **Communication:** The formatted data is sent to the index.html page via PostWebMessageAsString.
* **Web Page Visualization:** The JavaScript code in index.html parses the string, extracts the touch point coordinates, and dynamically draws circles on the canvas to represent the touch positions.

## Important Note: Disable Windows Touchpad Gestures

Before using this project, it's crucial to disable the default Windows touchpad gestures in your system settings. This ensures accurate and unhindered multitouch data collection.
![Image of Windows Touchpad Gesture Settings](https://github.com/banyapon/RawInputTouchPadWindows/blob/main/screenA.png?raw=true)

## Multitouch in Action

Here's a visualization of 5 simultaneous touch points being recognized and processed by the project:
![Image of This Application](https://github.com/banyapon/RawInputWebViewJSONTouch/blob/main/images/pic4.png?raw=true)

## Setup Instructions
Prerequisites:
**Visual Studio:** Ensure you have Visual Studio installed with the necessary WPF development tools and the WebView2 SDK.
**Microsoft Edge (Chromium):** WebView2 relies on the Microsoft Edge (Chromium) runtime. Make sure it's installed and up-to-date.
**Clone the Repository:**

```html
git clone https://github.com/your-username/Touchpad-Controlled-3D-Navigation.git
```
**Open and Build:** Open the solution in Visual Studio and build the project.
**Run:** Run the application. You should see a window with an embedded web page. Interacting with your touchpad will display touch points as circles on the canvas.

## Potential Improvements
**3D Navigation Integration:** Implement actual 3D navigation controls based on the touchpad input.
**Gesture Recognition:** Incorporate machine learning or gesture recognition algorithms to interpret more complex touchpad interactions.
**Customization:** Allow for customization of the visualization (e.g., color, size of the circles) and the types of touchpad events to capture.

## Research Potential
This project serves as a foundation for exploring novel touchpad-based 3D navigation techniques. Future research could focus on:

**Intuitive Gesture Design:** Designing user-friendly gestures for 3D manipulation.
**Machine Learning Models:** Training models to recognize complex gestures and map them to 3D actions.
**User Studies:** Evaluating the effectiveness and usability of the new gestures.

## Contributing

Contributions are welcome! Feel free to open issues, submit pull requests, or share your ideas for extending this project. Let me know if you'd like any adjustments or further details in this README file!

## Research Affiliation

This project is part of the ongoing research conducted at the School of Information Technology, King Mongkut's Institute of Technology Ladkrabang.

![Image of School of Information Technology Logo](https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS5b-D1sOtWlEknmbzk-dSl8WRjjl3bcXXwk-0I_qBxhvvb4HPYpaHWUC5vHWnlySdmEg&usqp=CAU)

## License

This project is licensed under the [MIT License](LICENSE). 
