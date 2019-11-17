!function (global) {
  function getCurrentThemeColor() {
    return sessionStorage.getItem('color') || 'blue';
  }

  function createLoading() {
    var oLoading = document.createElement('div');
    oLoading.setAttribute('id', 'loading');
    var oDiv = document.createElement('div');
    var oSlogan = document.createElement('h2');
    oSlogan.innerText = 'welcome to pretty blog';
    oDiv.appendChild(oSlogan);
    for (var i = 0; i < 7; i++) {
      oDiv.appendChild(document.createElement('span'));
    }
    oLoading.appendChild(oDiv);
    return oLoading;
  }

  function loadTheme(color) {
    var oLink = document.createElement('link');
    oLink.setAttribute('rel', 'stylesheet');
    oLink.setAttribute('href', './assets/css/theme/material-kit-' + color + '.css');    
    document.head.appendChild(oLink);
  }
  var oLoading = createLoading();
  document.body.appendChild(oLoading);
  window.onload = () => {
    document.body.removeChild(oLoading);
  }
  loadTheme(getCurrentThemeColor());

  global.theme = {
    setCurrentTheme: function (color) {
      sessionStorage.setItem('color', color);
      loadTheme(color);
    }
  }
}(window);