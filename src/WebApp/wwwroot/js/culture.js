window.blazorCulture = {
    set: function (name, value) {
        document.cookie = name + "=" + value + ";path=/;max-age=31536000";
    }
};