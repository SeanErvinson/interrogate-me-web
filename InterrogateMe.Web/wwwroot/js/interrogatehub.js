class InterrogateHub {
    constructor() {
        this._hub = null;
        this.debug = false;
        this.onNotifyList = null;
    }

    start(groupName) {
        this._hub = new signalR.HubConnectionBuilder()
            .withUrl("/interrogate?groupName=" + groupName)
            .build();
        this._hub.onclose(() => {
            if (this.debug)
                console.log("Connection Closed")

            let interval = setInterval(() => {
                this.start(groupName).then(() => {
                    clearInterval(interval);
                    if (this.debug)
                        console.log("Connection Reconnected");
                });
            }, 5000);
        });

        this._hub.on("OnNotifyList", Question => {
            if (this.debug) console.log("Notify");
            if (this.onNotifyList) this.onNotifyList(Question);
        });

        return this._hub.start();
    }

}