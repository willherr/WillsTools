"use strict";
(function () {
    const willsToolsInfo = {
        version: "0.2003.0"
    };
    const willsToolsKey = "WillsTools";

    let needsHardRefresh = false;
    let clientWillsToolsInfo = willsToolsInfo;
    const temp = localStorage.getItem(willsToolsKey);

    if (temp) {
        clientWillsToolsInfo = JSON.parse(temp);
        if (clientWillsToolsInfo.version !== willsToolsInfo.version) {
            clientWillsToolsInfo.version = willsToolsInfo.version;
            needsHardRefresh = true;
        }
    }
    localStorage.setItem(willsToolsKey, JSON.stringify(clientWillsToolsInfo));

    if (needsHardRefresh) {
        location.reload(true);
    }
}())
