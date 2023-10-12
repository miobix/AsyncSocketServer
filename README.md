# AsyncSocketServer

TCP/IP Sockets Server WinForm. 

## How to use


Launch program, start server to listen to any clients. 

Program will log the connected client info on Connection Event

Program will log messages received in the server from clients

Use the TextBox to send a message to all connected Clients.


## Client connections

Use the AsyncSocketClient solution to launch a client capabable of communicating with this server

Alternatively, use Telnet to test a connection.

Run cdm, 

> telnet 127.0.0.1 23000

- or -

> telnet (your ip) 23000
