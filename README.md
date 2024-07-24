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