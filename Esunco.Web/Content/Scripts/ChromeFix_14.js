var scriptExecutedAttrName = "data-executed";
function isScriptExecuted(script) {
    return _aspxGetAttribute(script, scriptExecutedAttrName);
}

function markScriptAsExecuted(script) {
    _aspxSetAttribute(script, scriptExecutedAttrName, true);
}

function _aspxInitializeScripts() {
    var scripts = _aspxGetIncludeScripts();
    for (var i = 0; i < scripts.length; i++)
        _aspxCacheIncludeScript(scripts[i]);

    var startupScripts = _aspxGetStartupScripts();
    for (var i = 0; i < startupScripts.length; i++)
        markScriptAsExecuted(startupScripts[i]);
}

function _aspxRunStartupScriptsCore() {
    var scripts = _aspxGetStartupScripts();
    var code;
    for (var i = 0; i < scripts.length; i++) {
        if (!isScriptExecuted(scripts[i])) {
            code = _aspxGetScriptCode(scripts[i]);
            eval(code);
            markScriptAsExecuted(scripts[i]);
        }
    }
}

function _aspxOnScriptReadyStateChangedCallback(scriptElement, isCallback) {
    if (scriptElement.readyState == "loaded") {
        _aspxCacheIncludeScript(scriptElement);

        for (var i = 0; i < __aspxCreatedIncludeScripts.length; i++) {
            var script = __aspxCreatedIncludeScripts[i];
            if (_aspxIsKnownIncludeScript(script)) {
                if (!isScriptExecuted(script)) {
                    markScriptAsExecuted(script);
                    _aspxAppendScript(script);
                    __aspxAppendedScriptsCount++;
                }
            } else
                break;
        }

        if (__aspxCreatedIncludeScripts.length == __aspxAppendedScriptsCount)
            _aspxFinalizeScriptProcessing(isCallback);
    }
}