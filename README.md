# FSOC PAT Simulator

An interactive simulation environment for Free-Space Optical Communication (FSOC) Pointing, Acquisition and Tracking (PAT).

## Overview

This project simulates an optical communication scenario involving a moving satellite/UAV target, optical beacon detection, noisy measurements, PAT state transitions, Kalman-based estimation, PID control, PTZ pointing, and an FSOC channel model.

The objective is to provide an experimental environment for studying the effect of target motion, measurement noise, angular jitter, detection dropout, vibration, atmospheric attenuation, turbulence, and pointing error on optical link performance.

## System Workflow

Moving Satellite/UAV
        ↓
Optical Beacon
        ↓
Optical Camera
        ↓
Beacon Detection
        ↓
Noisy Measurement
        ↓
Kalman Estimation
        ↓
PAT State Machine
        ↓
PID Controller
        ↓
PTZ Pan/Tilt
        ↓
Pointing Error
        ↓
FSOC Channel Model
        ↓
Received Power / Link Quality
        ↓
Performance Metrics

## PAT States

- Searching
- Acquiring
- Tracking
- Locked

## Main Simulation Components

### Target
- Satellite/UAV motion
- Optional lateral motion
- Optical beacon

### Detection
- Optical camera
- Beacon detection
- Image-space error
- Detection confidence
- Measurement noise
- Detection dropout

### Estimation
- 2D Kalman tracking

### Control
- PID controller
- PTZ pan/tilt control

### Disturbance Model
- Measurement noise
- Angular jitter
- Detection dropout
- Vibration

### FSOC Channel
- Range
- Beam divergence
- Receiver aperture
- Atmospheric attenuation
- Turbulence
- Pointing loss
- Received optical power
- Link quality

### Experimentation
- Scenario management
- Acquisition time
- Lock duration
- RMS pointing error
- Maximum pointing error
- Detection availability
- Link availability
- CSV experiment logging

## Technology Stack

- Unity
- C#
- Unity WebGL
- HTML
- JavaScript
- Python
- Git / GitHub
- Git LFS

## Research Basis

The architecture is inspired by published research and engineering work on optical communication Pointing, Acquisition and Tracking systems.

Key references include:

- ESA PATROL
- NASA TBIRD optical communication mission
- NASA optical communication pointing systems
- IEEE research on ATP mechanisms for mobile FSO
- Research on atmospheric effects in optical communication

## Running the Unity Project

1. Clone the repository.
2. Open the project using Unity Hub.
3. Open the MainSimulation scene.
4. Press Play.
5. Configure simulation parameters using the Unity Inspector or simulation controls.

## WebGL Version

The project also contains a browser-oriented WebGL deployment configuration.

The WebGL deployment uses:

- Unity WebGL
- Custom HTML interface
- JavaScript browser controls
- Python local HTTP server

## Project Status

Implemented:

- Satellite/UAV motion
- Optical beacon
- Optical camera
- Beacon detection
- Measurement noise
- Angular jitter
- Detection dropout
- PAT state machine
- Kalman tracking
- PID control
- PTZ control
- FSOC channel model
- Experiment metrics
- CSV logging
- WebGL deployment

Planned:

- More physically rigorous optical propagation model
- More rigorous Kalman implementation
- Computer-vision based beacon detection
- YOLO-based detection
- More detailed atmospheric turbulence model
- BER/SNR/link-margin calculation
- Hardware-in-the-loop experimentation

## Disclaimer

This is an engineering simulation and research prototype. The optical propagation, detector, actuator, and communication models are simplified representations intended for experimentation and visualization rather than flight qualification.