function showProtocolDialog(message, install)
{
    jQuery("#fullscreen_overlay").fadeIn(500, function() { jQuery("#protocolRedirectSplashContainer").fadeIn(500) });
    jQuery("#dialogMessage").text(message);
    if(install)
    {
        jQuery("#dialogInstall").show();
        jQuery("#protocolRedirectButton").hide();
    } else {
        jQuery("#dialogInstall").hide();
        jQuery("#protocolRedirectButton").show();
    }
}

function waitUntilGlueInitialized(cb, timeout) {
	if (!isSupportedBrowser()) {
		cb.call();
		return;
	}
	if (isGlueInstalled()) {
		var info = getGlueInfo();
		if (info && info.initialized == "true") {
			cb.call(null, info);
			return;
		}
	}
	if (timeout && timeout < 0) {
		cb.call();
		return;
	}
	var freq = 500;
	setTimeout(function() {
		waitUntilGlueInitialized(cb, parseInt(timeout) - freq);
	}, freq);
}

function glueInstalledCheck(cb, timeout){
    if (!isSupportedBrowser()) {
        cb.call();
        return;
    }
    if (isGlueInstalled()) {
        cb.call( null, true );
        return;
    }
    if (timeout && timeout < 0) {
        cb.call();
        return;
    }
    var freq = 500;
    setTimeout(function() {
        glueInstalledCheck(cb, parseInt(timeout) - freq);
    }, freq);
}

function getGlueInfo() {
	var info = new Object();
	try {
        if ( isFirefox() ){
            jQuery.getScript("glue://info");
        }
        else {
            Glue.ProtocolHandler.request("glue://info");
        }
    } catch ( e ) {}
	var glueInfo = document.getElementById("glueinfo");
	if (glueInfo) {
		var pairs = glueInfo.firstChild.nodeValue.split(";");
		for(var i = 0; i < pairs.length; i++) {
			var t = pairs[i].split("=");
			info[t[0]] = t[1];
		}
	}
	return info;
}

function getProtocolPath() {
	var s = document.URL;
	var idx = s.indexOf("glue.com/");
	return s.substring(idx + "glue.com/".length);
}

function isSupportedBrowser() {
    var supported = false;
        jQuery.each(jQuery.browser, function(i, val) {
        if ((jQuery.browser.mozilla)||(navigator.appVersion.indexOf("MSIE") != -1 && parseFloat(navigator.appVersion.split("MSIE")[1]) > 6)) {
            supported = true;
        };
    });
    return supported;
}

function isFirefox() {
	var browser = navigator.userAgent;
	return (browser.indexOf("Firefox") != -1);
}

function isGlueInstalled() {
    return navigator.userAgent.indexOf("Glue") != -1 || document._abGlueInstalled || document.documentElement.getAttribute("_abGlueInstalled");

}