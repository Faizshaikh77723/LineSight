mergeInto(LibraryManager.library, {

    BrowserSetSatelliteSpeed: function (value) {
        SendMessage(
            "SimulationManager",
            "SetSatelliteSpeed",
            UTF8ToString(value)
        );
        SendMessage(
            "SimulationManager",
            "BrowserSetSatelliteSpeed",
            UTF8ToString(value)
        );
    },

    BrowserSetPan: function (value) {
        SendMessage(
            "SimulationManager",
            "SetPan",
            UTF8ToString(value)
        );
        SendMessage(
            "SimulationManager",
            "BrowserSetSatelliteSpeed",
            UTF8ToString(value)
        );
    },

    BrowserSetTilt: function (value) {
        SendMessage(
            "SimulationManager",
            "SetTilt",
            UTF8ToString(value)
        );
        SendMessage(
            "SimulationManager",
            "BrowserSetSatelliteSpeed",
            UTF8ToString(value)
        );
    },

    BrowserSetNoise: function (value) {
        SendMessage(
            "SimulationManager",
            "SetMeasurementNoiseAmplitude",
            UTF8ToString(value)
        );
        SendMessage(
            "SimulationManager",
            "BrowserSetSatelliteSpeed",
            UTF8ToString(value)
        );
    },

    BrowserSetJitter: function (value) {
        SendMessage(
            "SimulationManager",
            "SetAngularJitterAmplitude",
            UTF8ToString(value)
        );
        SendMessage(
            "SimulationManager",
            "BrowserSetSatelliteSpeed",
            UTF8ToString(value)
        );
    },

    BrowserSetDropout: function (value) {
        SendMessage(
            "SimulationManager",
            "SetDropoutProbability",
            UTF8ToString(value)
        );
        SendMessage(
            "SimulationManager",
            "BrowserSetSatelliteSpeed",
            UTF8ToString(value)
        );
    },

    BrowserSetTurbulence: function (value) {
        SendMessage(
            "SimulationManager",
            "SetTurbulenceStrength",
            UTF8ToString(value)
        );
        SendMessage(
            "SimulationManager",
            "BrowserSetSatelliteSpeed",
            UTF8ToString(value)
        );
    }

});