# webapiescola

https://github.com/vsandrade/AspNetCoreWebAPI

http://localhost:5000/swagger/smartschoolapi/swagger.json



testando filtros
http://localhost:5000/api/v1/aluno?ativo=1&nome=ma&matricula=1

Testando filtros com paginação
http://localhost:5000/api/v1/aluno?pageNumber=5&pageSize=2&ativo=1&nome=ma&matricula=1

http://localhost:5000/api/v1/aluno?pageNumber=1&pageSize=10&ativo=1&nome=ma&matricula=5


Criar imagem da api
docker build -t smartschool:1.0 .


Alterando para mysql
dotnet ef migrations add initMysql
dotnet ef database update


====================Front==========================
nvm list ... Listar versão node
nvm use v12.16.3...Mudando a versão node.

ng version ...Versão Angular

ng new... Criar projeto angular
ng serve... sobe a aplicação