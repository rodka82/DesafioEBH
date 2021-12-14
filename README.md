# DesafioEBH

Este projeto foi desenvolvido como um teste para EdubraHub

Para rodar o projeto localmente:

1. **Instale o SDK do .NET 5**
2. **Instale o Visual Studio**
3. **Clone o projeto no Visual Studio usando este endereço do repositorio: *https://github.com/rodka82/DesafioEBH.git***
4. **Execute o projeto em modo debug com o projeto com IIS Express**
5. **Será aberto o Swagger da aplicação**
6. **A API é protegida por token JWT. O únido endpoint liberado é o do JWT. Execute-o e copie o token gerado**
7. **Clique no botão de autenticação (um cadeado no alto da página do swagger).**
8. **No campo requerido escreva Bearer + token gerado **. Isto liberará o acesso aos endpoints.**

A aplicação pode:
- Cadastrar, alterar, excluir e pesquisar Loja (apenas por ID)
- Cadastrar, alterar, excluir e pesquisar Produto (apenas por ID)
- Criar estoque de um produto em uma loja (relacionar loja e produto e inserir uma quantidade inicial de itens)
- Adicionar itens de um produto ao estoque
- Retirar itens de um produto do estoque 
Obs: (O Entity Framework está configurado para não permitir acessos simultâneos alterando este valor. Caso aconteça, uma exceção será lançada. Testes de carga feitos pelo JMeter simulando até mil usuários simultâneos.)

O banco de dados utilizado é o Sqlite. O mesmo é destruído e recriado a cada inicialização se utilizando de migrations para isso. Dados iniciais são inseridos a cada inicialização da aplicação.






