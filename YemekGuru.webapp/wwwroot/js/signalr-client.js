var connection = new signalR.HubConnectionBuilder().withUrl("/adminhub").build();

async function start() {
    try {
        await connection.start();
        console.log("Hub ile bağlantı kuruldu!");
    } catch (e) {
        console.error(e);
        setTimeout(() => start(), 3000);
    }
}

connection.onclose(async () => {
    await start();
});

start();

const span = document.getElementById("span-total-client-count");
connection.on("ReceiveTotalClientCount", (clientCount) => {
    span.textContent = clientCount;
});