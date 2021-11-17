
//*******************************************************************************************************
//Js run time functions **********************************************************************************
//*******************************************************************************************************

//culture language config
window.blazorCulture = {
    get: () => localStorage['BlazorCulture'],
    set: (value) => localStorage['BlazorCulture'] = value
};

//return the view dimensions
window.getWindowDimensions = function () {
    return {
        Width: window.innerWidth,
        Height: window.innerHeight
    };
};