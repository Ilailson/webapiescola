# webapiescola

https://github.com/vsandrade/AspNetCoreWebAPI


An unhandled exception of type 'System.IO.IOException' occurred in System.Private.CoreLib.dll: 'Failed to bind to address http://127.0.0.1:5003: address already in use.'

No Linux ou macOS

    Abra o terminal.

    Execute o comando para listar os processos usando a porta 5003:

    bash

lsof -i :5003

Observe o número do processo (PID) associado à porta.

Encerre o processo usando o comando:

bash

    kill -9 <PID>

