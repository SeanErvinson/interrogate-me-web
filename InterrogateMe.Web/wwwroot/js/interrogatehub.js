class InterrogateHub {
    constructor() {
        this._hub = null;
        this.debug = false;
        this.onNotifyList = null;
        this.onQuestionCount = null;
    }

    start(groupName) {
        this._hub = new signalR.HubConnectionBuilder()
            .withUrl("/interrogate?groupName=" + groupName, {
                transport: signalR.HttpTransportType.WebSockets
            })
            .build();
        this._hub.onclose(() => {
            if (this.debug)
                console.debug("Connection Closed")

            let interval = setInterval(() => {
                this.start(groupName).then(() => {
                    clearInterval(interval);
                    if (this.debug)
                        console.debug("Connection Reconnected");
                });
            }, 5000);
        });

        this._hub.on("OnNotifyList", Question => {
            if (this.debug) console.debug("Notify");
            if (this.onNotifyList) this.onNotifyList(Question);
        });

        this._hub.on("OnUpdateQuestionCount", count => {
            if (this.debug) console.debug("Notify");
            if (this.onQuestionCount) this.onQuestionCount(count);
        });


        return this._hub.start();
    }

}