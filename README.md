Test

# Shelyak UVEX

## Introduction
The Shelyak UVEX software allows the control of an UVEX spectrograph equipped with a motor.

It consists of two distinct parts. On the one hand, a Web application, the part you are currently in, which allows configuration and manual control through a browser. And on the other hand, three ASCOM drivers allowing control from your usual control software (PRISM, N.I.N.A, ...).

## Configuration
When using it for the first time, you will be directed to the configuration page where you will have to select the COM port corresponding to your UVEX. Optionally, you can also configure the network density.

### COM Port
Displays the list of COM ports detected on your computer. Select the one corresponding to your UVEX. If the UVEX was not connected when the configuration page was displayed, you will need to reload it (F5 key or reload button in your browser toolbar).

### Network density
Specify here the density of the network installed in your UVEX

### Swagger
Swagger is a web interface that allows you to interact directly with the device using the API. A server restart is required after a configuration change

### Server shutdown
This function allows you to stop the web service. Do not perform this action if the UVEX is currently being used via ASCOM drivers.

## UVEX Control
The control page allows you to display various information and control the functions of the UVEX.

### Information and probes
These two areas display various information related to your UVEX as well as the current temperature measured by the device.

### Controls
The other components of this page allow you to control the UVEX.

The first three areas contain the following elements:
- The current position of the element concerned
- The step size as well as two + and - buttons to increment/decrement the current position
- Target value followed by two buttons. "Go to" will move the element to the indicated value. "Calibrate" will redefine the current position with the target value.

## ASCOM Drivers
Since the ASCOM platform does not provide a specific driver to control a spectrograph, it was necessary to use the available interfaces.

### Focuser Driver
It allows the focus control.

### Rotator Driver
It allows the control of the grating rotation.

### Filter wheel Driver
It allows the activation of the calibration functions (Dark, ...)

### Connection
When connecting a driver, the UVEX service will be automatically started if it has not been previously executed, either manually or by one of the other ASCOM drivers.
.